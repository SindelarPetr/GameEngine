using GameEngine.CameraEngine;
using GameEngine.Menu.Screens;
using GameEngine.Options;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.ScreensAs.TextureObjects
{
	public class RotatingTexture : ScreenTextureContainer
	{
		protected float RotationSpeed;
		public RotatingTexture(Camera camera, Vector2 position, Vector2 size, IScreenParentObject parent = null, MyTexture2D texture = null, float rotationSpeed = 0.001f)
			: base(camera, position, size, parent, texture)
		{
			RotationSpeed = rotationSpeed;
		}

		public override void Update()
		{
			base.Update();

			BasicRotation += RotationSpeed * (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
		}
	}
}
