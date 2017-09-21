using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Pixel
{
	[Flags]
	public enum PixelAttribute { None = 0, Light = 1, Static = 2 }

	public class PixelDescription
	{
		public PixelAttribute PixelAttribute { get; }
		public Color Color { get; }

		public PixelDescription(Color color, PixelAttribute pixelAttribute = PixelAttribute.None)
		{
			Color = color;
			PixelAttribute = pixelAttribute;
		}
	}
}
