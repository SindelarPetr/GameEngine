namespace GameEngine.ValueHolders
{
	public class SignalValueUser : SignalValue
	{
		private readonly float _valueFrom;
		private readonly float _valueTo;
		private bool _isSignalizing;

		/// <summary>
		/// Creates instance of the signal value user.
		/// </summary>
		/// <param name="valueFrom">First value.</param>
		/// <param name="valueTo">Value will move to this position.</param>
		/// <param name="origin">Value will calculate speed according to origin.</param>
		public SignalValueUser(float valueFrom, float valueTo, float origin) : base(origin, valueFrom)
		{
			_valueFrom = valueFrom;
			_valueTo = valueTo;
			ValueToGo = valueTo;
			_isSignalizing = true;
		}

		public override void Update()
		{
			base.Update();

			if (!_isSignalizing) return;
			if (Value == _valueTo) ValueToGo = _valueFrom;
			if (Value == _valueFrom) ValueToGo = _valueTo;
		}

		public void StartSignalizing()
		{
			_isSignalizing = true;
		}

		public void StopSignalizing()
		{
			_isSignalizing = false;
			ValueToGo = _valueTo;
		}
	}
}
