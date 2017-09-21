using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Pixel
{
	public abstract class BasicPixelObject : TextureObject
	{
		public Vector2 PixelsCount { get; set; }
		public GameObject[,] Pixels { get; set; }

		protected BasicPixelObject(Camera camera, Vector2 position, PixelDescription[,] pixelsDescription, Vector2 pixelsCount, IParentObject parent = null) : base(camera, position, PixelOptions.PixelSideSize * pixelsCount, parent, TextureManager.Box2)
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
