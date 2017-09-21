using GameEngine.CameraEngine;
using GameEngine.Menu.Screens;
using GameEngine.Primitives;
using GameEngine.ValueHolders;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.ScreensAs.Buttons
{
	public class ScreenButtonSignalizing : ScreenTextButton
	{
		protected SignalValueUser SignalOpacity;

		public override float GetLocalOpacity()
		{
			return base.GetLocalOpacity() * SignalOpacity;
		}

		public ScreenButtonSignalizing(Camera camera, Vector2 position, Vector2 size, string text, IScreenParentObject parent = null)
			: base(camera, position, size, text, parent)
		{
			SignalOpacity = CreateSignalOpacity();
		}

		SignalValueUser CreateSignalOpacity()
		{
			var signal = new SignalValueUser(0.3f, 1, 1);
			signal.BasicSpeed = 0.0001f;
			signal.Friction = 0.004f;
			return signal;
		}

		public override void Update()
		{
			base.Update();

			SignalOpacity.Update();
		}
	}
}
