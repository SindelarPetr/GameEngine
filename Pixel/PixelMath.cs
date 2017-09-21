using GameEngine.MathEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel
{
	public static class PixelMath
	{
		public static Vector2 PixelToGameVector(Vector2 vector)
		{
			return vector * PixelOptions.PixelSize;
		}

		public static Vector2 PixelToGamePosition(Vector2 pixelIndex, Vector2 pixelsCountInMap)
		{
			// Count position from top left corner
			Vector2 topLeftPosition = pixelIndex * PixelOptions.PixelSideSize + PixelOptions.PixelSize / 2f;

			// Move the position according to rule: middle of the map is on position [0,0]
			return topLeftPosition - pixelsCountInMap * PixelOptions.PixelSideSize / 2;
		}

		public static Vector2 GameToPixelVector(Vector2 gameVector)
		{
			return gameVector / PixelOptions.PixelSize;
		}

		public static Vector2 GameToStrictPixelVector(Vector2 gameVector)
		{
			return MyMath.ZaokrouhlitCislo(GameToPixelVector(gameVector));
		}

		public static Vector2 GameToPixelPosition(Vector2 gamePosition, Vector2 pixelsCountInMap)
		{
			// Count position from top left corner
			Vector2 topLeftPosition = gamePosition + pixelsCountInMap * PixelOptions.PixelSize / 2;

			// Move the position according to rule: middle of the map is on position [0,0]
			return GameToPixelVector(topLeftPosition);
		}

		public static Vector2 GameToStrictPixelPosition(Vector2 gamePosition, Vector2 pixelsCountInMap)
		{
			return MyMath.ZaokrouhlitCislo(GameToPixelPosition(gamePosition, pixelsCountInMap));
		}
	}
}
