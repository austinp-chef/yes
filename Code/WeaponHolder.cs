using System;

/// <summary>
/// FPS inventory + weapon system. Attach to the Player Controller.
/// - E to pick up items into hotbar slots
/// - 1-5 or scroll to switch slots
/// - Left click to fire (ranged) or swing (melee)
/// - Q to drop current item
/// </summary>
public sealed class WeaponHolder : Component
{
	// --- Pickup ---
	[Property, Category( "Pickup" )] public float PickupRange { get; set; } = 150f;
	[Property, Category( "Pickup" )] public GameObject CameraObject { get; set; }

	// --- Firing ---
	[Property, Category( "Firing" )] public float FireRate { get; set; } = 0.12f;
	[Property, Category( "Firing" )] public float RecoilKick { get; set; } = 1.5f;
	[Property, Category( "Firing" )] public float RecoilRecovery { get; set; } = 10f;

	// --- Ammo ---
	[Property, Category( "Ammo" )] public int MaxAmmo { get; set; } = 30;
	[Property, Category( "Ammo" )] public int MaxReserve { get; set; } = 120;
	[Property, Category( "Ammo" )] public float ReloadTime { get; set; } = 1.8f;

	// --- View Bob ---
	[Property, Category( "View Bob" )] public float BobFrequency { get; set; } = 10f;
	[Property, Category( "View Bob" )] public float BobAmountX { get; set; } = 0.4f;
	[Property, Category( "View Bob" )] public float BobAmountY { get; set; } = 0.3f;

	// --- Sway ---
	[Property, Category( "Sway" )] public float SwayAmount { get; set; } = 0.6f;
	[Property, Category( "Sway" )] public float SwaySmooth { get; set; } = 8f;
	[Property, Category( "Sway" )] public float TiltAmount { get; set; } = 2f;

	// --- FPS Position ---
	[Property, Category( "Position" )] public Vector3 HoldPosition { get; set; } = new Vector3( 20f, 8f, -6f );
	[Property, Category( "Position" )] public Angles HoldAngles { get; set; } = new Angles( 15f, 0f, 0f );
	[Property, Category( "Position" )] public Vector3 BarrelOffset { get; set; } = new Vector3( 35f, 6f, -5f );

	// --- Melee ---
	[Property, Category( "Melee" )] public float MeleeRate { get; set; } = 0.5f;
	[Property, Category( "Melee" )] public float MeleeSwingKick { get; set; } = 3f;

	// ── Inventory ──
	public const int SlotCount = 5;
	public WeaponPickup[] Inventory { get; private set; } = new WeaponPickup[SlotCount];
	public int SelectedSlot { get; set; } = 0;
	public WeaponPickup HeldWeapon => Inventory[SelectedSlot];

	// ── Ammo state ──
	public int CurrentAmmo { get; private set; }
	public int ReserveAmmo { get; private set; }
	public bool IsReloading { get; private set; }
	public float ReloadProgress { get; private set; }

	// ── Private ──
	private float _originalZNear;
	private GameObject _viewModel;
	private GameObject _meshChild;
	private float _lastFireTime;
	private float _lastMeleeTime;
	private float _reloadStartTime;

	// Melee swing animation
	private float _swingTime = -1f;
	private const float SwingDuration = 0.35f;
	private bool _swingHasHit;

	// Motion state
	private float _bobTimer;
	private Vector3 _currentSway;
	private Vector3 _currentBob;
	private float _currentRecoil;
	private float _currentTilt;
	private PlayerController _playerController;

	protected override void OnStart()
	{
		if ( CameraObject is null )
		{
			var cam = GameObject.GetComponentInChildren<CameraComponent>();
			if ( cam is not null )
				CameraObject = cam.GameObject;
		}
		_playerController = GameObject.GetComponent<PlayerController>();
	}

	protected override void OnUpdate()
	{
		if ( IsProxy ) return;

		HandleSlotInput();
		HandlePickupDrop();

		if ( HeldWeapon is not null && _viewModel is not null && _viewModel.IsValid )
		{
			UpdateWeaponMotion();
			PositionViewModel();
			UpdateReload();
			UpdateSwingAnimation();

			if ( Input.Down( "attack1" ) )
				HandleAttack();
		}
	}

	// ─── Slot Input ───

	private void HandleSlotInput()
	{
		var prevSlot = SelectedSlot;

		if ( Input.Pressed( "Slot1" ) ) SelectedSlot = 0;
		if ( Input.Pressed( "Slot2" ) ) SelectedSlot = 1;
		if ( Input.Pressed( "Slot3" ) ) SelectedSlot = 2;
		if ( Input.Pressed( "Slot4" ) ) SelectedSlot = 3;
		if ( Input.Pressed( "Slot5" ) ) SelectedSlot = 4;
		if ( Input.Pressed( "SlotNext" ) ) SelectedSlot = (SelectedSlot + 1) % SlotCount;
		if ( Input.Pressed( "SlotPrev" ) ) SelectedSlot = (SelectedSlot - 1 + SlotCount) % SlotCount;

		if ( prevSlot != SelectedSlot )
			OnSlotChanged( prevSlot );
	}

	private void OnSlotChanged( int prevSlot )
	{
		// Destroy current viewmodel
		DestroyViewModel();
		IsReloading = false;

		// Create new viewmodel if slot has an item
		if ( HeldWeapon is not null )
			CreateViewModel( HeldWeapon );
	}

	// ─── Pickup / Drop ───

	private void HandlePickupDrop()
	{
		if ( Input.Pressed( "use" ) )
			TryPickup();

		// Q to drop
		if ( Input.Pressed( "Slot0" ) && HeldWeapon is not null )
			DropCurrent();
	}

	private void TryPickup()
	{
		if ( CameraObject is null ) return;

		var eyePos = CameraObject.WorldPosition;
		var eyeEnd = eyePos + CameraObject.WorldRotation.Forward * PickupRange;

		var tr = Scene.Trace
			.Ray( eyePos, eyeEnd )
			.WithoutTags( "player" )
			.HitTriggers()
			.Run();

		if ( !tr.Hit || tr.GameObject is null ) return;

		var pickup = tr.GameObject.GetComponent<WeaponPickup>();
		if ( pickup is null && tr.GameObject.Parent is not null )
			pickup = tr.GameObject.Parent.GetComponent<WeaponPickup>();
		if ( pickup is null ) return;

		// Find slot — prefer item's preferred slot, fall back to first empty
		var slot = pickup.PreferredSlot;
		if ( slot < 0 || slot >= SlotCount || Inventory[slot] is not null )
		{
			slot = -1;
			for ( int i = 0; i < SlotCount; i++ )
			{
				if ( Inventory[i] is null ) { slot = i; break; }
			}
		}
		if ( slot < 0 ) return; // No empty slot

		// Store in inventory
		Inventory[slot] = pickup;
		pickup.GameObject.Enabled = false;

		// Set ammo for ranged weapons
		if ( pickup.Type == WeaponPickup.WeaponType.Ranged )
		{
			CurrentAmmo = MaxAmmo;
			ReserveAmmo = MaxReserve;
		}

		// Switch to the picked up slot
		var prevSlot = SelectedSlot;
		SelectedSlot = slot;
		if ( prevSlot != slot )
			DestroyViewModel();
		CreateViewModel( pickup );

		Log.Info( $"Picked up {pickup.WeaponName} in slot {slot + 1}" );
	}

	public void DropCurrent()
	{
		if ( HeldWeapon is null ) return;

		var weapon = HeldWeapon;
		Inventory[SelectedSlot] = null;

		DestroyViewModel();
		IsReloading = false;

		weapon.GameObject.Enabled = true;
		weapon.GameObject.WorldPosition = CameraObject.WorldPosition + CameraObject.WorldRotation.Forward * 60f;

		var rb = weapon.GameObject.GetComponent<Rigidbody>();
		if ( rb is not null )
			rb.Velocity = CameraObject.WorldRotation.Forward * 200f;

		Log.Info( $"Dropped {weapon.WeaponName}" );
	}

	// ─── ViewModel ───

	private void CreateViewModel( WeaponPickup weapon )
	{
		DestroyViewModel();

		_currentBob = Vector3.Zero;
		_currentSway = Vector3.Zero;
		_currentRecoil = 0f;
		_currentTilt = 0f;
		_bobTimer = 0f;

		var model = Model.Load( weapon.WorldModelPath );
		_viewModel = new GameObject( true, "ViewModel" );
		_viewModel.SetParent( CameraObject );

		_meshChild = new GameObject( true, "ViewModelMesh" );
		_meshChild.SetParent( _viewModel );
		_meshChild.LocalPosition = -weapon.MeshOriginOffset;

		var renderer = _meshChild.Components.Create<ModelRenderer>();
		renderer.Model = model;
		renderer.RenderType = Sandbox.ModelRenderer.ShadowRenderType.Off;

		var cam = CameraObject.GetComponent<CameraComponent>();
		if ( cam is not null )
		{
			_originalZNear = cam.ZNear;
			cam.ZNear = 0.5f;
		}
	}

	private void DestroyViewModel()
	{
		if ( _viewModel is not null && _viewModel.IsValid )
		{
			_viewModel.Destroy();
			_viewModel = null;
			_meshChild = null;
		}

		var cam = CameraObject?.GetComponent<CameraComponent>();
		if ( cam is not null )
			cam.ZNear = _originalZNear;
	}

	// ─── Motion ───

	private void UpdateWeaponMotion()
	{
		var dt = Time.Delta;
		var velocity = _playerController is not null ? _playerController.Velocity : Vector3.Zero;
		var isGrounded = _playerController is not null && _playerController.IsOnGround;
		var speed = velocity.WithZ( 0 ).Length;

		// View bob
		if ( isGrounded && speed > 10f )
		{
			var bobScale = MathF.Min( speed / 300f, 1f );
			_bobTimer += dt * BobFrequency * bobScale;
			_currentBob.x = MathF.Sin( _bobTimer ) * BobAmountX * bobScale;
			_currentBob.y = MathF.Cos( _bobTimer * 2f ) * BobAmountY * bobScale;
		}
		else
		{
			_currentBob = Vector3.Lerp( _currentBob, Vector3.Zero, dt * 6f );
			_bobTimer = 0f;
		}

		// Mouse sway
		var mouseDelta = Input.MouseDelta;
		var targetSway = new Vector3( 0f, -mouseDelta.x * SwayAmount, mouseDelta.y * SwayAmount );
		_currentSway = Vector3.Lerp( _currentSway, targetSway, dt * SwaySmooth );

		// Strafe tilt
		var localVel = CameraObject.WorldRotation.Inverse * velocity;
		var targetTilt = -(localVel.y / 300f) * TiltAmount;
		_currentTilt = MathX.Lerp( _currentTilt, targetTilt, dt * 6f );

		// Recoil recovery
		_currentRecoil = MathX.Lerp( _currentRecoil, 0f, dt * RecoilRecovery );
	}

	private void PositionViewModel()
	{
		var finalPos = HoldPosition;
		finalPos.y += _currentBob.x + _currentSway.y;
		finalPos.z += _currentBob.y + _currentSway.z;
		finalPos.x -= _currentRecoil;

		_viewModel.LocalPosition = finalPos;

		var modelRot = new Angles( HeldWeapon.ModelRotation.x, HeldWeapon.ModelRotation.y, HeldWeapon.ModelRotation.z );
		var recoilAngles = new Angles( -_currentRecoil * 2f, 0f, _currentTilt );
		_viewModel.LocalRotation = (modelRot + HoldAngles + recoilAngles).ToRotation();

		if ( _meshChild is not null && _meshChild.IsValid )
			_meshChild.LocalPosition = -HeldWeapon.MeshOriginOffset;
	}

	// ─── Attack ───

	private void HandleAttack()
	{
		if ( IsReloading ) return;

		if ( HeldWeapon.Type == WeaponPickup.WeaponType.Ranged )
		{
			if ( Time.Now - _lastFireTime >= FireRate && CurrentAmmo > 0 )
				Fire();
			else if ( CurrentAmmo <= 0 && ReserveAmmo > 0 )
				StartReload();
		}
		else if ( HeldWeapon.Type == WeaponPickup.WeaponType.Hitscan )
		{
			if ( Time.Now - _lastFireTime >= FireRate && CurrentAmmo > 0 )
				FireHitscan();
			else if ( CurrentAmmo <= 0 && ReserveAmmo > 0 )
				StartReload();
		}
		else if ( HeldWeapon.Type == WeaponPickup.WeaponType.Melee )
		{
			if ( Time.Now - _lastMeleeTime >= MeleeRate )
				MeleeSwing();
		}
	}

	private void Fire()
	{
		_lastFireTime = Time.Now;
		_currentRecoil += RecoilKick;
		CurrentAmmo--;

		Sound.Play( "sounds/laser_shot.sound", CameraObject.WorldPosition );

		var camPos = CameraObject.WorldPosition;
		var camRot = CameraObject.WorldRotation;

		var crosshairEnd = camPos + camRot.Forward * 10000f;
		var aimTrace = Scene.Trace
			.Ray( camPos, crosshairEnd )
			.WithoutTags( "player", "projectile" )
			.HitTriggers()
			.Run();

		var targetPoint = aimTrace.Hit ? aimTrace.HitPosition : crosshairEnd;

		var barrelPos = camPos
			+ camRot.Forward * BarrelOffset.x
			+ camRot.Right * BarrelOffset.y
			+ camRot.Up * BarrelOffset.z;

		var direction = (targetPoint - barrelPos).Normal;

		var bolt = new GameObject( true, "LaserBolt" );
		bolt.Tags.Add( "projectile" );
		bolt.WorldPosition = barrelPos;
		bolt.WorldRotation = Rotation.LookAt( direction );

		var renderer = bolt.Components.Create<ModelRenderer>();
		renderer.Model = Model.Load( "models/dev/box.vmdl" );
		renderer.Tint = new Color( 0.3f, 0.6f, 1.0f );
		bolt.LocalScale = new Vector3( 0.5f, 0.1f, 0.1f );

		var projectile = bolt.Components.Create<LaserProjectile>();
		projectile.SetDirection( direction );
	}

	private void FireHitscan()
	{
		_lastFireTime = Time.Now;
		_currentRecoil += RecoilKick * 0.6f;
		CurrentAmmo--;

		Sound.Play( "sounds/laser_shot.sound", CameraObject.WorldPosition );

		var camPos = CameraObject.WorldPosition;
		var camRot = CameraObject.WorldRotation;

		// Barrel position
		var barrelPos = camPos
			+ camRot.Forward * BarrelOffset.x
			+ camRot.Right * BarrelOffset.y
			+ camRot.Up * BarrelOffset.z;

		// Hitscan trace from camera center
		var hitEnd = camPos + camRot.Forward * 10000f;
		var tr = Scene.Trace
			.Ray( camPos, hitEnd )
			.WithoutTags( "player", "projectile" )
			.HitTriggers()
			.Run();

		var endPoint = tr.Hit ? tr.HitPosition : hitEnd;

		// Damage
		if ( tr.Hit && tr.GameObject is not null )
		{
			var health = tr.GameObject.GetComponent<ZombieHealth>();
			if ( health is null && tr.GameObject.Parent is not null )
				health = tr.GameObject.Parent.GetComponent<ZombieHealth>();

			if ( health is not null )
				health.TakeDamage( HeldWeapon.HitscanDamage );
		}

		// Tracer line from barrel to hit
		SpawnTracer( barrelPos, endPoint );

		// Impact spark at hit point
		if ( tr.Hit )
			SpawnHitscanImpact( tr.HitPosition );
	}

	private void SpawnTracer( Vector3 start, Vector3 end )
	{
		try
		{
			var tracerObj = new GameObject( true, "Tracer" );
			tracerObj.Tags.Add( "projectile" );
			tracerObj.WorldPosition = start;

			// Thin stretched box from start to end
			var dir = (end - start);
			var dist = dir.Length;
			dir = dir.Normal;

			tracerObj.WorldRotation = Rotation.LookAt( dir );
			tracerObj.LocalScale = new Vector3( dist, 0.3f, 0.3f );

			var renderer = tracerObj.Components.Create<ModelRenderer>();
			renderer.Model = Model.Load( "models/dev/box.vmdl" );
			renderer.Tint = new Color( 1f, 0.9f, 0.5f, 0.8f );
			renderer.RenderType = Sandbox.ModelRenderer.ShadowRenderType.Off;

			// Flash light at barrel
			var light = tracerObj.Components.Create<PointLight>();
			light.LightColor = new Color( 1f, 0.8f, 0.4f );
			light.Radius = 120f;

			// Auto-destroy
			var flash = tracerObj.Components.Create<ImpactFlash>();
			flash.Duration = 0.08f;
			flash.SparkCount = 0;
		}
		catch { }
	}

	private void SpawnHitscanImpact( Vector3 pos )
	{
		try
		{
			var vfx = new GameObject( true, "HitscanImpact" );
			vfx.WorldPosition = pos;

			var light = vfx.Components.Create<PointLight>();
			light.LightColor = new Color( 1f, 0.7f, 0.3f );
			light.Radius = 100f;

			var flash = vfx.Components.Create<ImpactFlash>();
			flash.SparkCount = 5;
			flash.SparkSpeed = 200f;
		}
		catch { }
	}

	private void MeleeSwing()
	{
		_lastMeleeTime = Time.Now;
		_swingTime = Time.Now;
		_swingHasHit = false;

		// Delay the hit detection slightly into the swing arc
		// The trace happens at the peak of the swing in UpdateSwingAnimation
	}

	private void UpdateSwingAnimation()
	{
		if ( _swingTime < 0f ) return;

		var elapsed = Time.Now - _swingTime;
		var t = elapsed / SwingDuration;

		if ( t >= 1f )
		{
			_swingTime = -1f;
			return;
		}

		// Swing arc: wind up, swing through, follow through
		// t: 0 = start, 0.3 = wound back, 0.5 = impact, 1.0 = follow through
		float swingPitch;
		float swingYaw;
		float swingForward;

		if ( t < 0.2f )
		{
			// Wind up — pull back and to the right
			var wind = t / 0.2f;
			swingPitch = -20f * wind;
			swingYaw = 15f * wind;
			swingForward = -3f * wind;
		}
		else if ( t < 0.5f )
		{
			// Swing through — fast arc from right to left
			var swing = (t - 0.2f) / 0.3f;
			swingPitch = -20f + 60f * swing;
			swingYaw = 15f - 40f * swing;
			swingForward = -3f + 6f * swing;
		}
		else
		{
			// Follow through — decelerate
			var follow = (t - 0.5f) / 0.5f;
			swingPitch = 40f * (1f - follow);
			swingYaw = -25f * (1f - follow);
			swingForward = 3f * (1f - follow);
		}

		// Apply swing on top of normal position
		_currentRecoil -= swingForward * 0.3f;

		if ( _viewModel is not null && _viewModel.IsValid )
		{
			var basePos = _viewModel.LocalPosition;
			_viewModel.LocalPosition = basePos + new Vector3( swingForward, swingYaw * 0.1f, swingPitch * 0.05f );

			var modelRot = new Angles( HeldWeapon.ModelRotation.x, HeldWeapon.ModelRotation.y, HeldWeapon.ModelRotation.z );
			var swingAngles = new Angles( swingPitch, swingYaw, swingYaw * 0.5f );
			var recoilAngles = new Angles( -_currentRecoil * 2f, 0f, _currentTilt );
			_viewModel.LocalRotation = (modelRot + HoldAngles + recoilAngles + swingAngles).ToRotation();
		}

		// Hit detection at the peak of the swing
		if ( t >= 0.35f && t <= 0.55f && !_swingHasHit )
		{
			_swingHasHit = true;
			DoMeleeHit();
		}
	}

	private void DoMeleeHit()
	{
		var camPos = CameraObject.WorldPosition;
		var camRot = CameraObject.WorldRotation;
		var reach = HeldWeapon.MeleeRange;

		var tr = Scene.Trace
			.Ray( camPos, camPos + camRot.Forward * reach )
			.WithoutTags( "player" )
			.HitTriggers()
			.Run();

		if ( tr.Hit && tr.GameObject is not null )
		{
			var health = tr.GameObject.GetComponent<ZombieHealth>();
			if ( health is null && tr.GameObject.Parent is not null )
				health = tr.GameObject.Parent.GetComponent<ZombieHealth>();

			if ( health is not null )
			{
				health.TakeDamage( HeldWeapon.MeleeDamage );
				Log.Info( $"Melee hit {tr.GameObject.Name} for {HeldWeapon.MeleeDamage} damage!" );
			}

			SpawnMeleeImpact( tr.HitPosition );
		}
	}

	private void SpawnMeleeImpact( Vector3 pos )
	{
		try
		{
			var vfx = new GameObject( true, "MeleeImpact" );
			vfx.WorldPosition = pos;

			var light = vfx.Components.Create<PointLight>();
			light.LightColor = new Color( 1f, 0.7f, 0.3f );
			light.Radius = 100f;

			vfx.Components.Create<ImpactFlash>();
		}
		catch { }
	}

	// ─── Reload ───

	private void StartReload()
	{
		if ( HeldWeapon?.Type != WeaponPickup.WeaponType.Ranged ) return;
		IsReloading = true;
		_reloadStartTime = Time.Now;
		ReloadProgress = 0f;
	}

	private void UpdateReload()
	{
		if ( !IsReloading ) return;

		ReloadProgress = MathX.Clamp( (Time.Now - _reloadStartTime) / ReloadTime, 0f, 1f );

		if ( ReloadProgress >= 1f )
		{
			var needed = MaxAmmo - CurrentAmmo;
			var available = Math.Min( needed, ReserveAmmo );
			CurrentAmmo += available;
			ReserveAmmo -= available;
			IsReloading = false;
			ReloadProgress = 0f;
		}
	}

	// Called by HUD
	public bool HasItemInSlot( int slot )
	{
		return slot >= 0 && slot < SlotCount && Inventory[slot] is not null;
	}

	public bool IsSlotMelee( int slot )
	{
		return HasItemInSlot( slot ) && Inventory[slot].Type == WeaponPickup.WeaponType.Melee;
	}
}
