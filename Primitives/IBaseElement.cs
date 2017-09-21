using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Primitives
{
	public interface IBaseElement
	{
		void Update();

		void Draw(SpriteBatch spriteBatch);
	}
}
