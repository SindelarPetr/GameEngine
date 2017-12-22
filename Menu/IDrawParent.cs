using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu
{
	public interface IDrawParent
	{
		float Opacity { get; }
		MyColor Color { get; }
		Vector2 Position { get; }
		Vector2 Size { get; }
	}
}
