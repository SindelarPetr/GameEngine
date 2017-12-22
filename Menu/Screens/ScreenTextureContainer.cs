using System;
using GameEngine.CameraEngine;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.Screens
{
	/// <summary>
	/// Adds a texture into screen container - most of controls have some "background" which this class provides. The texture has draw order of 3.
	/// </summary>
	public class ScreenTextureContainer : ScreenBag
	{
		protected ScreenTexture ScreenTexture;

		public ScreenTextureContainer(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider,
			IScreenParentObject parent = null, MyTexture2D texture = null)
			: base(camera, positionProvider, sizeProvider, parent)
		{
			ScreenTexture = CreateTexture(texture);

		}

		public ScreenTextureContainer(Camera camera, Vector2 position, Vector2 size, IScreenParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent)
		{
			ScreenTexture = CreateTexture(texture);
		}

		ScreenTexture CreateTexture(MyTexture2D texture)
		{
			var screenTexture = new ScreenTexture(Camera, Vector2.Zero, BasicSize, this, texture);
			screenTexture.ReferenceVisuals(this);
			AddNestedObject(screenTexture, 3);
			OnBasicSizeChanged += newSize => screenTexture.BasicSize = newSize;
			//OnBasicPositionChanged += newPosition => screenTexture.BasicPosition = newPosition;
			return screenTexture;
		}
	}
}
