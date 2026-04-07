using System;

/// <summary>
/// Simple zombie AI. Patrols in a circle, chases player when close.
/// Drives citizen animation via raw SkinnedModelRenderer params (move_x, move_y, b_grounded).
/// This is the same approach used by fish.scc ShrimpleWalker — verified working.
/// </summary>
public sealed class ZombieAI : Component
{
	// --- Patrol ---
	[Property, Category( "Patrol" )] public float PatrolRadius { get; set; } = 100f;
	[Property, Category( "Patrol" )] public float PatrolSpeed { get; set; } = 40f;

	// --- Chase ---
	[Property, Category( "Chase" )] public float DetectRange { get; set; } = 300f;
	[Property, Category( "Chase" )] public float ChaseSpeed { get; set; } = 120f;
	[Property, Category( "Chase" )] public float LoseRange { get; set; } = 500f;

	// --- Movement ---
	[Property, Category( "Movement" )] public float RotationSpeed { get; set; } = 5f;

	private Vector3 _spawnPos;
	private float _patrolAngle;
	private bool _chasing;
	private SkinnedModelRenderer _renderer;
	private Vector3 _velocity;
	private float _smoothMoveX;
	private float _smoothMoveY;

	protected override void OnStart()
	{
		_spawnPos = GameObject.WorldPosition;
		_renderer = Components.Get<SkinnedModelRenderer>( FindMode.EverythingInSelfAndDescendants );

		// Make collider a trigger so zombies don't push the player
		var collider = GameObject.GetComponent<CapsuleCollider>();
		if ( collider is not null )
			collider.IsTrigger = true;

		// Set up zombie appearance — bonemerge zombie head onto citizen body
		SetupZombieLook();
	}

	protected override void OnFixedUpdate()
	{
		var player = FindPlayer();
		var myPos = GameObject.WorldPosition;

		// Detection logic
		if ( player is not null )
		{
			var dist = (player.WorldPosition - myPos).Length;

			if ( _chasing )
			{
				if ( dist > LoseRange )
					_chasing = false;
			}
			else
			{
				if ( dist < DetectRange )
					_chasing = true;
			}
		}

		// Store position before move
		var prevPos = myPos;

		// Move
		if ( _chasing && player is not null )
			MoveToward( player.WorldPosition, ChaseSpeed );
		else
			PatrolUpdate();

		// Calculate velocity from actual movement
		_velocity = (GameObject.WorldPosition - prevPos) / Time.Delta;
	}

	protected override void OnUpdate()
	{
		if ( _renderer is null || !_renderer.IsValid )
			return;

		// Anim params: project velocity onto local forward/right (same as ShrimpleWalker)
		var forward = _renderer.WorldRotation.Forward;
		var right = _renderer.WorldRotation.Right;

		var targetMoveX = Vector3.Dot( _velocity, forward );
		var targetMoveY = Vector3.Dot( _velocity, right );

		// Smooth lerp to avoid snapping
		_smoothMoveX = MathX.Lerp( _smoothMoveX, targetMoveX, Time.Delta * 10f );
		_smoothMoveY = MathX.Lerp( _smoothMoveY, targetMoveY, Time.Delta * 10f );

		_renderer.Set( "move_x", _smoothMoveX );
		_renderer.Set( "move_y", _smoothMoveY );
		_renderer.Set( "b_grounded", true );
	}

	private void PatrolUpdate()
	{
		_patrolAngle += Time.Delta * (PatrolSpeed / PatrolRadius);

		var targetPos = _spawnPos + new Vector3(
			MathF.Cos( _patrolAngle ) * PatrolRadius,
			MathF.Sin( _patrolAngle ) * PatrolRadius,
			0f
		);

		MoveToward( targetPos, PatrolSpeed );
	}

	private void MoveToward( Vector3 target, float speed )
	{
		var myPos = GameObject.WorldPosition;
		var dir = (target - myPos).WithZ( 0 );

		if ( dir.Length < 1f )
			return;

		dir = dir.Normal;

		GameObject.WorldPosition = myPos + dir * speed * Time.Delta;

		var targetRot = Rotation.LookAt( dir, Vector3.Up );
		GameObject.WorldRotation = Rotation.Lerp( GameObject.WorldRotation, targetRot, Time.Delta * RotationSpeed );
	}

	private GameObject FindPlayer()
	{
		foreach ( var obj in Scene.GetAllObjects( true ) )
		{
			if ( obj.Tags.Contains( "player" ) )
				return obj;
		}
		return null;
	}

	private void SetupZombieLook()
	{
		if ( _renderer is null ) return;

		// Green zombie tint
		_renderer.Tint = new Color( 0.45f, 0.65f, 0.35f );
	}
}
