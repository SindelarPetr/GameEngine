using GameEngine.CameraEngine;
using GameEngine.Properties;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Primitives
{
	public class ContainerGameObject : GameObject
	{
		protected List<IGameElement> GameElements;
		protected bool DrawBackground;

		public ContainerGameObject(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null, MyTexture2D texture = null) : base(camera, position, size, parent, texture)
		{
			GameElements = new List<IGameElement>();
			DrawBackground = false;
		}

		protected virtual void AddObject(IGameElement gameElement)
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
