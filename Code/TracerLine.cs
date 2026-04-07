/// <summary>
/// A quick line tracer that fades out and self-destructs.
/// Draws a line from start to end using a LineRenderer.
/// </summary>
public sealed class TracerLine : Component
{
	[Property] public float Duration { get; set; } = 0.1f;

	private float _spawnTime;
	private PointLight _light;

	public Vector3 StartPos { get; set; }
	public Vector3 EndPos { get; set; }

	protected override void OnStart()
	{
		_spawnTime = Time.Now;

		try
		{
			// Use a LineRenderer for the tracer
			var line = GameObject.Components.Create<LineRenderer>();
			line.CastShadows = false;
			line.Opaque = false;

			// Flash at the hit point
			var flashObj = new GameObject( true, "TracerFlash" );
			flashObj.WorldPosition = EndPos;
			flashObj.SetParent( GameObject );

			_light = flashObj.Components.Create<PointLight>();
			_light.LightColor = new Color( 1f, 0.8f, 0.3f );
			_light.Radius = 100f;
		}
		catch { }
	}

	protected override void OnUpdate()
	{
		var t = (Time.Now - _spawnTime) / Duration;

		if ( t >= 1f )
		{
			GameObject.Destroy();
			return;
		}

		// Fade the light
		if ( _light is not null && _light.IsValid )
			_light.Radius = 100f * (1f - t);
	}
}
