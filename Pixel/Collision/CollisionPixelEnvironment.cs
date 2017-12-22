using FarseerPhysics.Dynamics;
using GameEngine.CameraEngine;
using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel.Collision
{
	// TODO: Borders from big rectangles

	public class CollisionPixelEnvironment : CollisionPixelObject
	{
		public CollisionPixelEnvironment(BasicLevel level, World world, PixelDescription[,] pixelsDescription, Vector2 pixelsCount, IWorldObject parent = null) : base(level, world, Vector2.Zero, pixelsDescription, pixelsCount, parent)
		{
		}
	}
}
