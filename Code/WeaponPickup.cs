/// <summary>
/// Marks an object as a pickupable weapon. Attach to any weapon prop.
/// </summary>
public sealed class WeaponPickup : Component
{
	[Property] public string WeaponName { get; set; } = "Weapon";
	[Property] public string WorldModelPath { get; set; } = "scenes/lasergun.vmdl";

	/// <summary>
	/// The model mesh origin offset — baked into the model file.
	/// </summary>
	[Property] public Vector3 MeshOriginOffset { get; set; } = new Vector3( -255f, -131f, 4.6f );

	/// <summary>
	/// Extra rotation applied to the viewmodel (pitch, yaw, roll).
	/// Stored as Vector3 so it can be set in the inspector and via MCP.
	/// X = pitch, Y = yaw, Z = roll.
	/// </summary>
	[Property] public Vector3 ModelRotation { get; set; } = new Vector3( 17.8f, -17f, -4f );
}
