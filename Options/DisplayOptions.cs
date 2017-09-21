using Microsoft.Xna.Framework;

namespace GameEngine.Options
{
	public static class DisplayOptions
	{
		public static Vector2 Resolution { get; private set; }
		public static Vector2 MiddleOfScreen { get; private set; }

		/// <summary>
		/// The type of the screen for which should be adjusted sizes in the game.
		/// </summary>
		public static PresentationScreen PresentationScreen = PresentationScreen.Small;

		public static void ActualiseResolution(Vector2 newResolution)
		{
			Resolution = newResolution;
			MiddleOfScreen = newResolution / 2;
		}

	}
}
