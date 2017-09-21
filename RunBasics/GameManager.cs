using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Input;
using GameEngine.MathEngine;
using GameEngine.Menu;
using GameEngine.Options;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.RunBasics
{
	/// <summary>
	/// This class outsources takes a care about all game logic
	/// </summary>
	public static class GameManager
	{
		private static Game _game;
		public static int DrawedObjectsCount { get; set; }
		private static TextObject _textObject;

		/// <summary>
		/// Load all the game content.
		/// </summary>
		/// <param name="game">Instance of game which calls this method.</param>
		/// <param name="platform">The platform program runs on.</param>
		public static void Load(Game game, Platform platform)
		{
			_game = game;

			GeneralOptions.Load(game, platform);

			TextureManager.LoadTextures();
			FontManager.LoadFonts();
			_textObject = new TextObject(new Camera(), FontManager.AntigoniMed50, "0", DisplayOptions.MiddleOfScreen * 0.8f);

			MenuScreenManager.LoadScreens();
			MenuScreenManager.AllScreensLoaded();
		}

		/// <summary>
		/// Update all game logic here. Firstly is updated options and then all menus (which includes even gameplay)
		/// </summary>
		/// <param name="gameTime">Duration of one frame.</param>
		/// <param name="isActive">Indicates whether the window with this game is focused.</param>
		public static void Update(GameTime gameTime, bool isActive)
		{
			GeneralOptions.Update(gameTime);
			InputOptions.Update(gameTime, isActive);

			MenuScreenManager.Update();
		}

		/// <summary>
		/// Draw all menus (including gameplay in a specific menu).
		/// </summary>
		/// <param name="spriteBatch">Element for drawing.</param>
		public static void Draw(SpriteBatch spriteBatch)
		{
			MenuScreenManager.Draw(spriteBatch);

#if DEBUG
			//_textObject.Content = DrawedObjectsCount.ToString();
			//_textObject.Draw(spriteBatch); 
#endif

			#region MousePointer
			if (GeneralOptions.UseMouse)
			{
				MouseState mouse = Mouse.GetState();
				Vector2 mousePointerSize = new Vector2(20);
				MyTexture2D pointerTexture = TextureManager.CircleR30;

				spriteBatch.Draw(pointerTexture.Texture, MyMath.PointToVector2(mouse.Position), null, Color.White, 0,
					pointerTexture.Middle, 1f * mousePointerSize / pointerTexture.Size, SpriteEffects.None, 0);
				spriteBatch.Draw(pointerTexture.Texture, MyMath.PointToVector2(mouse.Position), null, Color.Black, 0,
					pointerTexture.Middle, 0.5f * mousePointerSize / pointerTexture.Size, SpriteEffects.None, 0);
			}
			#endregion
		}

		/// <summary>
		/// Changes resolution of all elements in the game - just calls ResolutionChanged method of all elements in the game.
		/// </summary>
		/// <param name="newResolution">Value of resolution after change.</param>
		public static void ResolutionChanged(Vector2 newResolution)
		{
			DisplayOptions.ActualiseResolution(newResolution);

			MenuScreenManager.ResolutionChanged();
		}

		/// <summary>
		/// Quits the application - doesnt ask anything just terminates the game.
		/// </summary>
		public static void Exit()
		{
			_game.Exit();
		}
	}
}
