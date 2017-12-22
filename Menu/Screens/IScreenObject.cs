using GameEngine.ObjectPrimitives;

namespace GameEngine.Menu.Screens
{
	public interface IScreenObject : IPrimitiveElement
	{
		/// <summary>
		/// Will be called by MenuScreenManager after all screens are loaded. Should be used for connecting events between menus.
		/// </summary>
		void AllScreensLoaded();

		void LooseTouches();

		//Will be called when MenuScreen will start showing
		void Show(IScreenObject showInitializator);

		//Will be called when MenuScreen will start hidding
		void Hide();

		void ResolutionChanged();
	}
}
