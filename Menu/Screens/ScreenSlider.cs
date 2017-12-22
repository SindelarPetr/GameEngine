using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Input;
using GameEngine.Input.TouchPanel;
using GameEngine.Menu.Screens.Buttons;
using GameEngine.PropertiesEngine;

namespace GameEngine.Menu.Screens
{
	public enum SliderOrientation { Vertical, Horizontal }
	public class ScreenSlider : ScreenButton
	{
		private double _ratio;

		public double Ratio
		{
			get => _ratio;
			set
			{
				if (_ratio == value) return;

				QuietResetRatio(value);
				OnValueChanged?.Invoke(_ratio);
			}
		}

		private readonly ScreenTexture _slider;

		public event Action<double> OnValueChanged;


		public ScreenSlider(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, IScreenParentObject parent = null, MyTexture2D texture = null) : base(camera, positionProvider, sizeProvider, parent, texture)
		{
			OnClickContinues += ScreenSlider_OnClickContinues;

			_slider = new ScreenTexture(Camera, GetSliderPosition, () => new Vector2(sizeProvider.Invoke().X * 0.9f, sizeProvider.Invoke().X / 4f), this);
			AddNestedObject(_slider, 4);
		}

		private Vector2 GetSliderPosition()
		{
			return new Vector2(0, BasicSize.Y * (1 - (float)Ratio) - BasicSize.Y / 2f);
		}

		private void ScreenSlider_OnClickContinues(MyTouch touch)
		{
			float absRotation = GetAbsoluteRotation();
			Vector2 absPosition = GetAbsolutePosition();
			Vector2 absSize = GetAbsoluteSize();
			Vector2 touchPosition = MyMath.RotatePoint(absPosition, touch.Position, -absRotation);

			float height = touchPosition.Y - (absPosition.Y - absSize.Y / 2f);
			float ratio = 1 - height / BasicSize.Y;

			Ratio = MyMath.CutValue(ratio, 0, 1);
		}

		public void QuietResetRatio(double value)
		{
			_ratio = value;
			_slider.BasicPosition = GetSliderPosition();
		}
	}
}
