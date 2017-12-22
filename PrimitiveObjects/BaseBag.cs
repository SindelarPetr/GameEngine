using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.CameraEngine;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.PrimitiveObjects
{
	public class BaseBag<T> : BaseObject where T : IBaseObject
	{
		public BaseBag(Camera camera, Vector2 position, Vector2 size, IWorldObject parent = null) : base(camera, position, size, parent)
		{
		}

		public override void Update()
		{
			base.Update();


		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			
		}
	}
}
