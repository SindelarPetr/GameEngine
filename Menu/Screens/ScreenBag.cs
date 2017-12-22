using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.ObjectPrimitives;
using GameEngine.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Menu.Screens
{
	public class ScreenBag : BaseObject, IScreenObject, IScreenParentObject
	{
		#region Constants

		private const double DEFAULT_SHOWING_TIME = 2000;
		#endregion

		#region Show animation
		public override Vector2 GetWorldPosition()
		{
			return base.GetWorldPosition() + ShowValuePosition;
		}
		protected Vector2 ShowValuePosition => Vector2.Zero;
		protected Vector2 ShowValuePositionShift => new Vector2(0, DisplayOptions.Resolution.Y / 5f);

		public override float GetWorldOpacity()
		{
			return base.GetWorldOpacity() * ShowValueOpacity;
		}
		protected virtual float ShowValueOpacity => GetShowValue();

		public override float GetWorldRotation()
		{
			return base.GetWorldRotation() * ShowValueRotation;
		}
		protected virtual float ShowValueRotation => 1;

		public override Vector2 GetWorldScale()
		{
			return base.GetWorldScale() * ShowValueScale;
		}
		protected virtual Vector2 ShowValueScale => Vector2.One;
		#endregion

		/// <summary>
		/// List of nested objects and its draw order for updating and drawing. Draw order tells when the object should be drawed (lower will be drawn earlier). Generally I consider draw order 3 as a middle and 1, 2 as bottom layer and 4 and 5 as upper layer. I don't recommend to use too many of different draw orders.
		/// </summary>
		private List<KeyValuePair<int, IScreenObject>> _nestedObjects;

		#region Show timer
		private ShowTimer _showTimer;
		public double ShowValue => _showTimer.GetShowValue();
		public bool IsShowed => _showTimer.IsShowed();
		public bool IsHidden => _showTimer.IsHidden();
		public bool IsVisible => !_showTimer.IsHidden();
		#endregion

		protected IScreenParentObject ScreenParent => (IScreenParentObject)Parent;

		protected readonly Func<Vector2> PositionProvider;
		protected readonly Func<Vector2> SizeProvider;

		#region Events

		public event Action<KeyValuePair<int, IScreenObject>> OnNestedObjectAdded;
		public event Action<IScreenObject> OnNestedObjectRemoved;
		#endregion

		public ScreenBag(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider,
			IScreenParentObject parent = null)
			: this(camera, positionProvider.Invoke(), sizeProvider.Invoke(), parent)
		{
			PositionProvider = positionProvider;
			SizeProvider = sizeProvider;

			Initialize(out _nestedObjects);
		}

		public ScreenBag(Camera camera, Vector2 position, Vector2 size, IScreenParentObject parent = null)
			: base(camera, position, size, parent)
		{
			Initialize(out _nestedObjects);
		}

		private void Initialize(out List<KeyValuePair<int, IScreenObject>> nestedObjects)
		{
			_showTimer = new ShowTimer(DEFAULT_SHOWING_TIME);
			nestedObjects = new List<KeyValuePair<int, IScreenObject>>();
		}

		protected void AddNestedObject(IScreenObject nestedObject, int drawOrder)
		{
			var pair = new KeyValuePair<int, IScreenObject>(drawOrder, nestedObject);
			_nestedObjects.Add(pair);
			_nestedObjects = _nestedObjects.OrderByDescending(p => p.Key).ToList();
			OnNestedObjectAdded?.Invoke(pair);
		}

		protected void RemoveNestedObject(IScreenObject nestedObject)
		{
			var index = _nestedObjects.FindIndex(p => p.Value == nestedObject);
			if (index == -1)
				return;

			var obj = _nestedObjects[index].Value;
			_nestedObjects.RemoveAt(index);
			OnNestedObjectRemoved?.Invoke(obj);
		}

		public void ForEachNestedObjects(Action<IScreenObject> action)
		{
			_nestedObjects.ForEach(p => action(p.Value));
		}

		public virtual void Hide()
		{
			if (IsHidden) return;

			_showTimer.Reverse();

			_nestedObjects.ForEach(o => o.Value.Hide());
		}

		public virtual void Show(IScreenObject showInitializator = null)
		{
			if (IsShowed) return;

			_showTimer.Reverse();

			_nestedObjects.ForEach(o => o.Value.Show(showInitializator));
		}

		public virtual void AllScreensLoaded()
		{
			_nestedObjects.ForEach(o => o.Value.AllScreensLoaded());
		}

		public virtual void LooseTouches()
		{
			_nestedObjects.ForEach(o => o.Value.LooseTouches());
		}

		public virtual void ResolutionChanged()
		{
			if (PositionProvider != null)
				BasicPosition = PositionProvider.Invoke();

			if (SizeProvider != null)
				BasicSize = SizeProvider.Invoke();

			_nestedObjects.ForEach(o => o.Value.ResolutionChanged());
		}

		public float GetShowValue()
		{
			return (float)_showTimer.GetShowValue();
		}

		public override void Update()
		{
			if (IsHidden) return;

			ForEachNestedObjects(o => o.Update());

			_showTimer.Update();
			base.Update();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (IsHidden) return;

			for (var i = _nestedObjects.Count - 1; i >= 0; i--)
			{
				var obj = _nestedObjects[i];
				obj.Value.Draw(spriteBatch);
			}
		}
	}
}
