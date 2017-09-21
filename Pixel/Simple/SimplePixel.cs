using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel
{
	public class SimplePixel : GameObject
	{
		public SimplePixel(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null) : base(camera, position, size, parent, TextureManager.Box2)
		{

		}
	}
}
