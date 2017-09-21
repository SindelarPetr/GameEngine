using GameEngine.MathEngine;
using GameEngine.Options;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameEngine.Input
{
	public class MyState
	{
		public List<MyTouch> Touches = new List<MyTouch>();

		#region GamePad
		public GamePadState GamePadState = new GamePadState();
		public bool GamePadStateHandelt = false;
		#endregion

		public MyTouch MouseTouch;

		public float MouseWheelDiff { get; private set; }
		private float LastMouseWheelValue { get; set; }

		public bool BackPressed
		{
			get;
			private set;
		}

		public bool MouseWheelHandelt { get; set; }

		public MyState()
		{
			if (GeneralOptions.UseMouse)
				MouseTouch = new MyTouch(Mouse.GetState());
		}

		public void Actualise(bool isActive)
		{
			#region Touches
			TouchCollection newTouchLocations = TouchPanel.GetState();
			List<MyTouch> newMyTouches = new List<MyTouch>();

			#region MouseTouch
			if (GeneralOptions.UseMouse)
			{
				MouseState mouseState = Mouse.GetState();

				#region Mouse wheel

				MouseWheelHandelt = false;
				MouseWheelDiff = mouseState.ScrollWheelValue - LastMouseWheelValue;
				LastMouseWheelValue = mouseState.ScrollWheelValue;
				#endregion

				#region Mouse press
				if (isActive)
				{
					if (mouseState.LeftButton == ButtonState.Pressed)
					{
						#region Mouse is pressed
						if (MouseTouch == null)
						{
							MouseTouch = new MyTouch(mouseState);
						}
						else
						{
							MouseTouch.ActualiseMouse(mouseState);
						}
						#endregion
					}
					else
					{
						#region Mouse is relased
						if (MouseTouch != null)
						{
							MouseTouch.ActualiseMouse(mouseState);
						}
						#endregion
					}
				}
				else
				{
					MouseTouch.SetAsRelased();
				}
				#endregion
			}
			#endregion

			#region Checking newTouches
			//Prochází newTouchLocations a ke každému hledá starý myTouch
			if (isActive)
				for (int a = 0; a < newTouchLocations.Count; a++)
				{
					TouchLocation newTouchLocation = newTouchLocations[a];
					bool isNew = true;

					#region Searching old one for the new
					for (int i = 0; i < Touches.Count; i++)
					{
						MyTouch myTouch = Touches[i];

						#region Try actualise
						if (myTouch.TryActualise(newTouchLocation))
						{
							#region Found right newTouch
							newMyTouches.Add(myTouch);
							Touches.Remove(myTouch);
							i--;
							isNew = false;
							break;
							#endregion
						}
						#endregion
					}
					#endregion

					if (isNew)
					{
						MyTouch touch = new MyTouch(newTouchLocation);
						newMyTouches.Add(touch);
					}
				}
			#endregion

			#region Creating new from the rest
			foreach (MyTouch oldMyTouch in Touches)
			{
				oldMyTouch.SetAsRelased();
			}
			#endregion

			Touches = newMyTouches;
			#endregion
		}

		//Gives touch which has no owner
		public MyTouch GetNewTouch(bool setAsOwned = true)
		{
			foreach (MyTouch touch in Touches)
			{
				if (touch.State != TouchState.Relased && touch.HasOwner == false)
				{
					if (setAsOwned)
						touch.SetAsOwned();

					return touch;
				}
			}
			return null;
		}

		/// <summary>
		/// Looks in all touches for a JustPressed touch
		/// </summary>
		/// <param name="setAsOwned">True if the touch should be marked as it has got an owner</param>
		/// <returns>If didnt found then returns null. If found returns the touch.</returns>
		public MyTouch GetBrandNewTouch(bool setAsOwned = true)
		{
			foreach (MyTouch touch in Touches)
			{
				if (touch.State == TouchState.JustPressed && touch.HasOwner == false)
				{
					if (setAsOwned)
						touch.SetAsOwned();

					return touch;
				}
			}
			return null;
		}

		/// <summary>
		/// Looks in all touches for a JustPressed touch
		/// </summary>
		/// <param name="setAsOwned">True if the touch should be marked as it has got an owner</param>
		/// <returns>If didnt found then returns null. If found returns the touch.</returns>
		public MyTouch GetBrandNewTouchBut(MyTouch exception, bool setAsOwned = true)
		{
			foreach (MyTouch touch in Touches)
			{
				if (touch.State == TouchState.JustPressed && touch.HasOwner == false && touch.Id != exception.Id)
				{
					if (setAsOwned)
						touch.SetAsOwned();

					return touch;
				}
			}
			return null;
		}

		/// <summary>
		/// In rectangle
		/// </summary>
		/// <returns>The new touch in rectangle</returns>
		public MyTouch GetNewTouch(Vector2 positionOnDisplayRect, Vector2 sizeOnDisplayRect, float rotation, bool setAsOwned = true, bool blockOthers =true)
		{
			MyTouch selectedTouch = null;
			foreach (MyTouch touch in Touches)
			{
				if (touch.State == TouchState.JustPressed && touch.HasOwner == false && MyMath.CollisionPointAndRectangle(touch.Position, positionOnDisplayRect, rotation, sizeOnDisplayRect))
				{
					if (setAsOwned)
						touch.SetAsOwned();

					if (selectedTouch == null)
						selectedTouch = touch;
				}
			}
			return selectedTouch;
		}

		/// <summary>
		/// In circle
		/// </summary>
		/// <returns></returns>
		public MyTouch GetNewTouch(Vector2 positionCirc, float r, bool setAsOwned = true)
		{
			foreach (MyTouch touch in Touches)
			{
				if (touch.State == TouchState.JustPressed && touch.HasOwner == false && Vector2.Distance(positionCirc, touch.Position) <= r)
				{
					if (setAsOwned)
						touch.SetAsOwned();
					return touch;
				}
			}
			return null;
		}

		public bool ExistsFreeTouch()
		{
			foreach (MyTouch touch in Touches)
			{
				if (touch.State != TouchState.Relased && touch.HasOwner == false)
				{
					return true;
				}
			}
			return false;
		}

		public MyTouch[] GetTwoFreeTouches()
		{
			MyTouch first = GetNewTouch(true);

			if (first != null)
			{
				MyTouch second = GetNewTouch(true);

				if (second != null)
				{
					first.SetAsOwned();
					return new MyTouch[2] { first, second };
				}
				else
				{
					first.SetAsNotOwned();
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public MyTouch GetPressedMouse(bool setAsOwned)
		{
			if (MouseTouch != null && Touches.Count == 0 && !MouseTouch.HasOwner && MouseTouch.State == TouchState.JustPressed)
			{
				if (setAsOwned)
				{
					MouseTouch.SetAsOwned();
				}

				return MouseTouch;
			}

			return null;
		}
	}
}
