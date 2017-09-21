using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Options
{
	/// <summary>
	/// Type of controls used in this game - Touch options inclues Mouse control (because in windows is touch on the screen meant even by click on the mouse).
	/// </summary>
	public enum PlayerControlType { Touch }

	/// <summary>
	/// Types of platforms this app is able to run on (Windows is more limitting by UWP, but Windows is easyer to debug...)
	/// </summary>
	public enum Platform { Windows, Uwp, Android, Ios }

	/// <summary>
	/// There will be 2 types of screens this app will run on
	/// - Small	- Mobile phones
	/// - Big	- Computers, notebooks, tablets	
	/// </summary>
	public enum PresentationScreen { Small, Big }

	/// <summary>
	/// This class stores all settings in the game.
	/// </summary>
	public static class GeneralOptions
	{
		public static ContentManager Content { get; private set; }
		public static GameTime GameTime { get; private set; }
		public static Platform Platform { get; private set; }
		public static bool UseMouse { get; set; }
		public static Color BackgroundColor { get; set; } = Color.Black;
		public static GraphicsDevice GraphicsDevice { get; private set; }

		public static void Load(Game game, Platform platform)
		{
			Content = game.Content;
			Platform = platform;
			GraphicsDevice = game.GraphicsDevice;
		}

		/// <summary>
		/// Updates "frame specific" elements - GameTime and MyState.
		/// </summary>
		/// <param name="gameTime">Time of the current frame.</param>
		/// <param name="isActive">Indicator whether the game window is focused.</param>
		public static void Update(GameTime gameTime)
		{
			GameTime = gameTime;
		}
	}
}