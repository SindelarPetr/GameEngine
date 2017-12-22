using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.ObjectPrimitives
{
	/// <summary>
	/// This class provides basic functionality for an object in the app.
	/// </summary>
	public abstract class PrimitiveObject : IPrimitiveObject
	{
		/// <summary>
		/// Counts the position in the game world.
		/// </summary>
		/// <returns>Position in the game world.</returns>
		public virtual Vector2 GetWorldPosition() => Parent?.GetWorldPosition() ?? Vector2.Zero;

		/// <summary>
		/// Counts the scale which should be applied to the object in the game world.
		/// </summary>
		/// <returns>The scale of the object in the game world.</returns>
		public virtual Vector2 GetWorldScale() => Parent?.GetWorldScale() ?? Vector2.One;

		/// <summary>
		/// Counts the rotation of the object in the game world.
		/// </summary>
		/// <returns>The rotation in the game world.</returns>
		public virtual float GetWorldRotation() => Parent?.GetWorldRotation() ?? 0;

		/// <summary>
		/// Counts the opacity of the object in the game world.
		/// </summary>
		/// <returns>The opacity of the object in the game world.</returns>
		public virtual float GetWorldOpacity() => Parent?.GetWorldOpacity() ?? 1;

		#region Parent
		/// <summary>
		/// An object which is superior for this object. This object calculates position, scale, opacity, rotation according to the Parent object. If the Parent is Null then neutral values are used (such as position = (0, 0), scale = (1, 1), opacity = 1, rotation = 0)
		/// </summary>
		protected IWorldObject Parent { get; private set; }

		public event Action<IWorldObject> OnParentChanging;
		public event Action<IWorldObject> OnParentChanged;
		public event Action<IWorldObject> OnParentRemoving;
		public event Action<IWorldObject> OnParentRemoved; 
		#endregion

		public PrimitiveObject(IWorldObject parent = null)
		{
			Parent = parent;
		}

		/// <summary>
		/// If there is different Parent now, removes it and sets newParent as Parent.
		/// </summary>
		/// <param name="newParent"></param>
		public void ChangeParent(IWorldObject newParent)
		{
			OnParentChanging?.Invoke(newParent);

			if (Parent != null)
			{
				RemoveParent();
			}

			Parent = newParent;

			OnParentChanged?.Invoke(Parent);
		}

		/// <summary>
		/// Removes the parent - sets its value to null.
		/// </summary>
		public void RemoveParent()
		{
			IWorldObject parent = Parent;
			OnParentRemoving?.Invoke(parent);

			Parent = null;

			OnParentRemoved?.Invoke(parent);
		}

		public abstract void Update();

		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
