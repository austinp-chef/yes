using System;
using System.Collections.Generic;

/// <summary>
/// Laser impact effect. Spawns multiple small sparks that fly outward
/// from the hit point, plus a brief bright flash. Self-destructs.
/// </summary>
public sealed class ImpactFlash : Component
{
	[Property] public float Duration { get; set; } = 0.4f;
	[Property] public int SparkCount { get; set; } = 8;
	[Property] public float SparkSpeed { get; set; } = 300f;

	private float _spawnTime;
	private List<GameObject> _sparks = new();
	private List<Vector3> _sparkVelocities = new();

	protected override void OnStart()
	{
		_spawnTime = Time.Now;

		var pos = GameObject.WorldPosition;
		var rng = new Random();

		// Spawn small spark boxes flying outward
		for ( int i = 0; i < SparkCount; i++ )
		{
			var spark = new GameObject( true, "Spark" );
			spark.WorldPosition = pos;
			spark.LocalScale = new Vector3( 0.15f, 0.15f, 0.15f );

			var renderer = spark.Components.Create<ModelRenderer>();
			renderer.Model = Model.Load( "models/dev/box.vmdl" );

			// Vary the blue color slightly per spark
			var b = 0.7f + (float)rng.NextDouble() * 0.3f;
			renderer.Tint = new Color( 0.2f, 0.4f * b, b, 1f );
			renderer.RenderType = Sandbox.ModelRenderer.ShadowRenderType.Off;

			// Random outward direction
			var dir = new Vector3(
				(float)rng.NextDouble() * 2f - 1f,
				(float)rng.NextDouble() * 2f - 1f,
				(float)rng.NextDouble() * 1.5f + 0.3f
			).Normal;

			var speed = SparkSpeed * (0.5f + (float)rng.NextDouble() * 0.5f);

			_sparks.Add( spark );
			_sparkVelocities.Add( dir * speed );
		}
	}

	protected override void OnUpdate()
	{
		var elapsed = Time.Now - _spawnTime;
		var t = elapsed / Duration;

		if ( t >= 1f )
		{
			// Cleanup all sparks
			foreach ( var spark in _sparks )
			{
				if ( spark.IsValid )
					spark.Destroy();
			}
			GameObject.Destroy();
			return;
		}

		var fade = 1f - t;
		var dt = Time.Delta;

		// Move sparks outward, shrink as they fade
		for ( int i = 0; i < _sparks.Count; i++ )
		{
			var spark = _sparks[i];
			if ( spark is null || !spark.IsValid )
				continue;

			// Move with gravity
			_sparkVelocities[i] += Vector3.Down * 400f * dt;
			spark.WorldPosition += _sparkVelocities[i] * dt;

			// Shrink
			var scale = 0.15f * fade;
			spark.LocalScale = new Vector3( scale, scale, scale );
		}

		// Dim the main flash light
		var light = GameObject.GetComponent<PointLight>();
		if ( light is not null )
			light.Radius = 200f * fade;
	}
}
