using System;

namespace GameEngine.Primitives
{
	public interface IGameElement : IBaseElement
	{
		event EventHandler<IGameElement> OnRemoving;
	}
}
