using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Content
{
	public static class FontManager
	{
		public static ContentLoader<SpriteFont> AntigoniMed25 { get; set; }
		public static ContentLoader<SpriteFont> AntigoniMed50 { get; set; }
		public static ContentLoader<SpriteFont> AntigoniMed100 { get; set; }

		public static ContentLoader<SpriteFont> AntigoniMed25Bold { get; set; }
		public static ContentLoader<SpriteFont> AntigoniMed50Bold { get; set; }
		public static ContentLoader<SpriteFont> AntigoniMed100Bold { get; set; }

		public static void LoadFonts()
		{
			AntigoniMed25 = new ContentLoader<SpriteFont>("GameEngine\\Fonts\\AntigoniMed25");
			AntigoniMed50 = new ContentLoader<SpriteFont>("GameEngine\\Fonts\\AntigoniMed50");
			AntigoniMed100 = new ContentLoader<SpriteFont>("GameEngine\\Fonts\\AntigoniMed100");

			AntigoniMed25Bold = new ContentLoader<SpriteFont>("GameEngine\\Fonts\\AntigoniMed25Bold");
			AntigoniMed50Bold = new ContentLoader<SpriteFont>("GameEngine\\Fonts\\AntigoniMed50Bold");
			AntigoniMed100Bold = new ContentLoader<SpriteFont>("GameEngine\\Fonts\\AntigoniMed100Bold");
		}
	}
}