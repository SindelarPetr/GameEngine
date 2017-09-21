using GameEngine.Options;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel
{
	public static class PixelOptions
	{
		public static float PixelSideSize { get; set; } = DisplayOptions.Resolution.X / 80f;
		public static Vector2 PixelSize => new Vector2(PixelSideSize);
	}
}
