using GameEngine.ObjectPrimitives;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.GamePrimitives
{
	/// <summary>
	/// This object adds the posibility of destroying and removing objects
	/// </summary>
	public class GameObject : TextureObject, IGameElement
	{
		public BasicLevel Level { get; }

		/// <summary>
		/// Is fired when the object should dissapear from the map.
		/// </summary>
		public event EventHandler<IGameElement> OnRemoving;
		public event EventHandler<IGameElement> OnDestroying;

		public GameObject(BasicLevel level, Vector2 position, Vector2 size, IWorldObject parent = null, MyTexture2D texture = null)
			: base(level.Camera, position, size, parent ?? level, texture)
		{
			Level = level;
		}

		/// <summary>
		/// Removes the object without effects.
		/// </summary>
		protected virtual void Remove()
		{
			OnRemoving?.Invoke(this, this);
		}

		/// <summary>
		/// Makes destruction effects and removes it.
		/// </summary>
		public virtual void Destroy()
		{
			OnDestroying?.Invoke(this, this);
			Remove();
		}
	}
}