using GameEngine.Content;
using GameEngine.MathEngine;
using GameEngine.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Effects.Shapes
{
	public static class ShapeDrawing
	{
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color? color = null, float thickness = 5)
		{
			Vector2 difference = end - start;
			Vector2 position = (start + end) / 2f + DisplayOptions.MiddleOfScreen;
			Vector2 size = new Vector2(difference.Length(), thickness);
			spriteBatch.Draw(TextureManager.Box2, position, null, color ?? Color.Red, difference.MeasureAngle(),
				TextureManager.Box2.Middle, size / TextureManager.Box2.Size, SpriteEffects.None, 0f);
		}

		public static void DrawDisplayCover(SpriteBatch spriteBatch, float opacity, Color color)
		{
			spriteBatch.Draw(TextureManager.Box2, DisplayOptions.MiddleOfScreen, null, color * opacity, 0,
				TextureManager.Box2.Middle, DisplayOptions.Resolution / TextureManager.Box2.Size, SpriteEffects.None, 0f);
		}
	}
}
