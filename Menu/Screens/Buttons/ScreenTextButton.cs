using System;
using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Menu.Screens.Texts;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Menu.Screens.Buttons
{

	public enum ButtonClickState { Default, Pressed }

	/// <summary>
	/// Adds text to the button. Text has draw order of 5.
	/// </summary>
	public class ScreenTextButton : ScreenButton
	{
		#region Static
		private static Vector2 CalculateTextSize(Vector2 spaceSize, Vector2 textSize)
		{
			if (textSize.X > spaceSize.X)
			{
				float scale = spaceSize.X / textSize.X;
				textSize *= scale;
			}

			if (textSize.Y > spaceSize.Y)
			{
				float scale = spaceSize.Y / textSize.Y;
				textSize *= scale;
			}

			return textSize;
		}
		#endregion

		public ScreenText Text;

		public ScreenTextButton(Camera camera, Vector2 position, Vector2 size, string text, IScreenParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture)
		{
			SpriteFont fnt = FontManager.AntigoniMed50;
			Text = new ScreenText(Camera, fnt, text, Vector2.Zero, CalculateTextSize(BasicSize - new Vector2(5), fnt.MeasureString(text)).Y, this);
			Text.ColorChanger.ResetColor(Color.White);

			AddNestedObject(Text, 5);
		}

		public ScreenTextButton(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, string text, Func<float> textHeightProvider = null, Func<Vector2> textPositionProvider = null, IScreenParentObject parent = null, MyTexture2D texture = null) : base(camera, positionProvider, sizeProvider, parent, texture)
		{
			SpriteFont fnt = FontManager.AntigoniMed50;
			Text = new ScreenText(Camera, fnt, text, textPositionProvider, textHeightProvider ?? (() => CalculateTextSize(BasicSize - new Vector2(5), fnt.MeasureString(text)).Y), this);
			Text.ColorChanger.ResetColor(Color.White);

			AddNestedObject(Text, 5);
		}

	}
}