using System;

/// <summary>
/// Thrown grenade. Bounces on surfaces, explodes after fuse, kills and ragdolls zombies.
/// </summary>
public sealed class Grenade : Component
{
	public float FuseTime { get; set; } = 2.5f;
	public float ExplosionRadius { get; set; } = 250f;
	public float ExplosionForce { get; set; } = 800f;
	public float Damage { get; set; } = 200f;

	private float _spawnTime;
	private Vector3 _velocity;

	public void Launch( Vector3 vel )
	{
		_velocity = vel;
	}

	protected override void OnStart()
	{
		_spawnTime = Time.Now;
	}

	protected override void OnUpdate()
	{
		var dt = Time.Delta;

		// Gravity
		_velocity += Vector3.Down * 600f * dt;

		var startPos = GameObject.WorldPosition;
		var endPos = startPos + _velocity * dt;

		// Bounce
		var tr = Scene.Trace.Ray( startPos, endPos )
			.WithoutTags( "player", "projectile" )
			.HitTriggers()
			.Run();

		if ( tr.Hit )
		{
			var d = Vector3.Dot( _velocity, tr.Normal );
			_velocity = (_velocity - tr.Normal * (2f * d)) * 0.4f;
			GameObject.WorldPosition = tr.HitPosition + tr.Normal;
		}
		else
		{
			GameObject.WorldPosition = endPos;
		}

		// Spin
		GameObject.WorldRotation *= Rotation.FromYaw( 360f * dt );

		// Flash warning
		var remaining = FuseTime - (Time.Now - _spawnTime);
		if ( remaining < 1f )
		{
			var flash = MathF.Sin( Time.Now * 20f ) * 0.5f + 0.5f;
			var renderer = GameObject.GetComponent<ModelRenderer>();
			if ( renderer is not null )
				renderer.Tint = new Color( 0.1f + 0.9f * flash, 0.6f * (1f - flash), 0.1f );
		}

		// Explode
		if ( Time.Now - _spawnTime >= FuseTime )
			Explode();
	}

	private void Explode()
	{
		var pos = GameObject.WorldPosition;

		foreach ( var obj in Scene.GetAllObjects( true ) )
		{
			var health = obj.GetComponent<ZombieHealth>();
			if ( health is null ) continue;

			var diff = obj.WorldPosition - pos;
			if ( diff.Length > ExplosionRadius ) continue;

			var pushDir = diff.WithZ( 0 ).Normal;
			health.TakeDamage( Damage, pushDir );
		}

		// VFX
		try
		{
			var vfx = new GameObject( true, "Explosion" );
			vfx.WorldPosition = pos;

			var light = vfx.Components.Create<PointLight>();
			light.LightColor = new Color( 1f, 0.6f, 0.2f );
			light.Radius = 500f;

			var impact = vfx.Components.Create<ImpactFlash>();
			impact.Duration = 0.5f;
			impact.SparkCount = 20;
			impact.SparkSpeed = 500f;
		}
		catch { }

		GameObject.Destroy();
	}
}
