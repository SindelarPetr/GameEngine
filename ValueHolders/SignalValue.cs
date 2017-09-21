using System;
using GameEngine.MathEngine;

namespace GameEngine.ValueHolders
{
	public class SignalValue
	{
		#region Operators
		public static implicit operator float(SignalValue smoothValue)
		{
			return smoothValue.Value;
		}
		#endregion

		public float Value = 1;
		public float ValueTolerance = 0.001f;
		public float ValueToGo = 1;
		public float ValueOrigin = 1;
		public float BasicSpeed = 0.0001f;

		/// <summary>
		/// speed = (Friction * |Value - ValueOrigin| + BasicSpeed) * GameTime.ElapsedGameTime.TotalMiliseconds
		/// </summary>
		public float Friction = 0.005f;

		public SignalValue(float origin, float basicValue)
		{
			ResetValue(basicValue);
			ValueOrigin = origin;
		}

		public virtual void Update()
		{
			if (Value == ValueToGo) return;

			Value = MyMath.AdjustValue(Value, Math.Abs(ValueOrigin - Value) * Friction + BasicSpeed, ValueToGo);
		}

		public void ChangeValue(float newValue)
		{
			Value = newValue;
		}

		public void ResetValue(float value)
		{
			Value = ValueToGo = value;
		}

		public bool IsFinished()
		{
			return Value == ValueToGo;
		}

	}
}
