using GameEngine.CameraEngine;
using GameEngine.Properties;
using System;
using Microsoft.Xna.Framework;

namespace GameEngine.Primitives
{
	/// <summary>
	/// This object adds the posibility of destroying and removing objects
	/// </summary>
	public class GameObject : TextureObject, IGameElement
	{
		/// <summary>
		/// Is fired when the object disappears from the map.
		/// </summary>
		public event EventHandler<IGameElement> OnRemoving;
		public event EventHandler<IGameElement> OnDestroying;

		public GameObject(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture)
		{

		}

		/// <summary>
		/// Will remove the object without effects.
		/// </summary>
		protected virtual void Remove()
		{
			OnRemoving?.Invoke(this, this);
		}

		/// <summary>
		/// Will remove the object with effects.
		/// </summary>
		public virtual void Destroy()
		{
			OnDestroying?.Invoke(this, this);
			Remove();
		}
	}
}