using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Tail
{
	public class TextureDisolve : VelocityObject
	{
		public TextureDisolve(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null, float velocity = 0, float direction = 0, float rotationSpeed = 0, MyTexture2D texture = null)
			: base(camera, position, size, velocity, direction, rotationSpeed, parent, texture ?? TextureManager.Box2)
		{
			SmoothOpacity.ValueToGo = 0;
		}

		public override void Update()
		{
			base.Update();

			if (BasicOpacity < 0.001f)
				Remove();
		}
	}
}
