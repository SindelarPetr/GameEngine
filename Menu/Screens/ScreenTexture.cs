using GameEngine.CameraEngine;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Menu.Screens;

namespace GameEngine.Menu.ScreensAs
{
	public class ScreenTexture : TextureObject, IMenuScreenElement
	{
		protected readonly Func<Vector2> PositionProvider;
		protected readonly Func<Vector2> SizeProvider;

		public ScreenTexture(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, IParentObject parent = null, MyTexture2D texture = null)
			: this(camera, positionProvider.Invoke(), sizeProvider.Invoke(), parent, texture)
		{
			PositionProvider = positionProvider;
			SizeProvider = sizeProvider;
		}

		public ScreenTexture(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture) { }


		public void AllScreensLoaded()
		{
			
		}

		public void LooseTouches()
		{

		}

		public void Show(IMenuScreenElement showInitializator)
		{
			
		}

		public void Hide()
		{

		}

		public virtual void ResolutionChanged()
		{
			if (PositionProvider != null)
				BasicPosition = PositionProvider.Invoke();

			if (SizeProvider != null)
				BasicSize = SizeProvider.Invoke();
		}
	}
}
