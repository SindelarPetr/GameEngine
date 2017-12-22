using GameEngine.Content;
using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Pixel
{
	public abstract class BasicPixelObject : GameObject
	{
		public Vector2 PixelsCount { get; set; }
		public GameObject[,] Pixels { get; set; }

		protected BasicPixelObject(BasicLevel level, Vector2 position, PixelDescription[,] pixelsDescription, Vector2 pixelsCount, IWorldObject parent = null) : base(level, position, PixelOptions.PixelSideSize * pixelsCount, parent, TextureManager.Box2)
		{
			PixelsCount = pixelsCount;
		}

		protected abstract GameObject[,] CreatePixels(PixelDescription[,] pixelsDescription);

		public override void Draw(SpriteBatch spriteBatch)
		{
			foreach (var pixel in Pixels)
			{
				pixel?.Draw(spriteBatch);
			}
		}
	}
}
