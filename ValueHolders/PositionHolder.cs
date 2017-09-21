using GameEngine.MathEngine;
using GameEngine.Options;
using Microsoft.Xna.Framework;

namespace GameEngine.ValueHolders
{
	public class PositionHolder
	{
		public Vector2 Position;
		public Vector2 PrefferedPosition;
		public float BackForce = 0.015f; // the lower value the lower force
		public float Friction = 0.93f; // friction of the holder - the lower value the higher friction is
		float _mass = 3;

		public Vector2 ActualVelocity = Vector2.Zero;

		public PositionHolder()
			: this(Vector2.Zero) { }

		public PositionHolder(Vector2 position)
		{
			Position = PrefferedPosition = position;
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
			if (ActualVelocity.Length() < 0.01f && Vector2.Distance(PrefferedPosition, Position) < 0.2f)
			{
				ActualVelocity = Vector2.Zero;
				Position = PrefferedPosition;
			}
			else
			{
				float size = Vector2.Distance(ActualVelocity, Vector2.Zero);
				ApplyForce(MyMath.RotatePoint(Vector2.Zero, new Vector2(BackForce * Vector2.Distance(Position, PrefferedPosition), 0), MyMath.MeasureAngle(Position, PrefferedPosition)));

				Position += ActualVelocity * (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
			}
		}

		public void ResetValue(Vector2 newValue)
		{
			Position = PrefferedPosition = newValue;
		}

		/// <summary>
		/// Simulates impact to a wall. Sets actual position according to parameter and resets velocity component X.
		/// </summary>
		/// <param name="newPosition">Position which will be setted.</param>
		public void ImpactX(Vector2 newPosition)
		{
			Position = newPosition;

			if (ActualVelocity.X > 0 && Position.X < PrefferedPosition.X)
			{
				PrefferedPosition.X = Position.X;
			}

			if (ActualVelocity.X < 0 && Position.X > PrefferedPosition.X)
			{
				PrefferedPosition.X = Position.X;
			}

			ActualVelocity.X = 0;
		}
	}
}
