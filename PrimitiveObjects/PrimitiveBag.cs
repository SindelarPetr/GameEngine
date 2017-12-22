using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.MathEngine;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.PrimitiveObjects
{
	public class PrimitiveBag<T> : PrimitiveObject where T : IPrimitiveObject
	{
		private readonly List<T> _items;

		public PrimitiveBag(IWorldObject parent = null)
			: base(parent)
		{
			_items = new List<T>();
		}

		public void AddItem(T item)
		{
			_items.Add(item);

			AddedItem(item);
		}

		protected virtual void AddedItem(T item)
		{
			
		}

		public override void Update()
		{
			_items.ForEach(i => i.Update());
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			_items.ForEach(i => i.Draw(spriteBatch));
		}
	}
}
