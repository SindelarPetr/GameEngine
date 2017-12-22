using GameEngine.MathEngine;
using GameEngine.Options;
using Microsoft.Xna.Framework;

namespace GameEngine.ValueHolders
{
	public class ScaleHolder
	{
		public Vector2 Scale;
		public Vector2 PrefferedScale;
		public float BackForce = 0.015f; // the lower value the lower force
		public float Friction = (0.93f); //tření - čím menší tím vyšší
		readonly float _mass = 3;

		public Vector2 ActualVelocity = Vector2.Zero;

		public ScaleHolder(Vector2 position)
		{
			Scale = PrefferedScale = position;
		}

		public void ApplyForce(Vector2 force)
		{
			ActualVelocity += force / _mass;
		}

		public void Update()
		{
			//Treni
			ActualVelocity *= Friction;

			//BackForce
			if (ActualVelocity.Length() < 0.0001f && Vector2.Distance(PrefferedScale, Scale) < 0.0001f)
			{
				ActualVelocity = Vector2.Zero;
				Scale = PrefferedScale;
			}
			else
			{
				float size = Vector2.Distance(ActualVelocity, Vector2.Zero);
				ApplyForce(MyMath.RotatePoint(Vector2.Zero, new Vector2(BackForce * (Vector2.Distance(Scale, PrefferedScale)), 0), MyMath.MeasureAngle(Scale, PrefferedScale)));

				Scale += (ActualVelocity) * (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
			}
		}

		public void ResetValue(Vector2 newValue)
		{
			Scale = PrefferedScale = newValue;
		}
	}
}
