using System;

/// <summary>
/// Full FPS weapon system. Attach to the Player Controller.
/// E to pick up / drop. Left click to fire.
///
/// Features:
/// - Smooth weapon sway following mouse movement
/// - View bob when walking/running
/// - Recoil kick + recovery on fire
/// - Weapon tilt on strafe
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

	// --- Barrel ---
	/// <summary>
	/// Barrel tip offset from camera (forward, right, down).
	/// The laser spawns here and angles toward the crosshair.
	/// </summary>
	[Property, Category( "Position" )] public Vector3 BarrelOffset { get; set; } = new Vector3( 35f, 6f, -5f );

	public WeaponPickup HeldWeapon { get; private set; }

	private float _originalZNear;
	private bool _holding;
	private GameObject _viewModel;
	private GameObject _meshChild;
	private float _lastFireTime;

	// Animation state
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
		if ( IsProxy )
			return;

		if ( Input.Pressed( "use" ) )
		{
			if ( _holding )
				Drop();
			else
				TryPickup();
		}

		if ( _holding && _viewModel is not null && _viewModel.IsValid )
		{
			UpdateWeaponMotion();
			PositionViewModel();

			if ( Input.Down( "attack1" ) && Time.Now - _lastFireTime >= FireRate )
				Fire();
		}
	}

	// ─── Motion Systems ───

	private void UpdateWeaponMotion()
	{
		var dt = Time.Delta;
		var velocity = _playerController is not null ? _playerController.Velocity : Vector3.Zero;
		var isGrounded = _playerController is not null && _playerController.IsOnGround;
		var speed = velocity.WithZ( 0 ).Length;

		// ── View Bob ──
		// Sinusoidal bob when moving on ground
		if ( isGrounded && speed > 10f )
		{
			var bobScale = MathF.Min( speed / 300f, 1f );
			_bobTimer += dt * BobFrequency * bobScale;

			_currentBob.x = MathF.Sin( _bobTimer ) * BobAmountX * bobScale;
			_currentBob.y = MathF.Cos( _bobTimer * 2f ) * BobAmountY * bobScale;
		}
		else
		{
			// Smoothly return to center when not moving
			_currentBob = Vector3.Lerp( _currentBob, Vector3.Zero, dt * 6f );
			_bobTimer = 0f;
		}

		// ── Mouse Sway ──
		// Weapon lags behind mouse movement
		var mouseDelta = Input.MouseDelta;
		var targetSway = new Vector3( 0f, -mouseDelta.x * SwayAmount, mouseDelta.y * SwayAmount );
		_currentSway = Vector3.Lerp( _currentSway, targetSway, dt * SwaySmooth );

		// ── Strafe Tilt ──
		// Slight roll when strafing
		var localVel = CameraObject.WorldRotation.Inverse * velocity;
		var targetTilt = -(localVel.y / 300f) * TiltAmount;
		_currentTilt = MathX.Lerp( _currentTilt, targetTilt, dt * 6f );

		// ── Recoil Recovery ──
		_currentRecoil = MathX.Lerp( _currentRecoil, 0f, dt * RecoilRecovery );
	}

	private void PositionViewModel()
	{
		// Pivot sits near the camera at HoldPosition — rotation is intuitive
		var finalPos = HoldPosition;
		finalPos.y += _currentBob.x + _currentSway.y;
		finalPos.z += _currentBob.y + _currentSway.z;
		finalPos.x -= _currentRecoil;

		_viewModel.LocalPosition = finalPos;

		// Rotation: model rotation + hold angles + recoil + strafe tilt
		var modelRot = new Angles( HeldWeapon.ModelRotation.x, HeldWeapon.ModelRotation.y, HeldWeapon.ModelRotation.z );
		var recoilAngles = new Angles( -_currentRecoil * 2f, 0f, _currentTilt );
		var combinedAngles = modelRot + HoldAngles + recoilAngles;
		_viewModel.LocalRotation = combinedAngles.ToRotation();

		// Update mesh child offset every frame so inspector changes apply live
		if ( _meshChild is not null && _meshChild.IsValid )
			_meshChild.LocalPosition = -HeldWeapon.MeshOriginOffset;
	}

	// ─── Pickup / Drop ───

	private void TryPickup()
	{
		if ( CameraObject is null )
			return;

		var eyePos = CameraObject.WorldPosition;
		var eyeEnd = eyePos + CameraObject.WorldRotation.Forward * PickupRange;

		var tr = Scene.Trace
			.Ray( eyePos, eyeEnd )
			.WithoutTags( "player" )
			.Run();

		if ( !tr.Hit || tr.GameObject is null )
			return;

		var pickup = tr.GameObject.GetComponent<WeaponPickup>();
		if ( pickup is null && tr.GameObject.Parent is not null )
			pickup = tr.GameObject.Parent.GetComponent<WeaponPickup>();
		if ( pickup is null )
			return;

		Pickup( pickup );
	}

	public void Pickup( WeaponPickup weapon )
	{
		if ( _holding )
			Drop();

		HeldWeapon = weapon;
		_holding = true;

		// Reset motion state
		_currentBob = Vector3.Zero;
		_currentSway = Vector3.Zero;
		_currentRecoil = 0f;
		_currentTilt = 0f;
		_bobTimer = 0f;

		// Hide world weapon
		weapon.GameObject.Enabled = false;

		// Create viewmodel with two-level hierarchy:
		// _viewModel (pivot) → meshChild (offset renderer)
		// This way rotation happens around the pivot near the camera,
		// not around the distant mesh origin
		var model = Model.Load( weapon.WorldModelPath );

		_viewModel = new GameObject( true, "ViewModel" );
		_viewModel.SetParent( CameraObject );

		_meshChild = new GameObject( true, "ViewModelMesh" );
		_meshChild.SetParent( _viewModel );

		// Push the mesh child to compensate for origin offset
		_meshChild.LocalPosition = -weapon.MeshOriginOffset;
		_meshChild.LocalRotation = Rotation.Identity;

		var renderer = _meshChild.Components.Create<ModelRenderer>();
		renderer.Model = model;
		renderer.RenderType = Sandbox.ModelRenderer.ShadowRenderType.Off;

		// Lower ZNear
		var cam = CameraObject.GetComponent<CameraComponent>();
		if ( cam is not null )
		{
			_originalZNear = cam.ZNear;
			cam.ZNear = 0.5f;
		}

		PositionViewModel();
		Log.Info( $"Picked up {weapon.WeaponName}" );
	}

	public void Drop()
	{
		if ( HeldWeapon is null )
			return;

		var weapon = HeldWeapon;
		HeldWeapon = null;
		_holding = false;

		if ( _viewModel is not null && _viewModel.IsValid )
		{
			_viewModel.Destroy();
			_viewModel = null;
		}

		var cam = CameraObject.GetComponent<CameraComponent>();
		if ( cam is not null )
			cam.ZNear = _originalZNear;

		weapon.GameObject.Enabled = true;
		weapon.GameObject.WorldPosition = CameraObject.WorldPosition + CameraObject.WorldRotation.Forward * 60f;

		var rb = weapon.GameObject.GetComponent<Rigidbody>();
		if ( rb is not null )
			rb.Velocity = CameraObject.WorldRotation.Forward * 200f;

		Log.Info( $"Dropped {weapon.WeaponName}" );
	}

	// ─── Firing ───

	private void Fire()
	{
		_lastFireTime = Time.Now;
		_currentRecoil += RecoilKick;

		// Play fire sound
		Sound.Play( "sounds/laser_shot.sound", CameraObject.WorldPosition );

		var camPos = CameraObject.WorldPosition;
		var camRot = CameraObject.WorldRotation;

		// 1. Find where the crosshair is pointing (center screen ray)
		var crosshairEnd = camPos + camRot.Forward * 10000f;
		var aimTrace = Scene.Trace
			.Ray( camPos, crosshairEnd )
			.WithoutTags( "player", "projectile" )
			.Run();

		// Target point: either the hit point or far ahead
		var targetPoint = aimTrace.Hit ? aimTrace.HitPosition : crosshairEnd;

		// 2. Barrel position in world space
		var barrelPos = camPos
			+ camRot.Forward * BarrelOffset.x
			+ camRot.Right * BarrelOffset.y
			+ camRot.Up * BarrelOffset.z;

		// 3. Direction from barrel to crosshair target (the slight angle)
		var direction = (targetPoint - barrelPos).Normal;

		// Create laser bolt
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
}
