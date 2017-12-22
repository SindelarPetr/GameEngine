using GameEngine.CameraEngine;
using GameEngine.ObjectPrimitives;
using GameEngine.ValueHolders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Menu.Screens.Texts
{
	public class ScreenBumpText : ScreenText
	{
		private readonly SmoothValue _bumpValue;
		protected float BumpPitch;

		public ScreenBumpText(Camera camera, SpriteFont font, string text, Vector2 position, float height, IWorldObject parent)
			: base(camera, font, text, position, height, parent)
		{
			_bumpValue = new SmoothValue(1);
			BumpPitch = 2.5f;
		}

		public void Bump()
		{
			_bumpValue.Value = BumpPitch;
		}

		public override Vector2 GetWorldScale()
		{
			return base.GetWorldScale() * _bumpValue.Value;
		}

		public override void Update()
		{
			base.Update();

			_bumpValue.Update();
		}
	}
}
