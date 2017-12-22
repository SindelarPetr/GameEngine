using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.ObjectPrimitives
{
	public interface IPrimitiveElement
	{
		void Update();

		void Draw(SpriteBatch spriteBatch);
	}
}
