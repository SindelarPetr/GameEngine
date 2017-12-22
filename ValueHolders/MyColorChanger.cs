using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.ValueHolders
{
	public class MyColorChanger
	{
		#region Operators
		public static implicit operator Color(MyColorChanger colorChanger)
		{
			return colorChanger.MyColor;
		}

		public static Color operator *(MyColorChanger colorChanger, float num)
		{
			return colorChanger.MyColor * num;
		}
		#endregion

		public MyColor MyColor;
		public MyColor MyColorToGo;

		public float ChangeConst = 0.01f;
		public float ChangeBasicSpeed = 0.01f;

		public MyColorChanger()
		{
			MyColor = new MyColor(Color.Black);
			MyColorToGo = new MyColor(Color.Black);
		}

		public MyColorChanger(MyColor firstColor)
		{
			MyColor = firstColor.Duplicate();
			MyColorToGo = firstColor;
		}

		/// <summary>
		/// Sets the color to the given one - color and color to go. Copies it (wont reference it).
		/// </summary>
		/// <param name="color">The color to which local color should be changed</param>
		public void SetColor(MyColor color)
		{
			ResetColor(color.Color);
		}

		/// <summary>
		/// Sets the color to the given one - color and color to go. Copies it (wont reference it).
		/// </summary>
		/// <param name="color">The color to which local color should be changed</param>
		public void ResetColor(Color color)
		{
			MyColor = new MyColor(color);
			MyColorToGo = new MyColor(color);
		}

		public void Update()
		{
			MyColor.AdjustValue(ChangeBasicSpeed, ChangeConst, MyColorToGo);
		}
	}
}
