using GameEngine.Primitives;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Effects
{
	public class EffectManager : IBaseElement, IEffectManager
	{
		private readonly List<IGameElement> _effects;

		public EffectManager()
		{
			_effects = new List<IGameElement>();
		}

		public void Update()
		{
			for (var i = _effects.Count - 1; i >= 0; i--)
			{
				var gameElement = _effects[i];
				gameElement.Update();
			}
		}
		public void AddEffect(IGameElement effect)
		{
			_effects.Add(effect);
			effect.OnRemoving += Effect_OnRemove;
		}

		private void Effect_OnRemove(object sender, IGameElement e)
		{
			_effects.Remove(e);
			e.OnRemoving -= Effect_OnRemove;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var gameElement in _effects)
			{
				gameElement.Draw(spriteBatch);
			}
		}
	}
}
