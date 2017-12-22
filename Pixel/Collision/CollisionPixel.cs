using FarseerPhysics.Dynamics;
using GameEngine.Collisions;
using GameEngine.Content;
using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace GameEngine.Pixel.Collision
{
	public class CollisionPixel : CollisionGameObject
	{
		private int _neigboursKilled;
		private int _neighbourBorders;
		private int _neighboursCount;
		private bool _isAlive;

		public event EventHandler OnKilled;
		public event EventHandler OnRevived;

		public CollisionPixel(BasicLevel level, World world, Vector2 position, Vector2 size, IWorldObject parent = null) : base(level, world, position, size, parent, TextureManager.Box2)
		{
			_isAlive = true;

			Body.BodyType = BodyType.Static;
			Body.CollisionCategories = Category.Cat2;
			Body.CollidesWith = Category.Cat1 | Category.Cat3;
			Body.Friction = 0;
		}

		public void AddNeighbourBorder()
		{
			_neighbourBorders++;

			if (!ShouldBeEnabled()) DisableBody();
		}

		public void AddNeighbour(CollisionPixel collisionPixel)
		{
			_neighboursCount++;
			collisionPixel.OnKilled += CollisionPixel_OnKilled;
			collisionPixel.OnRevived += CollisionPixel_OnRevived;

			if (!ShouldBeEnabled()) DisableBody();
		}

		private void CollisionPixel_OnRevived(object sender, EventArgs e)
		{
			_neigboursKilled--;

			if (!ShouldBeEnabled())
				DisableBody();
		}

		private void CollisionPixel_OnKilled(object sender, EventArgs e)
		{
			_neigboursKilled++;

			EnableBody();
		}

		public void Kill(Vector2 position, float force)
		{
			Debug.Assert(_isAlive);

			DisableBody();
			_isAlive = false;

			OnKilled?.Invoke(this, null);
		}

		public void Revive()
		{
			Debug.Assert(!_isAlive);

			if (ShouldBeEnabled()) // neighbour is death or its side box
				EnableBody();
			_isAlive = true;

			OnRevived?.Invoke(this, null);
		}

		private bool ShouldBeEnabled() => _neigboursKilled > 0 || _neighboursCount < 4 - _neighbourBorders;

		public override void EnableBody()
		{
			base.EnableBody();

			ColorChanger.ResetColor(Color.White);
		}

		public override void DisableBody()
		{
			base.DisableBody();

			ColorChanger.ResetColor(Color.Blue);
		}
	}
}
