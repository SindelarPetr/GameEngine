using GameEngine.CameraEngine;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.Screens
{
	public abstract class EmptyScreenObject : BaseObject, IScreenObject
	{
		public EmptyScreenObject(Camera camera, Vector2 position, Vector2 size, IWorldObject parent = null) : base(camera, position, size, parent)
		{

		}

		public virtual void AllScreensLoaded()
		{

		}

		public virtual void LooseTouches()
		{

		}

		public virtual void Show(IScreenObject showInitializator)
		{

		}

		public virtual void Hide()
		{

		}

		public virtual void ResolutionChanged()
		{

		}
	}
}
