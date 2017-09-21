using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.MathEngine;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Explosions
{
	internal class ExplosionBox : GameObject
	{
		private readonly float _direction;
		private readonly float _distance;
		private readonly Vector2 _originPosition;

		public ExplosionBox(Camera camera, Vector2 position, float sideSize, float direction, float distance, IParentObject parent = null)
			: base(camera, position, new Vector2(sideSize), parent, TextureManager.Box2)
		{
			BasicRotation = _direction = direction;
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
