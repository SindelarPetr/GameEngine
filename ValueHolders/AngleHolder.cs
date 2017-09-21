
using GameEngine.MathEngine;
using System;

namespace GameEngine.ValueHolders
{
	public class AngleHolder
	{
		#region Operators

		public static implicit operator float(AngleHolder angleHolder) => angleHolder.Angle;
		#endregion

		public float Angle;
		public float AngleTolerance = 0.0001f;
		public float AngleToGo;

		private float _angleSpeed = 0;
		public float AngleSpeed
		{
			get
			{
				return _angleSpeed;
			}
			set
			{
				_angleSpeed = value;
			}
		}

		public float AngleSpeedTolerance = 0.0001f;

		public float Friction = 0.90f;
		public float BackForce = 0.005f;
		public float Mass = 1;

		public AngleHolder(float angle = 0)
		{
			Angle = angle;
		}

		public void Update()
		{
			if (AngleSpeed != 0 || Angle != AngleToGo)
			{
				ApplyForce(BackForce * MyMath.GetShorterAngleDiff(Angle, AngleToGo));
				AngleSpeed *= Friction;
				Angle += AngleSpeed;

				if (Math.Abs(MyMath.GetShorterAngleDiff(Angle, AngleToGo)) < AngleTolerance && Math.Abs(AngleSpeed) < AngleSpeedTolerance)
				{
					Angle = AngleToGo;
					AngleSpeed = 0;
				}
			}
		}

		public void ApplyForce(float force)
		{
			AngleSpeed += force / Mass;
		}
	}
}
