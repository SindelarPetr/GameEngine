using Microsoft.Xna.Framework;

namespace GameEngine.Primitives
{
	public interface IParentObject
	{
		Vector2 GetGamePosition();
		Vector2 GetGameScale();
		float GetGameRotation();
		float GetGameOpacity();
	}
}
