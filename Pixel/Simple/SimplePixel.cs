using GameEngine.Content;
using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel.Simple
{
	public class SimplePixel : GameObject
	{
		public SimplePixel(BasicLevel level, Vector2 position, Vector2 size, IWorldObject parent = null) : base(level, position, size, parent, TextureManager.Box2)
		{

		}
	}
}
