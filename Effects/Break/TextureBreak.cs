using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Break
{
	public class TextureBreak : ContainerGameObject
	{
		private readonly float _velocity;

		public TextureBreak(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null, MyTexture2D texture = null, float velocity = 0.1f)
			: base(camera, position, size, parent, texture)
		{
			_velocity = velocity;
			float friction = 0.001f;
			float randomAngleSpan = MathHelper.PiOver2;
			AddObject(new PartTextureDisolve(camera, LeftTopPosition, size, LeftTopRectangle, LeftTopOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) - MathHelper.PiOver4 - MathHelper.PiOver2, 0.1f, this, Texture) {SmoothOpacity = {Friction = friction}});
			AddObject(new PartTextureDisolve(camera, LeftBotPosition, size, LeftBotRectangle, LeftBotOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) + MathHelper.PiOver4 + MathHelper.PiOver2, 0.1f, this, Texture) { SmoothOpacity = { Friction = friction } });
			AddObject(new PartTextureDisolve(camera, RightTopPosition, size, RightTopRectangle, RightTopOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) - MathHelper.PiOver4, 0.1f, this, Texture) { SmoothOpacity = { Friction = friction } });
			AddObject(new PartTextureDisolve(camera, RightBotPosition, size, RightBotRectangle, RightBotOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) + MathHelper.PiOver4, 0.1f, this, Texture) { SmoothOpacity = { Friction = friction } });
		}

		private Vector2 LeftTopPosition => BasicPosition + new Vector2(-BasicSize.X, -BasicSize.Y) / 4f;
		private Vector2 LeftBotPosition => BasicPosition + new Vector2(-BasicSize.X, BasicSize.Y) / 4f;
		private Vector2 RightTopPosition => BasicPosition + new Vector2(BasicSize.X, -BasicSize.Y) / 4f;
		private Vector2 RightBotPosition => BasicPosition + new Vector2(BasicSize.X, BasicSize.Y) / 4f;

		Rectangle LeftTopRectangle => new Rectangle(new Vector2(Texture.Width * 1f / 4f, Texture.Height * 1f / 4f).ToPoint(), (Texture.Size / 2f).ToPoint());
		Rectangle LeftBotRectangle => new Rectangle(new Vector2(Texture.Width * 1f / 4f, Texture.Height * 3f / 4f).ToPoint(), (Texture.Size / 2f).ToPoint());
		Rectangle RightTopRectangle => new Rectangle(new Vector2(Texture.Width * 3f / 4f, Texture.Height * 1f / 4f).ToPoint(), (Texture.Size / 2f).ToPoint());
		Rectangle RightBotRectangle => new Rectangle(new Vector2(Texture.Width * 3f / 4f, Texture.Height * 3f / 4f).ToPoint(), (Texture.Size / 2f).ToPoint());

		private Vector2 LeftTopOriginMultiplier => new Vector2(-1, -1) / 4f + new Vector2(0.5f);
		private Vector2 LeftBotOriginMultiplier => new Vector2(-1, -1) / 4f + new Vector2(0.5f);
		private Vector2 RightTopOriginMultiplier => new Vector2(-1, -1) / 4f + new Vector2(0.5f);
		private Vector2 RightBotOriginMultiplier => new Vector2(-1, -1) / 4f + new Vector2(0.5f);
	}
}
