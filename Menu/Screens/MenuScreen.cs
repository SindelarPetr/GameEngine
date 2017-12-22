using System;
using GameEngine.CameraEngine;
using GameEngine.Options;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.Screens
{
	/// <summary>
	/// State of visibility of menu.
	/// </summary>
	public enum ShowState { Hidden, Hiding, Showed, Showing }

	[Flags]
	public enum DrawPriorities { Bottom = 1, MiddleBottom = 2, Middle = 4, MiddleTop = 8, Top = 16 }

	/// <summary>
	/// The root of all menu screens. Each menu screen must derivate from this screen. If you want the menu screen to be opened at the startup then mark the class with MainScreenAttribute attribute
	/// </summary>
	public class MenuScreen : ScreenBag
	{
		public DrawPriorities DrawPriorities { get; set; }
		public MenuScreen(Camera camera)
			: base(camera, Vector2.Zero, DisplayOptions.Resolution)
		{
			DrawPriorities = DrawPriorities.Middle;
		}
	}
}
