using GameEngine.CameraEngine;
using GameEngine.Menu.ScreensAs;
using GameEngine.Options;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.Screens.TextureObjects
{
	public class ScreenCover : ScreenTexture
	{
		private readonly float _maxOpacity;

		public ScreenCover(Color color = default(Color), float maxOpacity = 0.5f) : base(new Camera(), () => Vector2.Zero, () => DisplayOptions.Resolution)
		{
			SmoothOpacity.ResetValue(0);
			_maxOpacity = maxOpacity;
			ColorChanger.ResetColor(color);
		}

		public void Show()
		{
			SmoothOpacity.ChangeValue(_maxOpacity);
		}

		public void Hide()
		{
			SmoothOpacity.ChangeValue(0);
		}
	}
}
