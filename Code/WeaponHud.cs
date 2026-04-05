using System;

/// <summary>
/// Futuristic cyan weapon HUD using HudPainter.
/// Uses bars and shapes only — no text rendering dependency.
/// </summary>
public sealed class WeaponHud : Component
{
	private WeaponHolder _holder;

	protected override void OnStart()
	{
		_holder = GameObject.GetComponent<WeaponHolder>();
	}

	protected override void OnUpdate()
	{
		if ( _holder is null || _holder.HeldWeapon is null )
			return;

		if ( Scene.Camera is null )
			return;

		var hud = Scene.Camera.Hud;
		var sw = Screen.Width;
		var sh = Screen.Height;

		DrawCrosshair( hud, sw, sh );
		DrawAmmoDisplay( hud, sw, sh );

		if ( _holder.IsReloading )
			DrawReloadBar( hud, sw, sh );
	}

	private void DrawCrosshair( HudPainter hud, float sw, float sh )
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

	private void DrawAmmoDisplay( HudPainter hud, float sw, float sh )
	{
		var cyan = new Color( 0f, 0.9f, 1f );
		var cyanDim = new Color( 0f, 0.4f, 0.5f );
		var red = new Color( 1f, 0.2f, 0.2f );
		var dark = new Color( 0.04f, 0.1f, 0.16f, 0.75f );
		var isLow = _holder.CurrentAmmo <= 5;
		var accent = isLow ? red : cyan;
		var border = isLow ? red : cyanDim;

		// Panel position — bottom right
		var px = sw - 260f;
		var py = sh - 130f;

		// Background panel
		hud.DrawRect( new Rect( px, py, 220f, 90f ), dark );
		DrawBorder( hud, px, py, 220f, 90f, border );

		// ── Magazine ammo bar (big) ──
		var magBarX = px + 12f;
		var magBarY = py + 12f;
		var magBarW = 196f;
		var magBarH = 30f;
		var magFill = _holder.MaxAmmo > 0 ? (float)_holder.CurrentAmmo / _holder.MaxAmmo : 0f;

		// Bar background
		hud.DrawRect( new Rect( magBarX, magBarY, magBarW, magBarH ), new Color( 0.02f, 0.06f, 0.1f, 0.8f ) );
		// Bar fill
		hud.DrawRect( new Rect( magBarX, magBarY, magBarW * magFill, magBarH ), accent );
		// Bar border
		DrawBorder( hud, magBarX, magBarY, magBarW, magBarH, border );

		// Ammo tick marks inside the bar (one per bullet)
		var tickCount = Math.Min( _holder.MaxAmmo, 30 );
		var tickSpacing = magBarW / tickCount;
		for ( int i = 1; i < tickCount; i++ )
		{
			var tx = magBarX + i * tickSpacing;
			hud.DrawRect( new Rect( tx, magBarY, 1f, magBarH ), new Color( 0f, 0f, 0f, 0.3f ) );
		}

		// ── Reserve ammo bar (smaller) ──
		var resBarX = px + 12f;
		var resBarY = py + 52f;
		var resBarW = 196f;
		var resBarH = 10f;
		var resFill = _holder.MaxReserve > 0 ? (float)_holder.ReserveAmmo / _holder.MaxReserve : 0f;

		hud.DrawRect( new Rect( resBarX, resBarY, resBarW, resBarH ), new Color( 0.02f, 0.06f, 0.1f, 0.8f ) );
		hud.DrawRect( new Rect( resBarX, resBarY, resBarW * resFill, resBarH ), cyanDim );
		DrawBorder( hud, resBarX, resBarY, resBarW, resBarH, new Color( 0f, 0.3f, 0.4f, 0.5f ) );

		// ── Ammo count as individual dots (bottom of panel) ──
		var dotY = py + 72f;
		var dotSize = 4f;
		var dotGap = 2f;
		var dotsToShow = Math.Min( _holder.CurrentAmmo, 30 );
		for ( int i = 0; i < dotsToShow; i++ )
		{
			var dx = px + 12f + i * (dotSize + dotGap);
			hud.DrawRect( new Rect( dx, dotY, dotSize, dotSize ), accent );
		}
	}

	private void DrawReloadBar( HudPainter hud, float sw, float sh )
	{
		var cyan = new Color( 0f, 0.9f, 1f );
		var cyanDim = new Color( 0f, 0.4f, 0.5f );
		var dark = new Color( 0.04f, 0.1f, 0.16f, 0.85f );

		var bw = 200f;
		var bh = 8f;
		var bx = sw * 0.5f - bw * 0.5f;
		var by = sh * 0.5f + 50f;

		// Background
		hud.DrawRect( new Rect( bx - 2f, by - 2f, bw + 4f, bh + 4f ), dark );
		// Fill
		hud.DrawRect( new Rect( bx, by, bw * _holder.ReloadProgress, bh ), cyan );
		// Border
		DrawBorder( hud, bx, by, bw, bh, cyanDim );

		// Animated dots to indicate reloading
		var dotCount = 3;
		var phase = (Time.Now * 4f) % dotCount;
		for ( int i = 0; i < dotCount; i++ )
		{
			var alpha = (int)phase == i ? 1f : 0.3f;
			var dx = bx + bw * 0.5f - 12f + i * 10f;
			hud.DrawRect( new Rect( dx, by - 16f, 4f, 4f ), new Color( 0f, 0.9f, 1f, alpha ) );
		}
	}

	private void DrawBorder( HudPainter hud, float x, float y, float w, float h, Color c )
	{
		hud.DrawRect( new Rect( x, y, w, 1f ), c );
		hud.DrawRect( new Rect( x, y + h, w, 1f ), c );
		hud.DrawRect( new Rect( x, y, 1f, h ), c );
		hud.DrawRect( new Rect( x + w, y, 1f, h + 1f ), c );
	}
}
