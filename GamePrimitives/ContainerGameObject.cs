using GameEngine.ObjectPrimitives;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.GamePrimitives
{
	public class ContainerGameObject : GameObject
	{
		protected List<IGameElement> GameElements;
		protected bool DrawBackground;

		public ContainerGameObject(BasicLevel level, Vector2 position, Vector2 size, IWorldObject parent = null, MyTexture2D texture = null) : base(level, position, size, parent, texture)
		{
			GameElements = new List<IGameElement>();
			DrawBackground = false;
		}

		public virtual void AddObject(IGameElement gameElement)
		{
			GameElements.Add(gameElement);
			gameElement.OnRemoving += GameElement_OnRemove;
		}

		private void GameElement_OnRemove(object sender, IGameElement e)
		{
			GameElements.Remove(e);
			e.OnRemoving -= GameElement_OnRemove;

			if (GameElements.Count == 0)
				Remove();
		}

		public override void Update()
		{
			base.Update();

			UpdateGameElements();
		}

		private void UpdateGameElements()
		{
			for (var i = GameElements.Count - 1; i >= 0; i--)
			{
				var gameElement = GameElements[i];
				gameElement.Update();
			}
		}

		public override void Draw(SpriteBatch spriteBatch
			)
		{
			if (DrawBackground)
				base.Draw(spriteBatch);

			foreach (var gameElement in GameElements)
			{
				gameElement.Draw(spriteBatch);
			}
		}
	}
}
