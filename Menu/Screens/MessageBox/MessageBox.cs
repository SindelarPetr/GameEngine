namespace GameEngine.Menu.Screens.MessageBox
{
	//public class MessageBox : ScreenObject
	//{
	//	protected MbScreenText ScreenText;

	//	protected MbButton ButtonOk;

	//	//protected MbBackground Background;

	//	private ShowableElement _showableElement;

	//	private static Vector2 GetPosition()
	//	{
	//		return Vector2.Zero;
	//	}

	//	private static Vector2 GetSize()
	//	{
	//		return new Vector2(DisplayOptions.Resolution.X / 3f, DisplayOptions.Resolution.X / 6f);
	//	}

	//	public MessageBox(Camera camera, IParentObject parent = null) : base(camera, GetPosition(), GetSize(), TextureManager.Box2, parent)
	//	{
	//		_showableElement = new ShowableElement();
	//		//NestedObjectsBackground.Add(Background = new MbBackground(this));
	//		//Background.SetColor(Color.Black);
	//		NestedObjectsForeground.Add(ScreenText = new MbScreenText(this));
	//		NestedObjectsForeground.Add(ButtonOk = new MbButton(this));
	//		ColorChanger.ResetColor(Color.Gray);
	//	}

	//	public override void ResolutionChanged()
	//	{
	//		BasicPosition = GetPosition();
	//		BasicSize = GetSize();
	//		base.ResolutionChanged();
	//	}

	//	public void Show(string text, Action<object, MyTouch> clickAction)
	//	{
	//		_showableElement.Show();
	//		ButtonOk.OnClick += clickAction.Invoke;
	//		ScreenText.Content = text;
	//		Enable();
	//	}

	//	public void Hide()
	//	{
	//		_showableElement.Hide();
	//		Disable();
	//		LooseTouches();
	//	}

	//	public override void Update()
	//	{
	//		_showableElement.Update();

	//		if (_showableElement.IsHidden())
	//			return;

	//		base.Update();
	//	}

	//	public override void Draw(SpriteBatch spriteBatch
	//		)
	//	{
	//		if (_showableElement.IsHidden())
	//			return;

	//		base.Draw(spriteBatch);
	//	}
	//}
}
