using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.ObjectPrimitives
{

	public class TextObject : BaseObject
	{
		#region Text
		string _content = "";
		public string Content
		{
			get => _content;
			set
			{
				Vector2 scale = BasicSize / TextSize;

				_content = value;
				TextSize = _spriteFont.MeasureString(value);
				BasicSize = TextSize * scale;
			}
		}

		/// <summary>
		/// Text size without applying any scale on it.
		/// </summary>
		public Vector2 TextSize { get; private set; }
		#endregion

		#region SpriteFont
		SpriteFont _spriteFont;
		public SpriteFont SpriteFont
		{
			get => _spriteFont;
			set
			{
				float height = BasicSize.Y;

				_spriteFont = value;
				TextSize = _spriteFont.MeasureString(_content);

				BasicSize = MyMath.ScaleByY(TextSize, height);
			}
		}
		#endregion

		public TextObject(Camera camera, SpriteFont spriteFont, string text, Vector2 position, IWorldObject parent = null)
			: this(camera, spriteFont, text, position, spriteFont.MeasureString(text).Y, parent) { }

		public TextObject(Camera camera, SpriteFont spriteFont, string text, Vector2 position, float height, IWorldObject parent = null)
			: base(camera, position, MyMath.ScaleByY(spriteFont.MeasureString(text), height), parent)
		{
			_content = text;
			TextSize = spriteFont.MeasureString(text);
			_spriteFont = spriteFont;
		}

		/// <summary>
		/// Sets the new height and scales width according to new scale.
		/// </summary>
		/// <param name="newHeight"></param>
		public void ScaleHeight(float newHeight)
		{
			BasicSize = MyMath.ScaleByY(BasicSize, newHeight);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 absolutePosition = GetAbsolutePosition();
			Vector2 absoluteSize = GetAbsoluteSize();
			spriteBatch.DrawString(_spriteFont, _content, absolutePosition, ColorChanger * GetAbsoluteOpacity() * GetIndependentOpacity(), GetAbsoluteRotation(), TextSize * OriginMultiplier, absoluteSize / TextSize, SpriteEffects, 0f);
		}
	}
}