using GameEngine.MathEngine;
using System;

namespace GameEngine.ValueHolders
{
	public class SmoothValue
	{
		#region Operators
		public static implicit operator float(SmoothValue smoothValue)
		{
			return smoothValue.Value;
		}
		#endregion

		public float Value = 1;
		public float ValueToGo = 1;
		public float ValueTolerance = 0.001f;
		public float BasicSpeed = 0.0001f;

		public event EventHandler<float> OnValueChanged; 
		/// <summary>
		/// speed = (Friction * |Value - ValueToGo| + BasicSpeed) * GameTime.ElapsedGameTime.TotalMiliseconds
		/// </summary>
		public float Friction = 0.005f;

		public SmoothValue(float basicValue)
		{
			ResetValue(basicValue);
		}

		public void Update()
		{
			if (Value != ValueToGo)
			{
				Value = MyMath.AdjustValue(Value, Math.Abs(ValueToGo - Value) * Friction + BasicSpeed, ValueToGo);
				OnValueChanged?.Invoke(this, Value);
			}
		}

		public void ChangeValue(float newValue)
		{
			ValueToGo = newValue;
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
