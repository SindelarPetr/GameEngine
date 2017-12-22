using GameEngine.Content;
using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects
{
	public class TextureDisolve : VelocityObject
	{
		public TextureDisolve(BasicLevel level, Vector2 position, Vector2 size, IWorldObject parent = null, float velocity = 0, float direction = 0, float rotationSpeed = 0, MyTexture2D texture = null)
			: base(level, position, size, velocity, direction, rotationSpeed, parent, texture ?? TextureManager.Box2)
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
