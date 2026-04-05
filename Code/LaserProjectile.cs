using System;

/// <summary>
/// A laser bolt that flies forward and destroys on hit or timeout.
/// Blue bolt with point light glow and fading trail.
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
			// Blue tint on bolt
			var renderer = GameObject.GetComponent<ModelRenderer>();
			if ( renderer is not null )
				renderer.Tint = new Color( 0.3f, 0.6f, 1.0f );

			// Blue glow light
			var light = GameObject.Components.Create<PointLight>();
			light.LightColor = new Color( 0.3f, 0.6f, 1.0f );
			light.Radius = 200f;

			// Short trail with fade
			var trail = GameObject.Components.Create<TrailRenderer>();
			trail.LifeTime = 0.08f;
			trail.CastShadows = false;

			// Width: starts at 2, tapers to 0
			trail.Width = new Curve(
				new Curve.Frame( 0f, 2f ),
				new Curve.Frame( 0.5f, 1.2f ),
				new Curve.Frame( 1f, 0f )
			);

			// Color: bright blue front, fades to transparent
			trail.Color = new Gradient(
				new Gradient.ColorFrame( 0f, new Color( 0.4f, 0.7f, 1.0f, 1.0f ) ),
				new Gradient.ColorFrame( 0.3f, new Color( 0.3f, 0.5f, 1.0f, 0.6f ) ),
				new Gradient.ColorFrame( 1f, new Color( 0.2f, 0.3f, 0.8f, 0.0f ) )
			);

			trail.Opaque = false;
		}
		catch ( System.Exception e )
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
			.Run();

		if ( tr.Hit )
		{
			GameObject.WorldPosition = tr.HitPosition;
			GameObject.Destroy();
			return;
		}

		GameObject.WorldPosition = endPos;
	}
}
