using GameEngine.CameraEngine;
using GameEngine.PropertiesEngine;
using GameEngine.ValueHolders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.ObjectPrimitives
{
	[Flags]
	public enum OriginPositions
	{
		Middle = 1,
		Left = 2,
		Top = 4,
		Right = 8,
		Bottom = 16
	}

	/// <summary>
	/// This is the most basic object which will be used as a game element. It can store position, size, rotation, 
	/// opacity and color, but it is not saying if its a picture, text or anything else.
	/// 
	/// It has 3 layers of each visibility element (position, size, opacity, roatation)
	///  - Local - it is value of local object not affected by anything outside.
	///  - Game - it is value of object in the game (in game world) can by affected by objects parent.
	///  - Absolute - it is value of object on the screen. So it says which opacity/position/scale/rotation will object have when he will be drawn.
	/// </summary>
	public abstract class BaseObject : PrimitiveObject
	{
		private Vector2 _basicPosition;
		private Vector2 _basicSize;

		#region Position
		public virtual Vector2 BasicPosition
		{
			get => _basicPosition;
			set
			{
				_basicPosition = value;
				OnBasicPositionChanged?.Invoke(_basicPosition);
			}
		}

		/// <summary>
		/// Counts the position of the object on the screen. It should be used for drawing so dont 
		/// modificate the returned value for drawing, because this method is used also for implementing touches.
		/// </summary>
		/// <returns>Position on the screen.</returns>
		public Vector2 GetAbsolutePosition() =>
			Camera.TransformPosition(GetWorldPosition());

		public override Vector2 GetWorldPosition() =>
			Camera.TransformPosition(GetLocalPosition(), Vector2.Zero, base.GetWorldScale(), base.GetWorldRotation(), base.GetWorldPosition());

		/// <summary>
		/// Counts the local position of the object. Its position which is NOT affected by camera or parent
		/// </summary>
		/// <returns>The local position.</returns>
		public virtual Vector2 GetLocalPosition() => BasicPosition;
		#endregion

		#region Size

		public Vector2 BasicSize
		{
			get => _basicSize;
			set
			{
				if (_basicSize != value)
				{
					_basicSize = value;
					OnBasicSizeChanged?.Invoke(_basicSize);
				}
			}
		}

		public Vector2 GameSize
		{
			get => BasicSize * BasicScale;
			set => BasicScale = value / BasicSize;
		}

		// Will be applicated to kids.
		public Vector2 BasicScale { get; set; }

		public Vector2 GetAbsoluteScale() => GetWorldScale() * Camera.Zoom;
		public Vector2 GetAbsoluteSize() => GetAbsoluteScale() * BasicSize;
		public override Vector2 GetWorldScale() => GetLocalScale() * base.GetWorldScale();
		public virtual Vector2 GetLocalScale() => BasicScale;
		#endregion

		#region Rotation
		public virtual float BasicRotation { get; set; }

		public float GetAbsoluteRotation() => GetWorldRotation() + Camera.FinalRotation;

		public override float GetWorldRotation() => GetLocalRotation() + base.GetWorldRotation();

		public virtual float GetLocalRotation() => BasicRotation;
		#endregion

		#region Visibility
		public SmoothValue SmoothOpacity { get; }
		public float BasicOpacity { get; set; }

		/// <summary>
		/// This method calculates the independent opacity which is opacity applied only on texture of this object and nothing else.
		/// </summary>
		/// <returns>The calculated independent opacity.</returns>
		protected virtual float GetIndependentOpacity() => 1;

		public virtual float GetLocalOpacity() => BasicOpacity * SmoothOpacity;

		public override float GetWorldOpacity() => GetLocalOpacity() * base.GetWorldOpacity();

		public virtual float GetAbsoluteOpacity() => GetWorldOpacity();

		public MyColorChanger ColorChanger { get; private set; }

		/// <summary>
		/// Is used for calculating origin of the texture while drawing. Origin is calculated as OriginMultiplier * Texture.Size
		/// </summary>
		#endregion

		public Camera Camera { get; }

		public Vector2 OriginMultiplier { get; set; }

		protected SpriteEffects SpriteEffects { get; set; }

		public event Action<Vector2> OnBasicSizeChanged;
		public event Action<Vector2> OnBasicPositionChanged;

		public BaseObject(Camera camera, Vector2 position, Vector2 size, IWorldObject parent = null)
		{
			ColorChanger = new MyColorChanger((MyColor)Color.White);
			BasicOpacity = 1;
			SmoothOpacity = new SmoothValue(1);

			Camera = camera;
			_basicPosition = position;
			_basicSize = size;
			BasicScale = Vector2.One;

			SetOriginMultiplier(OriginPositions.Middle);
		}

		public override void Update()
		{
			ColorChanger.Update();
			SmoothOpacity.Update();
		}

		/// <summary>
		/// Changes Opacity and Color class to instances of baseObjects given in parameter. So it will have the same color and opacity
		/// </summary>
		/// <param name="baseObject"></param>
		public void ReferenceVisuals(BaseObject baseObject)
		{
			ColorChanger = baseObject.ColorChanger;
			BasicOpacity = baseObject.BasicOpacity;
		}

		public void SetOriginMultiplier(OriginPositions originPosition)
		{
			OriginMultiplier = new Vector2(0.5f);

			if (originPosition.HasFlag(OriginPositions.Left)) OriginMultiplier += new Vector2(-0.5f, 0);
			if (originPosition.HasFlag(OriginPositions.Right)) OriginMultiplier += new Vector2(0.5f, 0);
			if (originPosition.HasFlag(OriginPositions.Top)) OriginMultiplier += new Vector2(0, -0.5f);
			if (originPosition.HasFlag(OriginPositions.Bottom)) OriginMultiplier += new Vector2(0, 0.5f);
		}
	}
}
