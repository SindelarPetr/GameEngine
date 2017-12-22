using GameEngine.Input;
using GameEngine.Input.TouchPanel;
using GameEngine.Options;
using Microsoft.Xna.Framework;

namespace GameEngine.CameraEngine
{
	public class CameraFingerControl
	{
		public Vector2 ViewMove { get; private set; }
		public float FingerZoom { get; private set; }
		public bool Enabled { get; private set; } = true;

		private MyTouch _myTouch1;
		private bool _isMyTouch1Mouse;
		private MyTouch _myTouch2;
		private Vector2 _zoomTouchesAveragePosition;

		private readonly Camera _camera;

		private bool _firstTouchMoved;
		private readonly float _firstTouchMoveDist;
		private Vector2 _firstTouchPosition;

		public CameraFingerControl(Camera camera)
		{
			FingerZoom = 1;
			_firstTouchMoveDist = DisplayOptions.Resolution.X / (DisplayOptions.PresentationScreen == PresentationScreen.Small ? 110 : 220);

			_camera = camera;
		}

		private void LooseTouch1()
		{
			_myTouch1?.SetAsNotOwned();
			_myTouch1 = null;
		}

		private void LooseTouch2()
		{
			_myTouch2?.SetAsNotOwned();
			_myTouch2 = null;
		}

		private void LooseTouches()
		{
			LooseTouch1();
			LooseTouch2();
		}

		public void Update()
		{
			if (!Enabled) return;

			//Check touches if they are still active
			if (_myTouch1 != null)
			{
				if (_myTouch1.IsRelased())
				{
					LooseTouch1();
				}
			}

			if (_myTouch2 != null)
			{
				if (_myTouch2.IsRelased()) LooseTouch2();
			}

			//If second touch is active but first is not, then lets move second to first
			if (_myTouch2 != null && _myTouch1 == null)
			{
				_myTouch1 = _myTouch2;
				_myTouch2 = null;
			}

			//Get new touches
			if (_myTouch1 == null)
			{
				if ((_myTouch1 = InputOptions.MyState.GetBrandNewTouch(false)) != null)
				{
					_firstTouchMoved = false;
					_firstTouchPosition = _myTouch1.Position;
					_isMyTouch1Mouse = false;
				}
				else
				{
					//Try get mouse
					if ((_myTouch1 = InputOptions.MyState.GetPressedMouse(true)) != null)
					{
						_firstTouchMoved = false;
						_firstTouchPosition = _myTouch1.Position;
						_isMyTouch1Mouse = true;
					}
				}
			}

			if (_myTouch2 == null && _myTouch1 != null && !_isMyTouch1Mouse)
			{
				if ((_myTouch2 = InputOptions.MyState.GetBrandNewTouchBut(_myTouch1)) != null)
				{
					//Ready for zooming
					_zoomTouchesAveragePosition = (_myTouch1.Position + _myTouch2.Position) / 2;

					_myTouch1.SetAsOwned();
					_firstTouchMoved = true;
				}
			}

			if (_myTouch1 != null)
			{
				if (_myTouch2 == null)
				{
					if (!_firstTouchMoved)
					{
						if (Vector2.Distance(_myTouch1.Position, _firstTouchPosition) > _firstTouchMoveDist)
						{
							//Start dragging
							_firstTouchMoved = true;
							HardAdjustFocusPosition(_myTouch1.Position - _firstTouchPosition);
							_myTouch1.SetAsOwned();
						}
					}
					else
					{
						// There is only one touch - dragging!
						HardAdjustFocusPosition(_myTouch1.Move);
					}
				}
				else
				{
					// There are two touches - finger zooming!
					#region Fingerzooming!
					Vector2 previousDiff = _myTouch1.PreviousTouch.Position - _myTouch2.PreviousTouch.Position;
					float previousDist = previousDiff.Length();

					Vector2 currDiff = _myTouch1.Position - _myTouch2.Position;
					float currDist = currDiff.Length();

					FingerZoom *= currDist / previousDist;

					if (FingerZoom < CameraOptions.MinZoom) FingerZoom = CameraOptions.MinZoom;
					if (FingerZoom > CameraOptions.MaxZoom) FingerZoom = CameraOptions.MaxZoom;

					Vector2 newAveragePosition = (_myTouch1.Position + _myTouch2.Position) / 2;
					HardAdjustFocusPosition(newAveragePosition - _zoomTouchesAveragePosition);

					_zoomTouchesAveragePosition = newAveragePosition;
					#endregion
				}
			}
		}

		private void HardAdjustFocusPosition(Vector2 position)
		{
			position = -position * (1 / _camera.Zoom) - ViewMove;

			ViewMove = -position;
		}

		public void Disable()
		{
			Enabled = false;
			LooseTouches();
		}

		public void Enable()
		{
			Enabled = true;
		}
	}
}
