using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameEngine.Input.TouchPanel
{
	public enum TouchState { JustPressed, Continuing, Relased }

	public class MyTouch
	{
		public int Id => CurrTouch.Id;

		public bool HasOwner { get; private set; } = false;
		public void SetAsOwned() { HasOwner = true; }
		public void SetAsNotOwned() { HasOwner = false; }

		#region TouchState
		public TouchState State
		{
			get;
			private set;
		}

		public void SetAsRelased()
		{
			State = TouchState.Relased;
			//this.PreviousTouch = this.CurrTouch;
			Move = Vector2.Zero;
		}
		#endregion

		public Vector2 Position => CurrTouch.Position;

		public TouchLocation PreviousTouch;
		public TouchLocation CurrTouch;

		public Vector2 Move = Vector2.Zero;

		public MyTouch(TouchLocation touchLocation)
		{
			State = TouchState.JustPressed;
			PreviousTouch = CurrTouch = touchLocation;
		}

		public MyTouch(MouseState mouseState)
		{
			State = TouchState.JustPressed;
			PreviousTouch = CurrTouch = new TouchLocation(-1, TouchLocationState.Pressed, new Vector2(mouseState.X, mouseState.Y));
		}

		private MyTouch FindPreviousTouch(List<MyTouch> previousTouches)
		{
			foreach (MyTouch touch in previousTouches)
			{
				if (touch.Id == Id)
				{
					return touch;
				}
			}
			return null;
		}

		public bool TryActualise(TouchLocation touch) //returns if succesfully actualised
		{
			if (touch.Id == Id)
			{
				Actualise(touch);
				return true;
			}

			return false;
		}

		private void Actualise(TouchLocation touch)
		{
			PreviousTouch = CurrTouch;
			CurrTouch = touch;
			Move = CurrTouch.Position - PreviousTouch.Position;

			State = TouchState.Continuing;
		}

		public void ActualiseMouse(MouseState mouseState)
		{
			#region Touch state options
			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				#region Pressed options
				if (State == TouchState.Relased)
				{
					//Has been just pressed
					State = TouchState.JustPressed;
				}
				else
				{
					#region Coninuing
					if (State == TouchState.JustPressed)
					{
						State = TouchState.Continuing;
					}
					#endregion
				}
				#endregion
			}
			else
			{
				//Mouse left button is relased
				State = TouchState.Relased;
				return;
			}
			#endregion

			PreviousTouch = CurrTouch;
			CurrTouch = new TouchLocation(-1, TouchLocationState.Pressed, new Vector2(mouseState.X, mouseState.Y));
			Move = CurrTouch.Position - PreviousTouch.Position;
		}

		public bool IsJustPressedOrCont()
		{
			if (State == TouchState.JustPressed || State == TouchState.Continuing)
				return true;
			return false;
		}

		public bool IsRelased()
		{
			return State == TouchState.Relased;
		}

		public static bool IsTouchFresh(MyTouch touch)
		{
			return touch != null && touch.IsJustPressedOrCont();
		}
	}
}
