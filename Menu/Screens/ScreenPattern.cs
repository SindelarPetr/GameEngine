using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Menu.ScreensAs;
using GameEngine.Options;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.Screens
{
	public class ScreenBackgroundPattern : ScreenTexture
	{
		public ScreenBackgroundPattern(Camera camera, MyTexture2D texture = null) : base(camera, () => Vector2.Zero, () => DisplayOptions.Resolution, null, texture ?? TextureManager.StripPattern)
		{
			IsPattern = true;
			BasicOpacity = 0.1f;
		}
	}
}
