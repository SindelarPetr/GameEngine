using Microsoft.Xna.Framework;

namespace GameEngine.Input
{
	public class TouchControl
	{
		private MyTouch _myTouch;

		public Vector2 FirstPosition { get; private set; }

		public virtual void Update()
		{
			#region Searching new touch
			if (_myTouch == null)
			{
				_myTouch = InputOptions.MyState.GetBrandNewTouch(false);

				if (_myTouch == null)
				{
					_myTouch = InputOptions.MyState.GetPressedMouse(false);
				}
				
				if (_myTouch != null)
				{
					//Just pressed
					FirstPosition = _myTouch.Position;
				}
			}
			#endregion

			#region Is here some touch
			if (_myTouch != null)
			{
				switch (_myTouch.State)
				{
					#region Continuing
					case TouchState.Continuing:
						if (_myTouch.HasOwner)
						{
							LooseTouch();
						}
						break; 
					#endregion
					#region Relased
					case TouchState.Relased:
						LooseTouch();
						break; 
						#endregion
				}
			
			} 
			#endregion
		}

		public void LooseTouch()
		{
			_myTouch.SetAsNotOwned();
			_myTouch = null;
		}
	}
}
