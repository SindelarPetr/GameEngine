using System;
using GameEngine.ObjectPrimitives;

namespace GameEngine.GamePrimitives
{
	public interface IGameElement : IPrimitiveElement
	{
		event EventHandler<IGameElement> OnRemoving;
	}
}
