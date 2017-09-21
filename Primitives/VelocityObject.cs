using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Options;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace GameEngine.Primitives
{
	public class VelocityObject : GameObject
	{
		private readonly float _velocity;
		private readonly float _rotationSpeed;
		private readonly float _direction;

		public VelocityObject(Camera camera, Vector2 position, Vector2 size, float velocity, float direction, float rotationSpeed = 0, IParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture)
		{
			_velocity = velocity;
			_rotationSpeed = rotationSpeed;
			BasicRotation = _direction = direction;
		}

		public override void Update()
		{
			base.Update();

			BasicPosition += MyMath.RotatePoint(_velocity * (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds,
				_direction);
			BasicRotation += _rotationSpeed * (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
		}
	}
}
