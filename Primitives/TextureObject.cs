using System;
using GameEngine.CameraEngine;
using GameEngine.Content;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Primitives
{
	/// <summary>
	/// Takes BaseObject and form a texture object from him
	/// </summary>
	public class TextureObject : BaseObject
	{
		public MyTexture2D Texture { get; }


		protected Rectangle? CutRectangle { get; set; }

		public bool IsPattern { get; set; }

		public TextureObject(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent)
		{
			Texture = texture ?? TextureManager.Box2;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			var color = ColorChanger * GetAbsoluteOpacity() * GetIndependentOpacity();
			var absoluteSize = GetAbsoluteSize();
			var absolutePosition = GetAbsolutePosition();
			var absoluteRotation = GetAbsoluteRotation();

			if (!IsPattern)
			{
				spriteBatch.Draw(Texture, absolutePosition, CutRectangle, color,
					absoluteRotation, Texture.Size * OriginMultiplier, absoluteSize / Texture.Size, SpriteEffects, 0);
			}
			else
			{
				Vector2 originShift = OriginMultiplier * absoluteSize;
				for (float x = 0; x < absoluteSize.X; x += Texture.Size.X)
				{
					for (float y = 0; y < absoluteSize.Y; y += Texture.Size.Y)
					{
						Vector2 partPosition = new Vector2(x, y) - originShift;
						float width = Math.Min(Texture.Width, absoluteSize.X - x);
						float height = Math.Min(Texture.Height, absoluteSize.Y - y);
						Rectangle rectangle = new Rectangle(0,0, (int)width, (int)height);
						
						spriteBatch.Draw(Texture, absolutePosition + partPosition, rectangle, color, absoluteRotation, Vector2.Zero, Vector2.One, SpriteEffects, 0f);
					}
				}
			}
		}
	}
}