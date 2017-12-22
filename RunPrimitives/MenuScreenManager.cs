using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Menu;
using GameEngine.Menu.Screens;
using Microsoft.Xna.Framework.Graphics;
using PCLExt.AppDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameEngine.RunPrimitives
{
	/// <summary>
	/// This class manages menu screens - when you want to add a new menu screen, you have to add it here.
	/// </summary>
	public static class MenuScreenManager
	{
		public static Camera Camera { get; private set; }

		private static Dictionary<Type, MenuScreen> _screens;
		private static List<MenuScreen> _orderedScreens;

		/// <summary>
		/// List of all screen instances.
		/// </summary>
		#region Events
		//public static event Action OnScreenLoading;
		//public static event Action OnAllScreensLoaded;
		//public static event Action OnUpdate;
		//public static event Action<SpriteBatch> OnDraw;
		//public static event Action OnResolutionChanged;
		#endregion

		/// <summary>
		/// Saves the given screen and takes a care about his updating, drawing, and changing resolution. Screens must be loaded in the right draw order (they are drawed in order in which they were added).
		/// </summary>
		internal static void LoadScreens()
		{
			// Get assemblies with MenuScreens
			var assembliesWithScreens = AppDomain.GetAssemblies().Where(a => a.GetCustomAttribute(typeof(GameLogicAssembly)) != null);

			// Get all types of subclasses of MenuScreen
			var types = assembliesWithScreens.SelectMany(a => a.DefinedTypes).Where(t => t.IsSubclassOf(typeof(MenuScreen))).ToList();

			Camera = new Camera();
			_screens = new Dictionary<Type, MenuScreen>();
			_orderedScreens = new List<MenuScreen>();

			MenuScreen mainScreen = null;
			// Create instances of all MenuScreens
			types.ForEach(t =>
			{
				MenuScreen screen = (MenuScreen)t.DeclaredConstructors.First().Invoke(new object[] { Camera });

				if (screen.GetType().GetTypeInfo().GetCustomAttribute(typeof(MainScreenAttribute)) != null)
				{
					mainScreen = screen;
				}

				AddScreen(screen);
			});

			if (mainScreen == null) throw new MissingMemberException("No MenuScreen has attribute MainScreenAttribute. Mark the MenuScreen, which should be showed as the first after start up, with the MainScreenAttribute attribute.");
			mainScreen.Show();
			_screens.Values.ForEach(p => p.AllScreensLoaded());
		}

		private static void AddScreen(MenuScreen screen)
		{
			_screens.Add(screen.GetType(), screen);
			_orderedScreens.Add(screen);
			_orderedScreens.Sort((screen1, screen2) => screen1.DrawPriorities.CompareTo(screen2.DrawPriorities));
		}

		public static T GetScreen<T>() where T : MenuScreen
		{
			return (T)_screens[typeof(T)];
		}

		internal static void AllScreensLoaded()
		{
			_screens.Values.ForEach(s => s.AllScreensLoaded());
		}

		/// <summary>
		/// Updates all screens
		/// </summary>
		internal static void Update()
		{
			_screens.Values.ForEachReverse(s => s.Update());
		}

		/// <summary>
		/// Draws all screens.
		/// </summary>
		internal static void Draw(SpriteBatch spriteBatch)
		{
			_screens.Values.ForEach(s => s.Draw(spriteBatch));
		}

		/// <summary>
		/// Changes proportions of all screens in the game.
		/// </summary>
		internal static void ResolutionChanged()
		{
			_screens.Values.ForEach(s => s.ResolutionChanged());
		}
	}
}
