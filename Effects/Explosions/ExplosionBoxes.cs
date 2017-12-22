using GameEngine.GamePrimitives;
using GameEngine.MathEngine;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Explosions
{
	public sealed class ExplosionBoxes : ContainerGameObject
	{
		public ExplosionBoxes(BasicLevel level, Vector2 position, float boxSideSize, int boxesCount, float distance, IWorldObject parent = null)
			: base(level, position, Vector2.Zero, parent)
		{
			// Creates boxes
			var span = MathHelper.TwoPi / boxesCount;
			for (var i = 0; i < boxesCount; i++)
			{
				var direction = MyMath.GetRandomAngle(span) + i * span;
				var box = new ExplosionBox(level, position, boxSideSize, direction, distance * MyMath.GetRandomFloat(0.8f, 1.2f));
				AddObject(box);
			}
		}
	}
}
