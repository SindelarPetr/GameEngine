using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.PrimitiveObjects
{
	public interface IBaseObject : IPrimitiveObject
	{
		/// <summary>
		/// Counts the position of the object on the screen. It should be used for drawing so dont 
		/// modificate the returned value for drawing, because this method is used also for implementing touches.
		/// </summary>
		/// <returns>Position on the screen.</returns>
		Vector2 GetAbsolutePosition();

		/// <summary>
		/// Counts the local position of the object. Its position which is NOT affected by camera or parent
		/// </summary>
		/// <returns>The local position.</returns>
		Vector2 GetLocalPosition();
		
		Vector2 GetAbsoluteScale();
		Vector2 GetAbsoluteSize();
		Vector2 GetLocalScale();

		float GetAbsoluteRotation();

		float GetLocalRotation();

		/// <summary>
		/// This method calculates the independent opacity which is opacity applied only on texture of this object and nothing else.
		/// </summary>
		/// <returns>The calculated independent opacity.</returns>

		float GetLocalOpacity();

		float GetAbsoluteOpacity();
	}
}
