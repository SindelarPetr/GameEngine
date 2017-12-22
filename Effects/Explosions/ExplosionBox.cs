using GameEngine.Content;
using GameEngine.GamePrimitives;
using GameEngine.MathEngine;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Explosions
{
	internal class ExplosionBox : GameObject
	{
		private readonly float _direction;
		private readonly float _distance;
		private readonly Vector2 _originPosition;

		public ExplosionBox(BasicLevel level, Vector2 position, float sideSize, float direction, float distance, IWorldObject parent = null)
			: base(level, position, new Vector2(sideSize), parent, TextureManager.Box2)
		{
			base.BasicRotation = _direction = direction;
			_distance = distance;
			_originPosition = position;
			SmoothOpacity.ValueToGo = 0;
		}

		public override void Update()
		{
			base.Update();

			BasicPosition = _originPosition + MyMath.RotatePoint((1 - BasicOpacity) * _distance, _direction);

			if (BasicOpacity < 0.0001f)
			{
				Remove();
			}
		}
	}
}
