using GameEngine.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.MathEngine
{
	public static class MyMath
	{
		#region Random number
		public static Random Random = new Random();
		public static float GetRandomAngle(float span = MathHelper.TwoPi)
		{
			int min = -(int)(span * 1000f / 2f);
			int max = (int)(span * 1000f / 2f);
			float angle = Random.Next(min, max) / 1000f;
			return angle;
		}

		public static int GetRandomMarkNotNull()
		{
			return Random.Next(0, 2) == 1 ? 1 : -1;
		}

		public static float GetRandomFloat(float from, float to)
		{
			return Random.Next((int)(from * 100000f), (int)(to * 100000f)) / 100000f;
		}
		#endregion

		#region Angle operations
		public static float MeasureAngle(this Vector2 zaklad, Vector2 b)
		{
			double rozdilX = -zaklad.X + b.X;
			double rozdilY = -zaklad.Y + b.Y;
			return (float)Math.Atan2(rozdilY, rozdilX);
		}
		public static float MeasureAngle(this Vector2 b)
		{
			return (float)Math.Atan2(b.Y, b.X);
		}
		public static bool IsAngleBetween(float alfa, float uhel1, float uhel2)
		{
			List<float> uhly = new List<float>();
			uhly.Add(MathHelper.WrapAngle(uhel1));
			uhly.Add(MathHelper.WrapAngle(uhel2));
			uhly.Sort();

			//Porovnava vzdy s uhlem mensim nez PI
			bool presNulu = Math.Abs(uhly[uhly.Count - 1]) + Math.Abs(uhly[0]) < (float)Math.PI;
			if (presNulu)
			{
				if (alfa > uhly[0] && alfa < uhly[uhly.Count - 1])
					return true;
			}
			else
			{
				if (uhly[0] > 0 || uhly[uhly.Count - 1] < 0)
				{
					if (alfa < 0 && uhly[uhly.Count - 1] < 0)
					{
						alfa = (float)Math.Abs(alfa);
						if ((Math.Abs(alfa) < Math.Abs(uhly[0]) && Math.Abs(alfa) > Math.Abs(uhly[uhly.Count - 1])) || (Math.Abs(alfa) > Math.Abs(uhly[0]) && Math.Abs(alfa) < Math.Abs(uhly[uhly.Count - 1])))
							return true;
					}
					else
					{
						if (alfa > 0 && uhly[0] > 0)
						{
							if ((Math.Abs(alfa) < Math.Abs(uhly[0]) && Math.Abs(alfa) > Math.Abs(uhly[uhly.Count - 1])) || (Math.Abs(alfa) > Math.Abs(uhly[0]) && Math.Abs(alfa) < Math.Abs(uhly[uhly.Count - 1])))
								return true;
						}
					}

				}
				else
				{
					for (int i = 0; i < uhly.Count; i++)
					{
						uhly[i] = PositiveAngle(uhly[i]);
					}
					uhly.Sort();
					float konecnaAlfa = PositiveAngle(alfa);
					if (konecnaAlfa < uhly[uhly.Count - 1] && konecnaAlfa > uhly[0])
						return true;
				}
			}
			return false;
		}
		public static Vector2 RotatePoint(Vector2 zaklad, Vector2 bod, float alfa)
		{
			if (alfa % MathHelper.TwoPi != 0)
			{
				float vzdalenost = Vector2.Distance(zaklad, bod);
				alfa += MeasureAngle(zaklad, bod);
				return zaklad + new Vector2((float)Math.Cos(alfa) * vzdalenost, (float)Math.Sin(alfa) * vzdalenost);
			}
			else
			{
				return bod;
			}
		}
		public static Vector2 RotatePoint(Vector2 bod, float alfa)
		{
			if (alfa % MathHelper.TwoPi != 0)
			{
				float vzdalenost = bod.Length();
				alfa += MeasureAngle(bod);
				return new Vector2((float)Math.Cos(alfa) * vzdalenost, (float)Math.Sin(alfa) * vzdalenost);
			}
			else
			{
				return bod;
			}
		}
		public static Vector2 RotatePoint(float x, float alfa)
		{
			Vector2 bod = new Vector2(x, 0);
			if (alfa % MathHelper.TwoPi != 0)
			{
				float vzdalenost = bod.Length();
				alfa += MeasureAngle(bod);
				return new Vector2((float)Math.Cos(alfa) * vzdalenost, (float)Math.Sin(alfa) * vzdalenost);
			}
			else
			{
				return bod;
			}
		}
		public static float PositiveAngle(float uhel)
		{
			if (uhel < 0)
			{
				return (float)Math.PI * 2 + uhel;
			}
			else
				return uhel;
		}
		public static float GetShorterAngleDiff(float fromAngleA, float toAngleB)
		{
			if (fromAngleA == toAngleB) return 0;

			float angleDiff = toAngleB - fromAngleA;
			return MathHelper.WrapAngle(angleDiff);
		}
		#endregion

		public static Vector2 Reposition(Vector2 origin, float distance, float angle)
		{
			return origin + RotatePoint(Vector2.Zero, new Vector2(0, distance), angle);
		}
		public static Vector2 Reposition(float xPosition, float angle)
		{
			return RotatePoint(Vector2.Zero, new Vector2(0, xPosition), angle);
		}
		public static List<int> GetCiphers(int number)
		{
			List<int> ciphers = new List<int>();

			while (((float)number) >= 1f)
			{
				ciphers.Add(number - (number / 10) * 10);
				number /= 10;
			}

			if (ciphers.Count == 0)
			{
				ciphers.Add(0);
			}

			return ciphers;
		}

		#region Values edit
		#region Linear value adjusters
		public static float AdjustValue(float hodnota, float rychlostZmeny, float hodnotaKDosazeni, bool cut = true)
		{
			rychlostZmeny *= (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
			if (hodnota != hodnotaKDosazeni)
			{
				if (hodnota < hodnotaKDosazeni)
				{
					hodnota += rychlostZmeny;
					if (cut)
						if (hodnota > hodnotaKDosazeni)
						{
							hodnota = hodnotaKDosazeni;
						}
				}
				else
				{
					hodnota -= rychlostZmeny;
					if (cut)
						if (hodnota < hodnotaKDosazeni)
						{
							hodnota = hodnotaKDosazeni;
						}
				}
			}

			return hodnota;
		}
		public static Vector2 AdjustValue(Vector2 hodnota, Vector2 rychlostZmeny, Vector2 hodnotaKDosazeni, bool cut = true)
		{
			hodnota.X = AdjustValue(hodnota.X, rychlostZmeny.X, hodnotaKDosazeni.X, cut);
			hodnota.Y = AdjustValue(hodnota.Y, rychlostZmeny.Y, hodnotaKDosazeni.Y, cut);
			return hodnota;
		}
		public static void AdjustValue(ref Vector2 hodnota, Vector2 rychlostZmeny, Vector2 hodnotaKDosazeni, bool cut = true)
		{
			hodnota.X = AdjustValue(hodnota.X, rychlostZmeny.X, hodnotaKDosazeni.X, cut);
			hodnota.Y = AdjustValue(hodnota.Y, rychlostZmeny.Y, hodnotaKDosazeni.Y, cut);
		}
		public static void AdjustValue(ref float hodnota, float rychlostZmeny, float hodnotaKDosazeni)
		{
			hodnota = AdjustValue(hodnota, rychlostZmeny, hodnotaKDosazeni);
		}
		public static Color AdjustValue(Color color, float colorChangeSpeed, Color colorToGo)
		{
			colorChangeSpeed *= (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
			if (colorToGo != color)
			{
				#region Red (R)
				if (color.R > colorToGo.R)
				{
					if (color.R - colorChangeSpeed < color.R)
						color.R -= (byte)colorChangeSpeed;
					else
						color.R = 0;

					if (color.R < colorToGo.R)
					{
						color.R = colorToGo.R;
					}
				}
				else
				if (color.R < colorToGo.R)
				{
					if (color.R + colorChangeSpeed > color.R)
					{
						float newValue = colorChangeSpeed + color.R;
						if (newValue > 255)
							newValue = 255;
						color.R = (byte)newValue;
					}
					else
						color.R = 255;

					if (color.R > colorToGo.R)
					{
						color.R = colorToGo.R;
					}
				}
				#endregion
				#region Green (G)
				if (color.G > colorToGo.G)
				{
					if (color.G - colorChangeSpeed < color.G)
						color.G -= (byte)colorChangeSpeed;
					else
						color.G = 0;

					if (color.G < colorToGo.G)
					{
						color.G = colorToGo.G;
					}
				}
				else
				if (color.G < colorToGo.G)
				{
					if (color.G + colorChangeSpeed > color.G)
					{
						float newValue = colorChangeSpeed + color.G;
						if (newValue > 255)
							newValue = 255;
						color.G = (byte)newValue;
					}
					else
						color.G = 255;

					if (color.G > colorToGo.G)
					{
						color.G = colorToGo.G;
					}
				}
				#endregion
				#region Blue (B)
				if (color.B > colorToGo.B)
				{
					if (color.B - colorChangeSpeed < color.B)
						color.B -= (byte)colorChangeSpeed;
					else
						color.B = 0;

					if (color.B < colorToGo.B)
					{
						color.B = colorToGo.B;
					}
				}
				else
				if (color.B < colorToGo.B)
				{
					if (color.B + colorChangeSpeed > color.B)
					{
						float newValue = colorChangeSpeed + color.B;
						if (newValue > 255)
							newValue = 255;
						color.B = (byte)newValue;
					}
					else
						color.B = 255;

					if (color.B > colorToGo.B)
					{
						color.B = colorToGo.B;
					}
				}
				#endregion
			}

			return color;
		}
		#endregion

		public static float AdjustCircularValue(float hodnota, float rychlostZmeny, float hodnotaKDosazeni)
		{
			rychlostZmeny *= (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;

			if (hodnotaKDosazeni == hodnota) return hodnota;

			if (IsAngleBetween(hodnotaKDosazeni, hodnota, hodnota - (float)Math.PI - 0.0001f))
			{
				hodnota += rychlostZmeny;
				if (!IsAngleBetween(hodnotaKDosazeni, hodnota, hodnota - (float)Math.PI - 0.0001f))
				{
					hodnota = hodnotaKDosazeni;
				}
			}
			else
			{
				hodnota -= rychlostZmeny;
				if (IsAngleBetween(hodnotaKDosazeni, hodnota, hodnota - (float)Math.PI - 0.0001f))
				{
					hodnota = hodnotaKDosazeni;
				}
			}
			return hodnota;
		}
		#endregion

		public static float SelectHighest(List<float> hodnoty)
		{
			float nejvyssi = hodnoty[0];
			foreach (float cislo in hodnoty)
			{
				if (cislo > nejvyssi)
					nejvyssi = cislo;
			}
			return nejvyssi;
		}
		public static float SelectLowest(List<float> hodnoty)
		{
			float nejnizsi = hodnoty[0];
			foreach (float cislo in hodnoty)
			{
				if (cislo < nejnizsi)
					nejnizsi = cislo;
			}
			return nejnizsi;
		}

		public static bool IsNumberIndistance(float cislo, float start, float konec)
		{
			if (cislo > start && cislo < konec)
				return true;
			return false;
		}

		public static int ZaokrouhlitCislo(float cislo)
		{
			if (cislo > 0)
			{
				if (cislo - (int)cislo < 0.5f)
				{
					return (int)cislo;
				}
				else
				{
					return (int)cislo + 1;
				}
			}
			else
			{
				if (cislo - (int)cislo < -0.5f)
				{
					return (int)cislo - 1;
				}
				else
				{
					return (int)cislo;
				}
			}
		}
		public static Vector2 ZaokrouhlitCislo(Vector2 cislo)
		{
			return new Vector2(ZaokrouhlitCislo(cislo.X), ZaokrouhlitCislo(cislo.Y));
		}
		public static Vector2 ZaokrouhlitCislo(Vector2 cislo, bool nahoru)
		{
			return new Vector2(ZaokrouhlitCislo(cislo.X, nahoru), ZaokrouhlitCislo(cislo.Y, nahoru));
		}
		public static int ZaokrouhlitCislo(float cislo, bool nahoru)
		{
			if (nahoru)
			{
				int intCis = (int)cislo;
				if (cislo == intCis)
				{
					return intCis;
				}
				else
				{
					return intCis + 1;
				}
			}
			else
			{
				int intCis = (int)cislo;

				if (intCis > cislo)
				{
					return intCis - 1;
				}

				return intCis;
			}
		}

		public static int BezpZaokrouhlitCisloDolu(float cislo)
		{
			int cisloInt = (int)cislo;
			if (cislo > cisloInt)   //Vse je v poradku
			{
				return cisloInt;
			}
			else
			{       //Chybne zaokrouhleni
				return cisloInt - 1;
			}
		}

		#region Collisions
		public static bool CollisionPointAndRectangle(Vector2 pointPosition, Vector2 rectanglePosition, float rectangleRotation, Vector2 rectangleSize)
		{
			Vector2 relativniPoziceBodu;
			if (rectangleRotation != 0)
			{
				relativniPoziceBodu = RotatePoint(rectanglePosition, pointPosition, -rectangleRotation);
			}
			else
			{
				relativniPoziceBodu = pointPosition;
			}

			if (rectanglePosition.X - rectangleSize.X / 2 < relativniPoziceBodu.X && rectanglePosition.X + rectangleSize.X / 2 > relativniPoziceBodu.X &&
				rectanglePosition.Y - rectangleSize.Y / 2 < relativniPoziceBodu.Y && rectanglePosition.Y + rectangleSize.Y / 2 > relativniPoziceBodu.Y)
				return true;
			return false;
		}
		public static bool CollisionRectangleInScreen(Vector2 rectanglePosition, Vector2 rectangleSize, Vector2 screenSize)
		{
			if ((rectanglePosition.X + rectangleSize.X / 2) > 0 && (rectanglePosition.X - rectangleSize.X / 2) < screenSize.X &&
				(rectanglePosition.Y + rectangleSize.Y / 2) > 0 && (rectanglePosition.Y - rectangleSize.Y / 2) < screenSize.Y)
			{
				return true;
			}
			return false;
		}
		public static bool CollisionRectangleInScreen(Vector2 rectanglePosition, Vector2 rectangleSize, Vector2 screenSize, Vector2 viewMove, float zoom)
		{
			if ((rectanglePosition.X * zoom + viewMove.X + rectangleSize.X * zoom / 2) > 0 && (rectanglePosition.X * zoom + viewMove.X - rectangleSize.X * zoom / 2) < screenSize.X &&
				(rectanglePosition.Y * zoom + viewMove.Y + rectangleSize.Y * zoom / 2) > 0 && (rectanglePosition.Y * zoom + viewMove.Y - rectangleSize.Y * zoom / 2) < screenSize.Y)
			{
				return true;
			}
			return false;
		}
		public static bool CollisionCircleInScreen(Vector2 circlePosition, float circleR, Vector2 screenSize)
		{
			Vector2 screenMidPos = screenSize / 2;
			double screenRadius = (double)Math.Sqrt((screenSize.X * screenSize.X + screenSize.Y * screenSize.Y)) / 2;
			if (CollisionRectangleInScreen(circlePosition, new Vector2(circleR * 2), screenSize))
			{
				if (Vector2.Distance(screenMidPos, circlePosition) <= circleR + screenRadius)
				{
					return true;
				}
			}
			return false;

		}
		public static bool CollisionRectangleAndRectangle(Vector2 rectanglePosition1, Vector2 rectangleSize1, Vector2 rectanglePosition2, Vector2 rectangleSize2)
		{
			if ((rectanglePosition1.X + rectangleSize1.X / 2) > rectanglePosition2.X - rectangleSize2.X / 2 && (rectanglePosition1.X - rectangleSize1.X / 2) < rectanglePosition2.X + rectangleSize2.X / 2 &&
				(rectanglePosition1.Y + rectangleSize1.Y / 2) > rectanglePosition2.Y - rectangleSize2.Y / 2 && (rectanglePosition1.Y - rectangleSize1.Y / 2) < rectanglePosition2.Y + rectangleSize2.Y / 2)
			{
				return true;
			}
			return false;
		}
		public static bool CollisionRectangleAndCircle(Vector2 rectanglePosition, Vector2 rectangleSize, Vector2 positionCircle, float rCircle)
		{
			if (CollisionRectangleAndRectangle(rectanglePosition, rectangleSize, positionCircle, new Vector2(rCircle * 2)))
			{
				float squareAsCircRatio = rectangleSize.Length();
				if (CollisionCircleAndCircle(rectanglePosition, squareAsCircRatio, positionCircle, rCircle))
					return true;

				return false;
			}
			return false;
		}
		public static bool CollisionRectangleAndCircle(Vector2 rectanglePosition, Vector2 rectangleSize, float rectangleRotation, Vector2 circlePosition, float circleR)
		{
			Vector2 noRotatedPositionSq = RotatePoint(circlePosition, rectanglePosition, -rectangleRotation);
			if (CollisionRectangleAndRectangle(noRotatedPositionSq, rectangleSize, circlePosition, new Vector2(circleR * 2)))
			{
				float squareAsCircRatio = rectangleSize.Length();
				if (CollisionCircleAndCircle(rectanglePosition, squareAsCircRatio, circlePosition, circleR))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}
		public static bool CollisionCircleAndCircle(Vector2 circlePosition1, float circleR1, Vector2 circlePosition2, float circleR2)
		{
			if (Vector2.Distance(circlePosition1, circlePosition2) < circleR1 + circleR2)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public static bool CollisionPointAndCircle(Vector2 pointPosition, Vector2 circlePosition, float circleR)
		{
			return Vector2.Distance(pointPosition, circlePosition) <= circleR;
		}
		public static bool CollisionLineAndLine(Vector2 startPosition1, Vector2 endPosition1, Vector2 startPosition2,
			Vector2 endPosition2)
		{
			float denominator = ((endPosition1.X - startPosition1.X) * (endPosition2.Y - startPosition2.Y)) - ((endPosition1.Y - startPosition1.Y) * (endPosition2.X - startPosition2.X));
			float numerator1 = ((startPosition1.Y - startPosition2.Y) * (endPosition2.X - startPosition2.X)) - ((startPosition1.X - startPosition2.X) * (endPosition2.Y - startPosition2.Y));
			float numerator2 = ((startPosition1.Y - startPosition2.Y) * (endPosition1.X - startPosition1.X)) - ((startPosition1.X - startPosition2.X) * (endPosition1.Y - startPosition1.Y));

			// Detect coincident lines (has a problem, read below)
			if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

			float r = numerator1 / denominator;
			float s = numerator2 / denominator;

			return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
		}

		public static bool CollisionLineAndRectangle(Vector2 lineStartPosition, Vector2 lineEndPosition, Vector2 rectanglePosition, Vector2 rectangleSize)
		{
			// Right, Bottom, Top, Left
			return CollisionLineAndLine(lineStartPosition, lineEndPosition,
					   rectanglePosition + new Vector2(rectangleSize.X, rectangleSize.Y) / 2,
					   rectanglePosition + new Vector2(rectangleSize.X, -rectangleSize.Y) / 2) ||
				   CollisionLineAndLine(lineStartPosition, lineEndPosition,
					   rectanglePosition + new Vector2(rectangleSize.X, rectangleSize.Y) / 2,
					   rectanglePosition + new Vector2(-rectangleSize.X, rectangleSize.Y) / 2) ||
				   CollisionLineAndLine(lineStartPosition, lineEndPosition,
					   rectanglePosition + new Vector2(-rectangleSize.X, -rectangleSize.Y) / 2,
					   rectanglePosition + new Vector2(rectangleSize.X, -rectangleSize.Y) / 2) ||
				   CollisionLineAndLine(lineStartPosition, lineEndPosition,
					   rectanglePosition + new Vector2(-rectangleSize.X, -rectangleSize.Y) / 2,
					   rectanglePosition + new Vector2(-rectangleSize.X, rectangleSize.Y) / 2);
		}
		#endregion

		public static bool IsNumber(string text)
		{
			float number;
			bool result = float.TryParse(text, out number);
			if (result)
			{
				return true;
			}
			return false;
		}
		public static string GetTextFromColor(Color color)
		{
			return color.R + ";" + color.G + ";" + color.B;
		}
		public static Color GetColorFromText(string text)
		{
			string[] colors = text.Split(';');
			float r = float.Parse(colors[0]);
			float g = float.Parse(colors[1]);
			float b = float.Parse(colors[2]);
			Color newColor = new Color();
			newColor.R = Convert.ToByte(r);
			newColor.G = Convert.ToByte(g);
			newColor.B = Convert.ToByte(b);
			newColor.A = 255;
			return newColor;
		}
		public static bool IsColor(string text)
		{
			string[] colors = text.Split(';');
			if (colors.Length == 3)
			{
				foreach (string color in colors)
				{
					if (IsNumber(color))
					{
						float number = float.Parse(color);
						if (number > 255 || number < 0)
							return false;
					}
					else
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
		public static void AppendToText(StringBuilder sb, string desc, string value)
		{
			sb.Append(desc + "=" + '"' + value + '"');
		}
		public static string VectorToText(Vector2 vector)
		{
			return vector.X + ";" + vector.Y;
		}
		public static Vector2 TextToVector(string text)
		{
			string[] values = text.Split(';');
			if (IsVector(text))
				return new Vector2(GetFloat(values[0]), GetFloat(values[1]));
			return Vector2.Zero;
		}
		public static string GetNumericForm(string text)
		{
			return text.Replace(',', '.');
		}
		public static float GetFloat(string text)
		{
			return float.Parse(GetNumericForm(text), System.Globalization.CultureInfo.InvariantCulture);
		}
		public static bool IsVector(string text)
		{
			string[] values = text.Split(';');
			if (values.Length == 2)
			{
				if (IsNumber(values[0]) && IsNumber(values[1]))
				{
					return true;
				}
			}
			return false;
		}

		public static Vector2 GetTexture2DSize(Texture2D texture)
		{
			return new Vector2(texture.Width, texture.Height);
		}

		public static Vector2 Abs(Vector2 vector)
		{
			return new Vector2(Math.Abs(vector.X), Math.Abs(vector.Y));
		}

		public static float GetLower(float num1, float num2)
		{
			if (num2 < num1)
				return num2;
			else
				return num1;
		}
		public static float GetHigher(float num1, float num2)
		{
			if (num2 > num1)
				return num2;
			else
				return num1;
		}

		public static float GetLower(Vector2 nums)
		{
			return GetLower(nums.X, nums.Y);
		}

		public static float GetAbsoluteHigher(float num1, float num2)
		{
			if (Math.Abs(num1) >= Math.Abs(num2))
			{
				return num1;
			}
			else
			{
				return num2;
			}
		}
		public static int GetLower(int num1, int num2)
		{
			if (num2 < num1)
				return num2;
			else
				return num1;
		}
		public static int GetHigher(int num1, int num2)
		{
			if (num2 > num1)
				return num2;
			else
				return num1;
		}
		public static Vector2 PointToVector2(Point point)
		{
			return new Vector2(point.X, point.Y);
		}
		public static Point Vector2ToPoint(Vector2 vector)
		{
			return new Point((int)vector.X, (int)vector.Y);
		}

		public static float GetDistPointAndRect(Vector2 positionRect, Vector2 sizeRect, float rotationRect, Vector2 positionPoint)
		{
			//neplati kdyz je kruh mimo pres roh!
			Vector2 rotatedPosCirc = RotatePoint(positionRect, positionPoint, -rotationRect);

			Vector2 diff = Abs(Abs(positionRect - rotatedPosCirc) - sizeRect);

			return GetLower(diff.X, diff.Y);
		}

		//public static bool IsTouchReady(MyTouch touch)
		//{
		//	return touch != null && (touch.State == TouchState.Continuing || touch.State == TouchState.JustPressed);
		//}

		#region Cut value
		public static Vector2 CutVector2(Vector2 vector, float dist)
		{
			float vectorLenght = vector.Length();

			if (dist < vectorLenght)
			{
				//Needs to be cutted!
				float longConst = dist / vectorLenght;
				return vector * longConst;
			}
			else
			{
				return vector;
			}
		}

		public static T CutValue<T>(T value, T min, T max) where T : IComparable
		{
			if (value.CompareTo(max) > 0)
				return max;

			if (value.CompareTo(min) < 0)
				return min;

			return value;
		}

		public static float CutToByte(float value)
		{
			return CutValue(value, 0, 255);
		}
		#endregion

		public static int GetMark(double num)
		{
			return num.CompareTo(0);
		}

		/// <summary>
		/// Changes Y component of the vector to the given value and edits X component to save the proportion of the vector.
		/// </summary>
		/// <param name="vectorToScale">The vector which will be editted.</param>
		/// <param name="newY">New value of Y component of the vector.</param>
		/// <returns>Vector with height newY and width scalled according to the scale of height</returns>
		public static Vector2 ScaleByY(Vector2 vectorToScale, float newY)
		{
			float scale = newY / vectorToScale.Y;
			return new Vector2(vectorToScale.X * scale, newY);
		}

		public static Vector2 ScaleByX(Vector2 vectorToScale, float newX)
		{
			float scale = newX / vectorToScale.X;
			return new Vector2(newX, vectorToScale.Y * scale);
		}

		public static Color HslToRgb(double h, double sl, double l)
		{
			double v;
			double r, g, b;

			r = l;   // default to gray
			g = l;
			b = l;
			v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
			if (v > 0)
			{
				double m;
				double sv;
				int sextant;
				double fract, vsf, mid1, mid2;

				m = l + l - v;
				sv = (v - m) / v;
				h *= 6.0;
				sextant = (int)h;
				fract = h - sextant;
				vsf = v * sv * fract;
				mid1 = m + vsf;
				mid2 = v - vsf;
				switch (sextant)
				{
					case 0:
						r = v;
						g = mid1;
						b = m;
						break;
					case 1:
						r = mid2;
						g = v;
						b = m;
						break;
					case 2:
						r = m;
						g = v;
						b = mid1;
						break;
					case 3:
						r = m;
						g = mid2;
						b = v;
						break;
					case 4:
						r = mid1;
						g = m;
						b = v;
						break;
					case 5:
						r = v;
						g = m;
						b = mid2;
						break;
				}
			}

			Color rgb = new Color
			{
				R = Convert.ToByte(r * 255.0),
				G = Convert.ToByte(g * 255.0),
				B = Convert.ToByte(b * 255.0),
				A = 255
			};
			return rgb;
		}

		public static void RgbToHsl(Color rgb, out double h, out double s, out double l)
		{
			double r = rgb.R / 255D;
			double g = rgb.G / 255D;
			double b = rgb.B / 255D;
			double v;
			double m;
			double vm;
			double r2, g2, b2;

			h = 0; // default to black
			s = 0;
			l = 0;
			v = Math.Max(r, g);
			v = Math.Max(v, b);
			m = Math.Min(r, g);
			m = Math.Min(m, b);
			l = (m + v) / 2.0;
			if (l <= 0.0)
			{
				return;
			}
			vm = v - m;
			s = vm;
			if (s > 0.0)
			{
				s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
			}
			else
			{
				return;
			}
			r2 = (v - r) / vm;
			g2 = (v - g) / vm;
			b2 = (v - b) / vm;
			if (r == v)
			{
				h = (g == m ? 5.0 + b2 : 1.0 - g2);
			}
			else if (g == v)
			{
				h = (b == m ? 1.0 + r2 : 3.0 - b2);
			}
			else
			{
				h = (r == m ? 3.0 + g2 : 5.0 - r2);
			}
			h /= 6.0;
		}
	}
}
