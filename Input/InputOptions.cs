using GameEngine.Input.TouchPanel;
using Microsoft.Xna.Framework;

namespace GameEngine.Input
{
	public static class InputOptions
	{
		public static MyState MyState { get; } = new MyState();

		public static void Update(GameTime gameTime, bool isActive)
		{
			MyState.Actualise(isActive);
		}
	}
}
