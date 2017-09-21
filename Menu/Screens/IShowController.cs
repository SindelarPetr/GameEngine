namespace GameEngine.Menu
{
	/// <summary>
	/// This interface is used in every element managing showing.
	/// </summary>
	public interface IShowController
	{
		ShowState GetShowState();
		float GetShowValue();
		float GetShowZoom();
		bool IsFullyVisible();
		bool IsHidden();
	}
}