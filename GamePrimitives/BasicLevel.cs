using GameEngine.CameraEngine;
using GameEngine.Menu.Screens;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.GamePrimitives
{
	public class BasicLevel : ScreenBag
	{
		private ContainerGameObject _effects;

		public BasicLevel(Camera camera, IScreenParentObject parent = null)
			: base(camera, Vector2.Zero, Vector2.Zero, parent)
		{

		}

		public void AddEffect(IGameElement effect)
		{
			_effects.
		}


	}
}
