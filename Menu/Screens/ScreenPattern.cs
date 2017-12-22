using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Options;
using GameEngine.PropertiesEngine;
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
