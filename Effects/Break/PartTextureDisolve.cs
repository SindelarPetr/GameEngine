using GameEngine.CameraEngine;
using GameEngine.Effects.Tail;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Break
{
	internal class PartTextureDisolve : TextureDisolve
	{
		public PartTextureDisolve(Camera camera, Vector2 position, Vector2 size, Rectangle rectangle, Vector2 originMultiplier,
			float velocity = 1f, float direction = 0f, float rotationSpeed = 0.1f, IParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, velocity, direction, rotationSpeed, texture)
		{
			CutRectangle = rectangle;
			OriginMultiplier = originMultiplier;

			SmoothOpacity.Friction = 0.0001f;
		}
	}
}
