using FarseerPhysics.Dynamics;
using GameEngine.CameraEngine;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace GameEngine.Pixel.Collision
{
	// TODO: Borders from big rectangles

	public class CollisionPixelEnvironment : CollisionPixelObject
	{
		public CollisionPixelEnvironment(Camera camera, World world, PixelDescription[,] pixelsDescription, Vector2 pixelsCount, IParentObject parent = null) : base(camera, world, Vector2.Zero, pixelsDescription, pixelsCount, parent)
		{
		}
	}
}
