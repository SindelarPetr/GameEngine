using GameEngine.MathEngine;
using System;

namespace GameEngine.ValueHolders
{
	public class MyValue
	{
		public float Value { get; private set; }
		public float ValueToGo { get; private set; }
		public float ValueLinearSpeed = 0.1f;
		public float ValueSmoothSpeedConst = 0.056f; //0.9 / 16

		public bool Done = true;

		public MyValue()
		{
			Value = ValueToGo = 0;
			SetSpeedValuesForTimer();
		}

		public MyValue(float value)
		{
			Value = ValueToGo = value;
		}

		public void Update()
		{
			Value = MyMath.AdjustValue(Value, ValueSmoothSpeedConst * Math.Abs(ValueToGo - Value) + ValueLinearSpeed,
				ValueToGo);

			Done = Value == ValueToGo;
		}

		public void SetSpeedValuesForTimer()
		{
			ValueLinearSpeed = 1f;
			ValueSmoothSpeedConst = 0f;
		}

		public void SetValuesHard(float value)
		{
			Value = ValueToGo = value;
		}

		public void SetValue(float value)
		{
			Value = value;

			Done = Value == ValueToGo;
		}

		public void SetValueToGo(float valueToGo)
		{
			ValueToGo = valueToGo;

			Done = Value == ValueToGo;
		}
	}
}
