using GameEngine.ValueHolders;
using System;

namespace GameEngine.Menu
{
	public class ShowTimer
	{
		public ShowState ShowState { get; private set; } = ShowState.Hidden;

		// Describes current progress of enabling or disabling
		private Timer _showValue;

		public event EventHandler<float> OnShowed;
		public event EventHandler<float> OnHidden;

		public ShowTimer(double showTimeMs = 2000, bool isShowed = false)
		{
			_showValue = new Timer(showTimeMs, false, false);
			_showValue.OnElapsed += ShowValueOnOnElapsed;

			if (isShowed)
			{
				ShowState = ShowState.Showed;
			}
		}

		private void ShowValueOnOnElapsed(object sender, EventArgs eventArgs)
		{
			if (ShowState == ShowState.Showing)
			{
				ShowState = ShowState.Showed;
				OnShowed?.Invoke(this, 1f);
			}
			else if (ShowState == ShowState.Hiding)
			{
				ShowState = ShowState.Hidden;
				OnShowed?.Invoke(this, 0f);
			}
		}

		public bool IsHidden()
		{
			return ShowState == ShowState.Hidden;
		}

		public bool IsShowed()
		{
			return ShowState == ShowState.Showed;
		}

		public double GetShowValue()
		{
			if (IsHidden()) return 0;
			if (IsShowed()) return 1;

			if (ShowState == ShowState.Showing) return 1 - _showValue.ProgressRatio;

			return _showValue.ProgressRatio;
		}

		public virtual void Show()
		{
			ShowState = ShowState.Showing;
			_showValue.Start();
		}

		public virtual void Hide()
		{
			ShowState = ShowState.Hiding;
			_showValue.Start();
		}

		public virtual void Update()
		{
			_showValue.Update();
		}

		public void Reverse()
		{
			if (IsHidden())
			{
				Show();
				return;
			}

			if (IsShowed())
			{
				Hide();
				return;
			}

			if (ShowState == ShowState.Hiding)
				ShowState = ShowState.Showing;

			else if (ShowState == ShowState.Showing)
				ShowState = ShowState.Hiding;

			_showValue.Reverse();
		}
	}
}
