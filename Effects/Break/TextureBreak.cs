using GameEngine.GamePrimitives;
using GameEngine.MathEngine;
using GameEngine.ObjectPrimitives;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Break
{
	public class TextureBreak : ContainerGameObject
	{
		private readonly float _velocity;

		public TextureBreak(BasicLevel level, Vector2 position, Vector2 size, IWorldObject parent = null, MyTexture2D texture = null, float velocity = 0.1f)
			: base(level, position, size, parent, texture)
		{
			_velocity = velocity;
			float friction = 0.001f;
			float randomAngleSpan = MathHelper.PiOver2;
			AddObject(new PartTextureDisolve(level, LeftTopPosition, size, LeftTopRectangle, LeftTopOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) - MathHelper.PiOver4 - MathHelper.PiOver2, 0.1f, this, Texture) { SmoothOpacity = { Friction = friction } });
			AddObject(new PartTextureDisolve(level, LeftBotPosition, size, LeftBotRectangle, LeftBotOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) + MathHelper.PiOver4 + MathHelper.PiOver2, 0.1f, this, Texture) { SmoothOpacity = { Friction = friction } });
			AddObject(new PartTextureDisolve(level, RightTopPosition, size, RightTopRectangle, RightTopOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) - MathHelper.PiOver4, 0.1f, this, Texture) { SmoothOpacity = { Friction = friction } });
			AddObject(new PartTextureDisolve(level, RightBotPosition, size, RightBotRectangle, RightBotOriginMultiplier, _velocity, MyMath.GetRandomAngle(randomAngleSpan) + MathHelper.PiOver4, 0.1f, this, Texture) { SmoothOpacity = { Friction = friction } });
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
