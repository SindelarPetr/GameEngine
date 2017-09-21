using GameEngine.Options;
using System;

namespace GameEngine.ValueHolders
{
	public class Timer
	{
		/// <summary>
		/// Represents a procentage number (from 0 to 1) of the progress of the timer. Its calculated as IntervalLeft / IntervalMax.
		/// </summary>
		public double ProgressRatio => IntervalLeft / IntervalMax;

		public double IntervalMax { get; set; }
		public double IntervalLeft { get; set; }

		private bool _running;
		private bool _runningToEnd;

		public bool IsRepeater { get; set; }

		public event EventHandler OnElapsed;

		public Timer(double intervalMaxMs, bool isRepeater = true, bool running = true)
		{
			IsRepeater = isRepeater;
			_running = running;
			IntervalMax = intervalMaxMs;
		}

		public void Update(double timeSpeedMultiply = 1)
		{
			if (_running == false) return;

			IntervalLeft -= GeneralOptions.GameTime.ElapsedGameTime.Milliseconds * timeSpeedMultiply;

			if (IntervalLeft > 0) return;

			if (_runningToEnd) return;

			OnElapsed?.Invoke(this, null);

			if (IsRepeater) IntervalLeft = IntervalMax;
			else Stop();
		}

		public void Start()
		{
			_running = true;
			_runningToEnd = false;

			if(IntervalLeft == IntervalMax) Reset();
		}


		/// <summary>
		/// Stops running time and resets the current interval.
		/// </summary>
		public void Stop()
		{
			Pause();
			Reset();
		}

		/// <summary>
		/// Stops counting time, but does not reset it.
		/// </summary>
		public void Pause()
		{
			_running = false;
			_runningToEnd = false;
		}

		/// <summary>
		/// Timer will still run, but when he should Elapse he just ends, calls no event and stops. It is used especially for timing shooting.
		/// </summary>
		public void RunToEnd()
		{
			_runningToEnd = true;
		}

		/// <summary>
		/// Will start to run for currently elapsed time. If its not running then resets itself and starts to run from zero.
		/// </summary>
		public void Reverse()
		{
			if (IntervalLeft == 0) Reset();

			IntervalLeft = IntervalMax - IntervalLeft;
			Start();
		}

		/// <summary>
		/// Resets currently elapsed time.
		/// </summary>
		public void Reset()
		{
			IntervalLeft = IntervalMax;
		}
	}
}
