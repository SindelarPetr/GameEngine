using Microsoft.Xna.Framework;

namespace GameEngine.ObjectPrimitives
{
	public interface IWorldObject
	{
		Vector2 GetWorldPosition();
		Vector2 GetWorldScale();
		float GetWorldRotation();
		float GetWorldOpacity();
	}
}
