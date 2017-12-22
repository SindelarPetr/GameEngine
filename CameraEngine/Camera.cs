using GameEngine.Input;
using GameEngine.MathEngine;
using GameEngine.Menu.Screens.Buttons;
using GameEngine.Options;
using GameEngine.ValueHolders;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Input.TouchPanel;

namespace GameEngine.CameraEngine
{
	public class Camera
	{
		#region Static
		public static Vector2 TransformPosition(Vector2 position, Vector2 viewMove, Vector2? scale = null, float rotation = 0, Vector2? displayOrigin = null)
		{
			return MyMath.RotatePoint(position + viewMove, rotation) * (scale ?? Vector2.One) + (displayOrigin ?? Vector2.Zero);
		}
		public static Vector2 TransformPosition(Vector2 position, Vector2 scale, float rotation = 0, Vector2? displayOrigin = null)
		{
			return TransformPosition(position, Vector2.Zero, scale, rotation, displayOrigin);
		}

		public static Vector2 ReverseTransformPosition(Vector2 position, Vector2 viewMove, Vector2? scale = null,
			float rotation = 0, Vector2? displayOrigin = null)
		{
			return MyMath.RotatePoint((position - (displayOrigin ?? Vector2.Zero)) / (scale ?? Vector2.One), -rotation) - viewMove;
		}
		#endregion

		public bool CameraLocked = false;

		#region ViewMove
		public Vector2 ActualViewMove => _fingerControlViewMove + ShakeScreen.ShakeViewMove + SmoothViewMove;
		public ShakeScreen ShakeScreen { get; }
		public SmoothVector SmoothViewMove { get; }
		#region Finger control

		private readonly ScreenButton _fingerControl;

		public void DisableFingerControl()
		{
			_fingerControl.IsTouchable = false;
		}
		#endregion

		private Vector2 _fingerControlViewMove;
		#endregion

		public Zoomer Zoomer;

		public float Zoom => Zoomer.FinalZoom;

		public bool EaseViewMoveOnBorder = false;

		public float StaticRotation { get; set; }
		public AngleHolder RotationShaker { get; set; }
		public float FinalRotation => StaticRotation + RotationShaker;

		public event Action<MyTouch> OnClick
		{
			add => _fingerControl.OnClick += value;
			remove => _fingerControl.OnClick -= value;
		}

		public Camera(Vector2? focusedPosition = null)
		{
			CreateRotationShaker();
			ShakeScreen = new ShakeScreen();
			_fingerControl = CreateCameraFingerControl();
			Zoomer = new Zoomer(this);
			SmoothViewMove = new SmoothVector(focusedPosition ?? Vector2.Zero);
		}

		private ScreenButton CreateCameraFingerControl()
		{
			var control = new ScreenButton(this, Vector2.Zero, DisplayOptions.Resolution);
			control.IsFullscreenButton = true;
			control.IsDraggingAllowed = true;
			control.OnDragging += ControlOnDragging;

			return control;
		}

		private void ControlOnDragging(ScreenButton sender, Vector2 vector)
		{
			_fingerControlViewMove += vector;
		}

		private void CreateRotationShaker()
		{
			RotationShaker = new AngleHolder
			{
				BackForce = 0.05f,
				Friction = 0.80f
			};
		}

		public virtual void Update()
		{
			#region Counting parts of ViewMove
			ShakeScreen.Update();

			_fingerControl.Update();

			SmoothViewMove.Update();
			#endregion

			if (!InputOptions.MyState.MouseWheelHandelt)
			{
				Zoomer.BasicZoom += InputOptions.MyState.MouseWheelDiff / 1000f;
				InputOptions.MyState.MouseWheelHandelt = true;
			}

			Zoomer.Update();
			RotationShaker.Update();
		}

		public void ShakeByMove(Vector2 force)
		{
			ShakeScreen.ApplyShakeForce(force);
		}

		public void ShakeByRotation(float force = 0.02f)
		{
			RotationShaker.ApplyForce(force);
		}

		public Vector2 TransformPosition(Vector2 position)
		{
			return TransformPosition(position, ActualViewMove, new Vector2(Zoom), StaticRotation + RotationShaker, DisplayOptions.MiddleOfScreen);
		}

		/// <summary>
		/// Calculates the position in the game from absolute position (position on the screen).
		/// </summary>
		/// <param name="absolutePosition">Position on the screen from which will be calculated a game position.</param>
		/// <returns></returns>
		public Vector2 ReverseTransformPosition(Vector2 absolutePosition)
		{
			return ReverseTransformPosition(absolutePosition, ActualViewMove, new Vector2(Zoom), FinalRotation,
				DisplayOptions.MiddleOfScreen);
		}

		#region Zoom
		public void SetZoom(float value)
		{
			Zoomer.ZoomTo(CropZoom(value));
		}

		private float CropZoom(float zoom)
		{
			return MyMath.CutValue(zoom, CameraOptions.MinZoom, CameraOptions.MaxZoom);
		}
		#endregion
	}
}
