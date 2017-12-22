using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Collisions
{
	public enum BodyShape { Rectangle, Circle }
	public class CollisionGameObject : GameObject
	{
		public override Vector2 BasicPosition
		{
			get => Body?.Position * 100f ?? base.BasicPosition;
			set
			{
				if (Body != null) Body.Position = value / 100f;
				else base.BasicPosition = value;
			}
		}

		public override float BasicRotation
		{
			get => Body?.Rotation ?? base.BasicRotation;
			set
			{
				if (Body != null) Body.Rotation = value;
				else base.BasicRotation = value;
			}
		}

		protected World World { get; }

		protected Body Body { get; }

		public bool Enabled
		{
			get => Body.Enabled;
			private set => Body.Enabled = value;
		}

		public CollisionGameObject(BasicLevel level, World world, Vector2 position, Vector2 size, IWorldObject parent = null, MyTexture2D texture = null, BodyShape bodyShape = BodyShape.Rectangle) : base(level, position, size, parent, texture)
		{
			if (bodyShape == BodyShape.Rectangle)
				Body = BodyFactory.CreateRectangle(world, size.X / 100f, size.Y / 100f, 1, position / 100f, 0, BodyType.Dynamic,
					this);
			else
				Body = BodyFactory.CreateCircle(world, size.X / 200, 1, position / 100f, BodyType.Dynamic, this);

			Body.SleepingAllowed = true;
			Body.Restitution = 0;

			Body.OnCollision += Body_OnCollision;
			World = world;
		}

		private bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
		{
			return true;
		}

		public void SetCollisionCategories(Category categories)
		{
			Body.CollisionCategories = categories;
		}

		public void SetCollidesWithCategories(Category categories)
		{
			Body.CollidesWith = categories;
		}

		public virtual void EnableBody()
		{
			Enabled = true;
		}

		public virtual void DisableBody()
		{
			Enabled = false;
		}
	}
}
