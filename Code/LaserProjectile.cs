using System;

/// <summary>
/// A laser bolt that flies forward and destroys on hit or timeout.
/// Blue bolt with point light glow and trail.
/// Spawns a blue particle explosion on impact and damages zombies.
/// </summary>
public sealed class LaserProjectile : Component
{
	[Property] public float Speed { get; set; } = 4000f;
	[Property] public float MaxLifetime { get; set; } = 2f;
	[Property] public float Damage { get; set; } = 25f;

	private float _spawnTime;
	private Vector3 _direction;

	public void SetDirection( Vector3 dir )
	{
		_direction = dir.Normal;
	}

	protected override void OnStart()
	{
		_spawnTime = Time.Now;

		try
		{
			var renderer = GameObject.GetComponent<ModelRenderer>();
			if ( renderer is not null )
				renderer.Tint = new Color( 0.3f, 0.6f, 1.0f );

			var light = GameObject.Components.Create<PointLight>();
			light.LightColor = new Color( 0.3f, 0.6f, 1.0f );
			light.Radius = 200f;

			var trail = GameObject.Components.Create<TrailRenderer>();
			trail.LifeTime = 0.08f;
			trail.CastShadows = false;
			trail.Opaque = false;

			trail.Width = new Curve(
				new Curve.Frame( 0f, 2f ),
				new Curve.Frame( 0.5f, 1.2f ),
				new Curve.Frame( 1f, 0f )
			);

			trail.Color = new Gradient(
				new Gradient.ColorFrame( 0f, new Color( 0.4f, 0.7f, 1.0f, 1.0f ) ),
				new Gradient.ColorFrame( 0.3f, new Color( 0.3f, 0.5f, 1.0f, 0.6f ) ),
				new Gradient.ColorFrame( 1f, new Color( 0.2f, 0.3f, 0.8f, 0.0f ) )
			);
		}
		catch ( Exception e )
		{
			Log.Error( $"Projectile VFX error: {e.Message}" );
		}
	}

	protected override void OnUpdate()
	{
		if ( Time.Now - _spawnTime > MaxLifetime )
		{
			GameObject.Destroy();
			return;
		}

		var moveDistance = Speed * Time.Delta;
		var startPos = GameObject.WorldPosition;
		var endPos = startPos + _direction * moveDistance;

		var tr = Scene.Trace
			.Ray( startPos, endPos )
			.WithoutTags( "player", "projectile" )
			.HitTriggers()
			.Run();

		if ( tr.Hit )
		{
			OnHit( tr.HitPosition, tr.Normal, tr.GameObject );
			return;
		}

		GameObject.WorldPosition = endPos;
	}

	private void OnHit( Vector3 hitPos, Vector3 hitNormal, GameObject hitObject )
	{
		// Damage zombie if hit
		if ( hitObject is not null )
		{
			var health = hitObject.GetComponent<ZombieHealth>();
			if ( health is null && hitObject.Parent is not null )
				health = hitObject.Parent.GetComponent<ZombieHealth>();

			if ( health is not null )
				health.TakeDamage( Damage );
		}

		// Spawn blue impact particle burst
		SpawnImpactVFX( hitPos, hitNormal );

		// Destroy the bolt
		GameObject.WorldPosition = hitPos;
		GameObject.Destroy();
	}

	private void SpawnImpactVFX( Vector3 pos, Vector3 normal )
	{
		try
		{
			var vfx = new GameObject( true, "LaserImpact" );
			vfx.WorldPosition = pos;

			// Blue flash light
			var light = vfx.Components.Create<PointLight>();
			light.LightColor = new Color( 0.3f, 0.6f, 1.0f );
			light.Radius = 200f;

			// Spark shower — ImpactFlash spawns and animates the sparks
			vfx.Components.Create<ImpactFlash>();
		}
		catch ( Exception e )
		{
			Log.Error( $"Impact VFX error: {e.Message}" );
		}
	}
}
