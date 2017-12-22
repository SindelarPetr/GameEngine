using GameEngine.CameraEngine;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Menu.Screens.Buttons
{
	public class ScreenIconButton : ScreenButton
	{
		protected MyTexture2D Icon { get; set; }
		protected MyColor IconColor { get; set; } = new MyColor();

		public ScreenIconButton(Camera camera, Vector2 position, Vector2 size, MyTexture2D icon, IScreenParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture)
		{
			Icon = icon;
		}

		protected override void SetDefaultStyle()
		{
			SmoothOpacity.ValueToGo = DefaultOpacity;
			ColorChanger.MyColorToGo = DefaultColor.Duplicate();
		}

		protected override void SetPressStyle()
		{
			SmoothOpacity.ResetValue(1);
			ColorChanger.ResetColor(PressColor.Color);
		}
	}
}
