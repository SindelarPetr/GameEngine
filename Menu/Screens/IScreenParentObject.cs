using GameEngine.Primitives;

namespace GameEngine.Menu.Screens
{
	public interface IScreenParentObject : IParentObject
	{

		/// <summary>
		/// Its a value from 0 to 1, which indicates the actual state of visibility.
		/// </summary>
		/// <returns>The ratio of visibility</returns>
		float GetShowValue();
	}
}
