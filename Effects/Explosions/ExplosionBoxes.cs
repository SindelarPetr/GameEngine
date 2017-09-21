using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Explosions
{
	public class ExplosionBoxes : ContainerGameObject
	{
		public ExplosionBoxes(Camera camera, Vector2 position, float boxSideSize, int boxesCount, float distance, IParentObject parent = null)
			: base(camera, position, Vector2.Zero, parent)
		{
			// Creates boxes
			float span = MathHelper.TwoPi / boxesCount;
			for (int i = 0; i < boxesCount; i++)
			{
				float direction = MyMath.GetRandomAngle(span) + i * span;
				ExplosionBox box = new ExplosionBox(camera, position, boxSideSize, direction, distance * MyMath.GetRandomFloat(0.8f, 1.2f));
				AddObject(box);
			}
		}
	}
}
