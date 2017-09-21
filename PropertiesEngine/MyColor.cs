using GameEngine.MathEngine;
using System;
using Microsoft.Xna.Framework;

namespace GameEngine.Properties
{
	public class MyColor
	{
		#region Operators
		public static bool operator ==(MyColor leftColor, MyColor rightColor)
		{
			if (leftColor._a == rightColor._a && leftColor._b == rightColor._b && leftColor._r == rightColor._r &&
				leftColor._g == rightColor._g)
				return true;

			return false;
		}
		public static bool operator !=(MyColor leftColor, MyColor rightColor)
		{
			return !(leftColor == rightColor);
		}

		public static implicit operator Color(MyColor color)
		{
			return color.Color;
		}

		public static Color operator *(MyColor color, float num)
		{
			return color.Color * num;
		}

		public static explicit operator MyColor(Color color)
		{
			return new MyColor(color);
		}
		#endregion

		#region Components
		float _a;
		public float A
		{
			get => _a;
			set
			{
				_a = MyMath.CutToByte(value);
				_color.A = (byte)_a;
			}
		}

		float _b;
		public float B
		{
			get
			{
				return _b;
			}
			set
			{
				_b = MyMath.CutToByte(value);
				_color.B = (byte)_b;
			}
		}

		float _r;
		public float R
		{
			get => _r;
			set
			{
				_r = MyMath.CutToByte(value);
				_color.R = (byte)_r;
			}
		}

		float _g;
		public float G
		{
			get => _g;
			set
			{
				_g = MyMath.CutToByte(value);
				_color.G = (byte)_g;
			}
		}

		private Color _color;
		public Color Color => _color;
		#endregion

		#region Constructors
		public MyColor()
		{
			_a = 255;
			_r = 255;
			_g = 255;
			_b = 255;
			_color = new Color(_r, _g, _b, _a);
		}

		public MyColor(Color ownColor)
		{
			_r = ownColor.R;
			_g = ownColor.G;
			_b = ownColor.B;
			_a = ownColor.A;
			_color = ownColor;
		}

		public MyColor(float r, float g, float b, float a)
		{
			_color = new Color();
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public MyColor(float r, float g, float b)
			: this(r, g, b, 255)
		{

		}
		#endregion

		#region Operations
		public MyColor Duplicate()
		{
			return new MyColor(_r, _g, _b, _a);
		}
		public override bool Equals(object obj)
		{
			return this == (MyColor)obj;
		}
		public override int GetHashCode()
		{
			return (int)R + (int)G + (int)B + (int)A;
		}
		#endregion

		#region Adjusters
		public void AdjustLight(float ratio)
		{
			if (ratio > 0)
			{
				R = AdjustValueHigher(R, ratio);
				G = AdjustValueHigher(G, ratio);
				B = AdjustValueHigher(B, ratio);
			}
			else
			{
				R = AdjustValueLower(R, ratio);
				G = AdjustValueLower(G, ratio);
				B = AdjustValueLower(B, ratio);

			}
		}

		private float AdjustValueHigher(float value, float ratio)
		{
			if (ratio >= 1 || ratio <= 0)
			{
				throw new ArgumentException("ratio is higher than 1 or lower than 0");
			}

			return value + (255 - value) * ratio;
		}

		private float AdjustValueLower(float value, float ratio)
		{
			if (ratio <= -1 || ratio >= 0)
			{
				throw new ArgumentException("ratio is higher than 0 or lower than -1");
			}

			return value + value * ratio;
		}

		public void Change(Color color)
		{
			R = color.R;
			G = color.G;
			B = color.B;
			A = color.A;
		}
		#endregion
	}
}
