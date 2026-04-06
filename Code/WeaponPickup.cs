/// <summary>
/// Marks an object as a pickupable item. Attach to any prop.
/// Supports both ranged (laser gun) and melee (crowbar) weapons.
/// </summary>
public sealed class WeaponPickup : Component
{
	public enum WeaponType { Ranged, Melee, Hitscan }

	[Property] public string WeaponName { get; set; } = "Weapon";
	[Property] public string WorldModelPath { get; set; } = "scenes/lasergun.vmdl";
	[Property] public WeaponType Type { get; set; } = WeaponType.Ranged;

	/// <summary>
	/// The model mesh origin offset — used to center the viewmodel.
	/// </summary>
	[Property] public Vector3 MeshOriginOffset { get; set; } = new Vector3( 0f, 0f, 0f );

	/// <summary>
	/// Rotation applied to the viewmodel (pitch, yaw, roll as Vector3).
	/// </summary>
	[Property] public Vector3 ModelRotation { get; set; } = new Vector3( 0f, 0f, 0f );

	/// <summary>
	/// Melee damage per swing.
	/// </summary>
	[Property] public float MeleeDamage { get; set; } = 100f;

	/// <summary>
	/// Melee reach distance.
	/// </summary>
	[Property] public float MeleeRange { get; set; } = 80f;

	/// <summary>
	/// Hitscan damage per shot.
	/// </summary>
	[Property] public float HitscanDamage { get; set; } = 12f;

	/// <summary>
	/// Which hotbar slot this item prefers (0-4).
	/// </summary>
	[Property] public int PreferredSlot { get; set; } = 0;
}
