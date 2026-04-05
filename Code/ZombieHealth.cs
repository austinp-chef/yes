/// <summary>
/// Health component for zombies. On death, ragdolls the body then destroys after a delay.
/// </summary>
public sealed class ZombieHealth : Component
{
	[Property] public float MaxHealth { get; set; } = 100f;
	[Property] public float DestroyDelay { get; set; } = 5f;

	public float CurrentHealth { get; private set; }
	public bool IsDead { get; private set; }

	private float _deathTime;

	protected override void OnStart()
	{
		CurrentHealth = MaxHealth;
	}

	protected override void OnUpdate()
	{
		if ( IsDead && Time.Now - _deathTime > DestroyDelay )
		{
			GameObject.Destroy();
		}
	}

	public void TakeDamage( float damage )
	{
		if ( IsDead )
			return;

		CurrentHealth -= damage;
		Log.Info( $"Zombie took {damage} damage, health: {CurrentHealth}/{MaxHealth}" );

		if ( CurrentHealth <= 0f )
			Die();
	}

	private void Die()
	{
		IsDead = true;
		_deathTime = Time.Now;
		Log.Info( "Zombie died! Ragdolling..." );

		// Death sound
		Sound.Play( "sounds/ambience/stings/sting-wolf-1.vsnd", GameObject.WorldPosition );

		// Disable AI so it stops moving
		var ai = GameObject.GetComponent<ZombieAI>();
		if ( ai is not null )
			ai.Enabled = false;

		// Disable the collider so it doesn't block
		var collider = GameObject.GetComponent<CapsuleCollider>();
		if ( collider is not null )
			collider.Enabled = false;

		// Add ModelPhysics to ragdoll the body
		var renderer = GameObject.GetComponent<SkinnedModelRenderer>();
		if ( renderer is not null )
		{
			var ragdoll = GameObject.Components.Create<ModelPhysics>();
			ragdoll.Model = renderer.Model;
			ragdoll.Renderer = renderer;
		}
	}
}
