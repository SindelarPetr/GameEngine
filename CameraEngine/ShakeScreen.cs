using GameEngine.MathEngine;
using GameEngine.Options;
using Microsoft.Xna.Framework;

namespace GameEngine.CameraEngine
{
	public class ShakeScreen
	{
		public Vector2 ShakeViewMove;
		public Vector2 ShakeActualVelocity;
		public float ShakeBackForce = 0.015f;
		public bool Enabled = true;
		public float ShakeFriction = 0.90f;
		public float ShakeMass = 1;

		public void Update()
		{
			#region Screen shake
			if (Enabled)
			{
				//Treni
				ShakeActualVelocity *= ShakeFriction;

				ApplyShakeForce(MyMath.RotatePoint(Vector2.Zero, new Vector2(ShakeBackForce * Vector2.Distance(ShakeViewMove, Vector2.Zero), 0), ShakeViewMove.MeasureAngle(Vector2.Zero)));

				ShakeViewMove += ShakeActualVelocity * (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
			}
			#endregion
		}

		public void ApplyShakeForce(Vector2 force)
		{
			ShakeActualVelocity += force / ShakeMass;
		}
	}
}
