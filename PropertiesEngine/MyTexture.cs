using GameEngine.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Properties
{
	public class MyTexture2D
	{
		#region Operators
		public static implicit operator Texture2D(MyTexture2D myTexture)
		{
			return myTexture.Texture;
		}

		public static explicit operator MyTexture2D(Texture2D texture)
		{
			return new MyTexture2D(texture);
		}

		public static explicit operator Texture(MyTexture2D texture)
		{
			return texture.Texture;
		}
		#endregion

		private Texture2D _texture;
		public Texture2D Texture
		{
			get => _texture;
			set
			{
				_texture = value;
				Size = _texture.GetSize();
				Middle = Size / 2;
				Top = new Vector2(Size.X / 2, 0);
				Bot = new Vector2(Size.X / 2, Size.Y);
			}
		}

		public Vector2 Middle { get; private set; }
		public Vector2 Top { get; private set; }
		public Vector2 Bot { get; private set; }

		public Vector2 Size { get; private set; }

		public float Height => Size.Y;
		public float Width => Size.X;

		public MyTexture2D(string path)
		{
			Texture = GeneralOptions.Content.Load<Texture2D>(path);
		}

		public MyTexture2D(Texture2D texture)
		{
			Texture = texture;
		}

	}
}