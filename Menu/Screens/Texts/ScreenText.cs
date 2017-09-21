using GameEngine.CameraEngine;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Menu
{
	public class ScreenText : TextObject, IMenuScreenElement
	{
		private Func<Vector2> _positionProvider;
		private Func<float> _heightProvider;

		public Func<Vector2> PositionProvider
		{
			get => _positionProvider;
			set
			{
				_positionProvider = value;

				if (_positionProvider != null)
					BasicPosition = _positionProvider.Invoke();
			}
		}

		public Func<float> HeightProvider
		{
			get => _heightProvider;
			set
			{
				_heightProvider = value;

				if (_heightProvider != null)
					ScaleHeight(_heightProvider.Invoke());
			}
		}


		public ScreenText(Camera camera, SpriteFont font, string text, Func<Vector2> positionProvider, Func<float> heightProvider = null,
			IParentObject parent = null)
			: this(camera, font, text, positionProvider?.Invoke() ?? Vector2.Zero, heightProvider?.Invoke(), parent)
		{
			_positionProvider = positionProvider;
			_heightProvider = heightProvider;
		}

		public ScreenText(Camera camera, SpriteFont font, string text, Vector2 position, float? height = null, IParentObject parent = null)
			: base(camera, font, text, position, height ?? font.MeasureString(text).Y, parent)
		{
		}

		public void Hide()
		{

		}

		public void Show(IMenuScreenElement showInitializator)
		{

		}

		public void AllScreensLoaded()
		{
			
		}

		public void LooseTouches()
		{

		}

		public virtual void ResolutionChanged()
		{
			if (PositionProvider != null)
				BasicPosition = PositionProvider.Invoke();

			if (HeightProvider != null)
				ScaleHeight(HeightProvider.Invoke());
		}
	}
}
