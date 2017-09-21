using GameEngine.CameraEngine;
using GameEngine.Input;
using GameEngine.MathEngine;
using GameEngine.Options;
using GameEngine.Primitives;
using GameEngine.Properties;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using GameEngine.Menu.Screens;

namespace GameEngine.Menu.ScreensAs.Buttons
{
	public class ScreenButton : ScreenTextureContainer
	{
		#region Color
		protected MyColor PressColor { get; private set; }
		public MyColor DefaultColor { get; private set; }

		public virtual void ChangeColor(MyColor newColor)
		{
			(PressColor = newColor.Duplicate()).AdjustLight(-0.6f);
			DefaultColor = newColor.Duplicate();
			ColorChanger.SetColor(newColor);

			ScreenTexture.ColorChanger.ResetColor(newColor);

			OnColorChanged?.Invoke(newColor);
		}
		#endregion

		public float DefaultOpacity = 1;

		public MyTouch Touch;
		public ButtonClickState ButtonState = ButtonClickState.Default;

		protected bool MouseHover { get; } = false;

		public bool Enabled { get; private set; }

		public bool IsClickable { get; protected set; }
		public bool IsTouchable { get; protected set; }
		public bool IsTouchBlocking { get; private set; }

		private MyTouch _blockedMouse;

		/// <summary>
		/// Determines whether the button is just a rectangle somewhere in the screen (false), or a button which covers the screen no metter where it has position (true)
		/// </summary>
		public bool IsFullscreenButton { get; set; }

		#region Dragging
		protected bool IsDragging { get; private set; }
		public bool IsDraggingAllowed { get; set; }

		private float _dragStartDistance = 10;
		private Vector2 _startDragPosition;

		// For dragging for higher steps than default.
		protected Vector2 DragBuffer { get; set; }

		public bool IsFingerControl { get; set; }

		#endregion

		#region Events
		public event Action<MyTouch> OnClick;
		public event Action<MyTouch> OnPressed;
		public event Action<MyTouch> OnClickLost;
		public event Action<MyTouch> OnClickContinues;

		public event Action OnDragStarted;
		public event Action<ScreenButton, Vector2> OnDragging;
		public event Action OnDragEnded;
		public event Action<Color> OnColorChanged;
		#endregion

		public ScreenButton(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider, IScreenParentObject parent = null,
			MyTexture2D texture = null)
			: base(camera, positionProvider, sizeProvider, parent, texture)
		{
			Init();
		}
		public ScreenButton(Camera camera, Vector2 position, Vector2 size, IScreenParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture)
		{
			Init();
		}

		private void Init()
		{
			IsTouchable = true;
			IsClickable = true;
			IsTouchBlocking = true;

			Enabled = true;

			ChangeColor((MyColor)Color.Blue);
		}

		public override void Update()
		{
			base.Update();

			TryReleaseMouseBlock();

			#region Getting touch
			if (CanBeTouched())
			{
				if (Touch == null)
				{
					Touch = GetMouse() ?? GetTouch();
				}

				BlockTouches();
			}
			#endregion

			#region Has touch
			if (Touch != null)
				switch (Touch.State)
				{
					#region JustPressed

					case TouchState.JustPressed:
						{
							if (IsClickable)
								Press();
						}
						break;

					#endregion

					#region Continuing
					case TouchState.Continuing:
						{
							#region Dragging

							if (IsDraggingAllowed)
								if (!IsDragging)
								{
									#region Drag start

									Vector2 mouseStartDragMove = Touch.Position - _startDragPosition;
									if (!IsClickable || mouseStartDragMove.Length() >= _dragStartDistance)
									{
										StartDragging();

										Drag(mouseStartDragMove);
									}

									#endregion
								}
								else
								{
									Drag(Touch.Move);
								}
							#endregion

							if (IsClickable)
								OnClickContinues?.Invoke(Touch);
						}
						break;

					#endregion

					#region Relased
					case TouchState.Relased:
						{
							ReleaseTouch();
						}
						break;

						#endregion
				}

			#endregion
		}

		private void ReleaseTouch()
		{
			if (!IsDragging)
			{
				if (IsClickable)
					if (IsFullscreenButton || MyMath.CollisionPointAndRectangle(Touch.Position, GetAbsolutePosition(), GetAbsoluteRotation(), GetAbsoluteSize()))
						Click();
					else
						LooseTouches();
			}
			else if (IsDraggingAllowed)
				DragEnded();

			Unpress();
		}

		private void TryReleaseMouseBlock()
		{
			if (_blockedMouse != null && _blockedMouse.State == TouchState.Relased)
			{
				_blockedMouse.SetAsNotOwned();
				_blockedMouse = null;
			}
		}

		private void BlockTouches()
		{
			if (IsTouchBlocking && Touch != null)
			{
				MyTouch touch;
				do
				{
					touch = GetTouch();
				} while (touch != null);

				if (_blockedMouse == null)
					_blockedMouse = GetMouse();
			}
		}

		private MyTouch GetTouch()
		{
			return IsFullscreenButton
				? InputOptions.MyState.GetBrandNewTouch()
				: InputOptions.MyState.GetNewTouch(GetAbsolutePosition(), GetAbsoluteSize(), GetAbsoluteRotation());
		}

		private MyTouch GetMouse()
		{
			MyTouch touch = null;
			var absPosition = GetAbsolutePosition();

			if (GeneralOptions.UseMouse && !InputOptions.MyState.MouseTouch.HasOwner &&
				InputOptions.MyState.MouseTouch.State == TouchState.JustPressed &&
				(IsFullscreenButton || MyMath.CollisionPointAndRectangle(InputOptions.MyState.MouseTouch.Position, absPosition,
					 GetAbsoluteRotation(), GetAbsoluteSize())))
			{
				touch = InputOptions.MyState.MouseTouch;
				InputOptions.MyState.MouseTouch.SetAsOwned();
			}

			return touch;
		}

		#region Touching

		protected virtual bool CanBeTouched() => IsTouchable && Enabled;

		#region Click
		private void Press()
		{
			_startDragPosition = Touch.Position;

			SetPressStyle();

			ButtonState = ButtonClickState.Pressed;

			OnPressed?.Invoke(Touch);
		}

		private void Click()
		{
			Debug.Assert(ButtonState == ButtonClickState.Pressed);

			OnClick?.Invoke(Touch);
		}

		#region Unpress
		public void Unpress()
		{
			RemoveTouch();

			SetDefaultStyle();

			Unpressed();
		}

		protected virtual void Unpressed()
		{
			ButtonState = ButtonClickState.Default;
		}
		#endregion
		#endregion

		#region Dragging

		private void Drag(Vector2 value)
		{
			// Draw from previous calling
			DragBuffer += Camera.ReverseTransformPosition(value, Vector2.Zero, new Vector2(Camera.Zoom), Camera.FinalRotation);

			var modifiedValue = DragValueModify(DragBuffer);

			if (modifiedValue.X != 0 || modifiedValue.Y != 0)
			{
				DragBuffer -= modifiedValue;

				CallDragging(this, modifiedValue);
			}
		}

		protected void CallDragging(ScreenButton sender, Vector2 value)
		{
			OnDragging?.Invoke(this, value);
		}

		public void StartDragging()
		{
			IsDragging = true;

			OnDragStarted?.Invoke();
		}

		//this will get the raw value of drag value and make a better form of it which returns.
		protected virtual Vector2 DragValueModify(Vector2 value)
		{
			return value;
		}

		protected void DragEnded()
		{
			IsDragging = false;
			DragBuffer = Vector2.Zero;
			OnDragEnded?.Invoke();
			RemoveTouch();
		}
		#endregion

		public override void LooseTouches()
		{
			if (Touch != null)
			{
				if (Touch.IsJustPressedOrCont())
				{
					OnClickLost?.Invoke(Touch);
				}
			}
		}

		private void RemoveTouch()
		{
			Touch?.SetAsNotOwned();

			Touch = null;
		}
		#endregion

		#region Style
		protected virtual void SetPressStyle()
		{

			ColorChanger.MyColor = (ColorChanger.MyColorToGo = PressColor).Duplicate();
		}

		protected virtual void SetDefaultStyle()
		{
			ColorChanger.MyColorToGo = DefaultColor.Duplicate();
		}
		#endregion

		#region Enability
		public override void Show(IMenuScreenElement showInitializator = null)
		{
			Enabled = true;

			base.Show(showInitializator);

			SetDefaultStyle();
		}

		public override void Hide()
		{
			Enabled = false;

			base.Hide();

			LooseTouches();
		}
		#endregion
	}
}
