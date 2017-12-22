
using GameEngine.PropertiesEngine;

namespace GameEngine.Content
{
	public static class TextureManager
	{
		public static MyTexture2D CircleR200;

		public static MyTexture2D CircleR30;

		public static MyTexture2D Box2;

		public static MyTexture2D Triangle60;

		public static MyTexture2D StripPattern;

		public static void LoadTextures()
		{
			CircleR200 = new MyTexture2D("GameEngine\\Textures\\CircleR200");
			CircleR30 = new MyTexture2D("GameEngine\\Textures\\CircleR30");
			Box2 = new MyTexture2D("GameEngine\\Textures\\Box2");
			StripPattern = new MyTexture2D("GameEngine\\Textures\\StripePattern");
			Triangle60 = new MyTexture2D("GameEngine\\Textures\\Triangle60");
		}
	}
}
