using GameEngine.MathEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.ValueHolders
{
	public class SmoothVector
	{
		#region Operators
		public static implicit operator Vector2(SmoothVector smoothValue)
		{
			return smoothValue.Value;
		}
		#endregion

		public Vector2 Value { get; set; }
		public Vector2 ValueToGo { get; set; }
		public float ValueTolerance { get; set; }
		public Vector2 BasicSpeed { get; set; }

		/// <summary>
		/// speed = (Friction * |Value - ValueToGo| + BasicSpeed) * GameTime.ElapsedGameTime.TotalMiliseconds
		/// </summary>
		public float Friction { get; set; }

		public SmoothVector(Vector2 basicValue)
		{
			ValueTolerance = 0.001f;
			BasicSpeed = new Vector2(0.0001f);
			Friction = 0.01f;

			ResetValue(basicValue);
		}

		public void Update()
		{
			if (Value != ValueToGo)
			{
				Value = MyMath.AdjustValue(Value, MyMath.Abs(ValueToGo - Value) * Friction + BasicSpeed, ValueToGo);
			}
		}

		public void ChangeValue(Vector2 newValue)
		{
			ValueToGo = newValue;
		}

		public void ResetValue(Vector2 value)
		{
			Value = ValueToGo = value;
		}

		public bool IsFinished()
		{
			return Value == ValueToGo;
		}

	}
}
