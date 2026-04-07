using System;

/// <summary>
/// Futuristic cyan HUD with:
/// - Center crosshair
/// - Bottom-center inventory hotbar (slots 1-5)
/// - Bottom-right ammo display (when holding weapon)
/// - Reload bar
/// </summary>
public sealed class WeaponHud : Component
{
	// Hotbar config
	private const int SlotCount = 5;
	private const float SlotSize = 60f;
	private const float SlotGap = 6f;
	private const float SlotRadius = 4f;

	private WeaponHolder _holder;

	protected override void OnStart()
	{
		_holder = GameObject.GetComponent<WeaponHolder>();
	}

	protected override void OnUpdate()
	{
		if ( Scene.Camera is null )
			return;

		if ( _holder is null )
			return;

		// Only draw HUD elements when we have at least one item
		var hasAnyItem = false;
		for ( int i = 0; i < WeaponHolder.SlotCount; i++ )
		{
			if ( _holder.HasItemInSlot( i ) ) { hasAnyItem = true; break; }
		}

		var hud = Scene.Camera.Hud;
		var sw = Screen.Width;
		var sh = Screen.Height;

		if ( hasAnyItem )
			DrawHotbar( hud, sw, sh );

		if ( _holder.HeldWeapon is not null )
		{
			DrawCrosshair( hud, sw, sh );

			if ( _holder.HeldWeapon.Type != WeaponPickup.WeaponType.Melee && _holder.HeldWeapon.Type != WeaponPickup.WeaponType.Throwable )
			{
				DrawAmmoDisplay( hud, sw, sh );

				if ( _holder.IsReloading )
					DrawReloadBar( hud, sw, sh );
			}
		}
	}

	// ─── Hotbar ───

	private void DrawHotbar( Sandbox.Rendering.HudPainter hud, float sw, float sh )
	{
		var cyan = new Color( 0f, 0.9f, 1f );
		var cyanDim = new Color( 0f, 0.4f, 0.5f );
		var dark = new Color( 0.04f, 0.1f, 0.16f, 0.7f );
		var darkSelected = new Color( 0.06f, 0.15f, 0.22f, 0.85f );

		var totalW = SlotCount * SlotSize + (SlotCount - 1) * SlotGap;
		var startX = sw * 0.5f - totalW * 0.5f;
		var startY = sh - SlotSize - 20f;

		for ( int i = 0; i < SlotCount; i++ )
		{
			var sx = startX + i * (SlotSize + SlotGap);
			var sy = startY;
			var isSelected = _holder is not null && i == _holder.SelectedSlot;

			// Slot background
			hud.DrawRect( new Rect( sx, sy, SlotSize, SlotSize ), isSelected ? darkSelected : dark );

			// Slot border
			var borderColor = isSelected ? cyan : cyanDim;
			var borderThickness = isSelected ? 2f : 1f;
			DrawBorder( hud, sx, sy, SlotSize, SlotSize, borderColor, borderThickness );

			// Selection glow — extra inner border
			if ( isSelected )
			{
				var glowColor = new Color( 0f, 0.9f, 1f, 0.15f );
				hud.DrawRect( new Rect( sx + 2f, sy + 2f, SlotSize - 4f, SlotSize - 4f ), glowColor );
			}

			// Slot number indicator — small box in top-left corner
			var numSize = 12f;
			var numColor = isSelected ? cyan : new Color( 0f, 0.5f, 0.6f, 0.6f );
			hud.DrawRect( new Rect( sx + 3f, sy + 3f, numSize, numSize ), new Color( 0f, 0f, 0f, 0.4f ) );
			DrawBorder( hud, sx + 3f, sy + 3f, numSize, numSize, numColor, 1f );

			// Small dot inside the number box to indicate slot number
			var dotCount = i + 1;
			if ( dotCount <= 3 )
			{
				for ( int d = 0; d < dotCount; d++ )
				{
					var dotX = sx + 5f + d * 4f;
					hud.DrawRect( new Rect( dotX, sy + 7f, 2f, 2f ), numColor );
				}
			}

			// Item icon based on what's in the slot
			if ( _holder is not null && _holder.HasItemInSlot( i ) )
			{
				var iconColor = isSelected ? cyan : cyanDim;
				var itemType = _holder.Inventory[i].Type;

				if ( itemType == WeaponPickup.WeaponType.Melee )
					DrawCrowbarIcon( hud, sx + 14f, sy + 12f, iconColor );
				else if ( itemType == WeaponPickup.WeaponType.Throwable )
					DrawGrenadeIcon( hud, sx + 16f, sy + 14f, iconColor );
				else
					DrawGunIcon( hud, sx + 12f, sy + 16f, iconColor );
			}
		}
	}

	private void DrawGunIcon( Sandbox.Rendering.HudPainter hud, float x, float y, Color c )
	{
		// Simple gun silhouette using rectangles
		// Barrel
		hud.DrawRect( new Rect( x + 10f, y + 6f, 24f, 6f ), c );
		// Body
		hud.DrawRect( new Rect( x, y + 4f, 22f, 12f ), c );
		// Grip
		hud.DrawRect( new Rect( x + 4f, y + 14f, 8f, 14f ), c );
		// Trigger guard
		hud.DrawRect( new Rect( x + 12f, y + 14f, 8f, 2f ), c );
		hud.DrawRect( new Rect( x + 18f, y + 14f, 2f, 8f ), c );
	}

	private void DrawGrenadeIcon( Sandbox.Rendering.HudPainter hud, float x, float y, Color c )
	{
		// Simple grenade shape — circle body + pin
		hud.DrawRect( new Rect( x + 4f, y + 2f, 20f, 24f ), c );
		hud.DrawRect( new Rect( x + 6f, y, 16f, 2f ), c );
		hud.DrawRect( new Rect( x + 8f, y - 4f, 12f, 4f ), c );
		// Pin
		hud.DrawRect( new Rect( x + 18f, y - 6f, 6f, 2f ), c );
		hud.DrawRect( new Rect( x + 22f, y - 6f, 2f, 6f ), c );
	}

	private void DrawCrowbarIcon( Sandbox.Rendering.HudPainter hud, float x, float y, Color c )
	{
		// Simple crowbar shape — long bar with a hook
		// Shaft
		hud.DrawRect( new Rect( x + 4f, y, 4f, 36f ), c );
		// Hook top
		hud.DrawRect( new Rect( x, y, 16f, 4f ), c );
		hud.DrawRect( new Rect( x, y + 4f, 4f, 8f ), c );
		// Flat bottom
		hud.DrawRect( new Rect( x + 2f, y + 32f, 10f, 4f ), c );
	}

	// ─── Crosshair ───

	private void DrawCrosshair( Sandbox.Rendering.HudPainter hud, float sw, float sh )
	{
		var cx = sw * 0.5f;
		var cy = sh * 0.5f;
		var len = 12f;
		var gap = 5f;
		var t = 2f;
		var c = new Color( 0f, 0.9f, 1f, 0.7f );

		hud.DrawRect( new Rect( cx - t * 0.5f, cy - len - gap, t, len ), c );
		hud.DrawRect( new Rect( cx - t * 0.5f, cy + gap, t, len ), c );
		hud.DrawRect( new Rect( cx - len - gap, cy - t * 0.5f, len, t ), c );
		hud.DrawRect( new Rect( cx + gap, cy - t * 0.5f, len, t ), c );
		hud.DrawRect( new Rect( cx - 1f, cy - 1f, 2f, 2f ), new Color( 0f, 0.9f, 1f, 0.4f ) );
	}

	// ─── Ammo Display ───

	private void DrawAmmoDisplay( Sandbox.Rendering.HudPainter hud, float sw, float sh )
	{
		var cyan = new Color( 0f, 0.9f, 1f );
		var cyanDim = new Color( 0f, 0.4f, 0.5f );
		var red = new Color( 1f, 0.2f, 0.2f );
		var dark = new Color( 0.04f, 0.1f, 0.16f, 0.75f );
		var isLow = _holder.CurrentAmmo <= 5;
		var accent = isLow ? red : cyan;
		var border = isLow ? red : cyanDim;

		var px = sw - 260f;
		var py = sh - SlotSize - 110f;

		hud.DrawRect( new Rect( px, py, 220f, 70f ), dark );
		DrawBorder( hud, px, py, 220f, 70f, border, 1f );

		// Magazine bar
		var magBarX = px + 12f;
		var magBarY = py + 10f;
		var magBarW = 196f;
		var magBarH = 24f;
		var magFill = _holder.MaxAmmo > 0 ? (float)_holder.CurrentAmmo / _holder.MaxAmmo : 0f;

		hud.DrawRect( new Rect( magBarX, magBarY, magBarW, magBarH ), new Color( 0.02f, 0.06f, 0.1f, 0.8f ) );
		hud.DrawRect( new Rect( magBarX, magBarY, magBarW * magFill, magBarH ), accent );
		DrawBorder( hud, magBarX, magBarY, magBarW, magBarH, border, 1f );

		var tickCount = Math.Min( _holder.MaxAmmo, 30 );
		var tickSpacing = magBarW / tickCount;
		for ( int i = 1; i < tickCount; i++ )
		{
			var tx = magBarX + i * tickSpacing;
			hud.DrawRect( new Rect( tx, magBarY, 1f, magBarH ), new Color( 0f, 0f, 0f, 0.3f ) );
		}

		// Reserve bar
		var resBarY = py + 42f;
		var resFill = _holder.MaxReserve > 0 ? (float)_holder.ReserveAmmo / _holder.MaxReserve : 0f;
		hud.DrawRect( new Rect( magBarX, resBarY, magBarW, 8f ), new Color( 0.02f, 0.06f, 0.1f, 0.8f ) );
		hud.DrawRect( new Rect( magBarX, resBarY, magBarW * resFill, 8f ), cyanDim );
		DrawBorder( hud, magBarX, resBarY, magBarW, 8f, new Color( 0f, 0.3f, 0.4f, 0.5f ), 1f );

		// Ammo dots
		var dotY = py + 56f;
		var dotsToShow = Math.Min( _holder.CurrentAmmo, 30 );
		for ( int i = 0; i < dotsToShow; i++ )
		{
			var dx = px + 12f + i * 6f;
			hud.DrawRect( new Rect( dx, dotY, 4f, 4f ), accent );
		}
	}

	// ─── Reload Bar ───

	private void DrawReloadBar( Sandbox.Rendering.HudPainter hud, float sw, float sh )
	{
		var cyan = new Color( 0f, 0.9f, 1f );
		var cyanDim = new Color( 0f, 0.4f, 0.5f );

		var bw = 200f;
		var bh = 8f;
		var bx = sw * 0.5f - bw * 0.5f;
		var by = sh * 0.5f + 50f;

		hud.DrawRect( new Rect( bx - 2f, by - 2f, bw + 4f, bh + 4f ), new Color( 0.04f, 0.1f, 0.16f, 0.85f ) );
		hud.DrawRect( new Rect( bx, by, bw * _holder.ReloadProgress, bh ), cyan );
		DrawBorder( hud, bx, by, bw, bh, cyanDim, 1f );

		var dotCount = 3;
		var phase = (Time.Now * 4f) % dotCount;
		for ( int i = 0; i < dotCount; i++ )
		{
			var alpha = (int)phase == i ? 1f : 0.3f;
			var dx = bx + bw * 0.5f - 12f + i * 10f;
			hud.DrawRect( new Rect( dx, by - 16f, 4f, 4f ), new Color( 0f, 0.9f, 1f, alpha ) );
		}
	}

	// ─── Utility ───

	private void DrawBorder( Sandbox.Rendering.HudPainter hud, float x, float y, float w, float h, Color c, float t = 1f )
	{
		hud.DrawRect( new Rect( x, y, w, t ), c );
		hud.DrawRect( new Rect( x, y + h - t, w, t ), c );
		hud.DrawRect( new Rect( x, y, t, h ), c );
		hud.DrawRect( new Rect( x + w - t, y, t, h ), c );
	}
}
