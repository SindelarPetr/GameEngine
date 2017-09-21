using System;
using GameEngine.MathEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Properties
{
	public static class PrpertiesMath
	{
		public static void AdjustValue(this MyColor myColor, float speed, MyColor myColorToGo)
		{
			if (myColor == myColorToGo) return;

			myColor.R = MyMath.AdjustValue(myColor.R, speed, myColorToGo.R);
			myColor.G = MyMath.AdjustValue(myColor.G, speed, myColorToGo.G);
			myColor.B = MyMath.AdjustValue(myColor.B, speed, myColorToGo.B);
			myColor.A = MyMath.AdjustValue(myColor.A, speed, myColorToGo.A);
		}

		public static void AdjustValue(this MyColor myColor, float speed, float speedConst, MyColor myColorToGo)
		{
			if (myColor == myColorToGo) return;

			myColor.R = MyMath.AdjustValue(myColor.R, speed + Math.Abs(myColor.R - myColorToGo.R) * speedConst, myColorToGo.R);
			myColor.G = MyMath.AdjustValue(myColor.G, speed + Math.Abs(myColor.G - myColorToGo.G) * speedConst, myColorToGo.G);
			myColor.B = MyMath.AdjustValue(myColor.B, speed + Math.Abs(myColor.B - myColorToGo.B) * speedConst, myColorToGo.B);
			myColor.A = MyMath.AdjustValue(myColor.A, speed + Math.Abs(myColor.A - myColorToGo.A) * speedConst, myColorToGo.A);
		}

		public static Vector2 GetSize(this Texture2D texture)
		{
			return new Vector2(texture.Width, texture.Height);
		}
	}
}
