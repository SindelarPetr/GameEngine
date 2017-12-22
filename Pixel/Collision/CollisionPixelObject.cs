using FarseerPhysics.Dynamics;
using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel.Collision
{
	public class CollisionPixelObject : BasicPixelObject
	{
		private readonly World _world;

		public CollisionPixelObject(BasicLevel level, World world, Vector2 position, PixelDescription[,] pixelsDescription, Vector2 pixelsCount, IWorldObject parent = null) : base(level, position, pixelsDescription, pixelsCount, parent)
		{
			_world = world;
			Pixels = CreatePixels(pixelsDescription);
		}

		protected sealed override GameObject[,] CreatePixels(PixelDescription[,] pixelsDescription)
		{
			var pixels = new GameObject[(int)PixelsCount.X, (int)PixelsCount.Y];

			for (int x = 0; x < PixelsCount.X; x++)
				for (int y = 0; y < PixelsCount.Y; y++)
				{
					var pixelDescribtion = pixelsDescription[x, y];

					if (pixelDescribtion == null) continue;

					var pixelIndex = new Vector2(x, y);
					CollisionPixel pixel;
					pixels[x, y] = pixel = new CollisionPixel(Level, _world,
						PixelMath.PixelToGamePosition(pixelIndex, PixelsCount), PixelOptions.PixelSize);
					PixelCreated(pixel);
					// Change pixels color according to description.
					pixel.ColorChanger.ResetColor(pixelDescribtion.Color);

					ConnectNeighbours(pixels, pixel, x, y);
					SetNeighbourBorders(pixel, x, y);
				}

			return pixels;
		}

		protected virtual void PixelCreated(CollisionPixel pixel)
		{

		}

		/// <summary>
		/// Checks if the pixel is at the game border and if so then tells it to the pixel.
		/// </summary>
		/// <param name="pixel">The specific pixel which is being checked.</param>
		/// <param name="indexX">X index in the pixel array</param>
		/// <param name="indexY">Y index in the pixel array</param>
		private void SetNeighbourBorders(CollisionPixel pixel, int indexX, int indexY)
		{
			if (indexX == 0)
				pixel.AddNeighbourBorder();

			if (indexX == PixelsCount.X - 1)
				pixel.AddNeighbourBorder();

			if (indexY == 0)
				pixel.AddNeighbourBorder();

			if (indexY == PixelsCount.Y - 1)
				pixel.AddNeighbourBorder();
		}

		private void ConnectNeighbours(GameObject[,] pixels, CollisionPixel pixel, int indexX, int indexY)
		{
			// Left pixel
			if (indexX > 0) ConnectPixels(pixel, (CollisionPixel)pixels[indexX - 1, indexY]);

			// Top pixel
			if (indexY > 0) ConnectPixels(pixel, (CollisionPixel)pixels[indexX, indexY - 1]);

			// No more are needed because the pixels are being created from left top corner.
		}

		private void ConnectPixels(CollisionPixel pixelA, CollisionPixel pixelB)
		{
			if (pixelA == null || pixelB == null) return;

			pixelA.AddNeighbour(pixelB);
			pixelB.AddNeighbour(pixelA);
		}
	}
}
