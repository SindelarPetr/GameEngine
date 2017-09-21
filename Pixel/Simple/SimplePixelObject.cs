using GameEngine.CameraEngine;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel
{
	public class SimplePixelObject : BasicPixelObject
	{
		public SimplePixelObject(Camera camera, Vector2 position, PixelDescription[,] pixelsDescription, Vector2 pixelsCount) : base(camera, position, pixelsDescription, pixelsCount)
		{

		}

		protected override GameObject[,] CreatePixels(PixelDescription[,] pixelsDescription)
		{
			SimplePixel[,] pixels = new SimplePixel[(int)PixelsCount.X, (int)PixelsCount.Y];

			for (int x = 0; x < PixelsCount.X; x++)
				for (int y = 0; y < PixelsCount.Y; y++)
				{
					PixelDescription pixelDescribtion = pixelsDescription[x, y];

					if (pixelDescribtion == null) continue;

					//if ((x % 2 == 0 && y % 2 == 0) || (x % 2 == 1 && y % 2 == 1)) pixelDescribtion = new PixelDescribtion(Color.Aqua);

					Vector2 pixelIndex = new Vector2(x, y);
					SimplePixel pixel = pixels[x, y] = new SimplePixel(Camera,
						PixelMath.PixelToGamePosition(pixelIndex, PixelsCount), PixelOptions.PixelSize);

					// Change pixels color according to description.
					pixel.ColorChanger.ResetColor(pixelDescribtion.Color);
				}

			return pixels;
		}

	}
}
