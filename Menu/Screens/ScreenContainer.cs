using GameEngine.CameraEngine;
using GameEngine.MathEngine;
using GameEngine.Menu.Screens;
using GameEngine.Options;
using GameEngine.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Menu.ScreensAs
{
	/// <summary>
	/// TODO: Solve enable and disable animations
	/// </summary>
	public class ScreenContainer : BaseObject, IMenuScreenElement, IScreenParentObject
	{
		#region Constants

		private const double DEFAULT_SHOWING_TIME = 2000;
		#endregion

		#region Show animation
		public override Vector2 GetGamePosition()
		{
			return base.GetGamePosition() + ShowValuePosition;
		}
		protected Vector2 ShowValuePosition => Vector2.Zero;
		protected Vector2 ShowValuePositionShift => new Vector2(0, DisplayOptions.Resolution.Y / 5f);

		public override float GetGameOpacity()
		{
			return base.GetGameOpacity() * ShowValueOpacity;
		}
		protected virtual float ShowValueOpacity => GetShowValue();

		public override float GetGameRotation()
		{
			return base.GetGameRotation() * ShowValueRotation;
		}
		protected virtual float ShowValueRotation => 1;

		public override Vector2 GetGameScale()
		{
			return base.GetGameScale() * ShowValueScale;
		}
		protected virtual Vector2 ShowValueScale => Vector2.One;
		#endregion

		/// <summary>
		/// List of nested objects and its draw order for updating and drawing. Draw order tells when the object should be drawed (lower will be drawn earlier). Generally I consider draw order 3 as a middle and 1, 2 as bottom layer and 4 and 5 as upper layer. I don't recommend to use too many of different draw orders.
		/// </summary>
		private List<KeyValuePair<int, IMenuScreenElement>> _nestedObjects;

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

		public event Action<KeyValuePair<int, IMenuScreenElement>> OnNestedObjectAdded;
		public event Action<IMenuScreenElement> OnNestedObjectRemoved;
		#endregion

		public ScreenContainer(Camera camera, Func<Vector2> positionProvider, Func<Vector2> sizeProvider,
			IScreenParentObject parent = null)
			: this(camera, positionProvider.Invoke(), sizeProvider.Invoke(), parent)
		{
			PositionProvider = positionProvider;
			SizeProvider = sizeProvider;

			Initialize(out _nestedObjects);
		}

		public ScreenContainer(Camera camera, Vector2 position, Vector2 size, IParentObject parent = null)
			: base(camera, position, size, parent)
		{
			Initialize(out _nestedObjects);
		}

		private void Initialize(out List<KeyValuePair<int, IMenuScreenElement>> nestedObjects)
		{
			_showTimer = new ShowTimer(DEFAULT_SHOWING_TIME);
			nestedObjects = new List<KeyValuePair<int, IMenuScreenElement>>();
		}

		protected void AddNestedObject(IMenuScreenElement nestedObject, int drawOrder)
		{
			var pair = new KeyValuePair<int, IMenuScreenElement>(drawOrder, nestedObject);
			_nestedObjects.Add(pair);
			_nestedObjects = _nestedObjects.OrderByDescending(p => p.Key).ToList();
			OnNestedObjectAdded?.Invoke(pair);
		}

		protected void RemoveNestedObject(IMenuScreenElement nestedObject)
		{
			var index = _nestedObjects.FindIndex(p => p.Value == nestedObject);
			if (index == -1)
				return;

			var obj = _nestedObjects[index].Value;
			_nestedObjects.RemoveAt(index);
			OnNestedObjectRemoved?.Invoke(obj);
		}

		public void ForEachNestedObjects(Action<IMenuScreenElement> action)
		{
			_nestedObjects.ForEach(p => action(p.Value));
		}

		public virtual void Hide()
		{
			if (IsHidden) return;

			_showTimer.Reverse();

			foreach (var obj in _nestedObjects)
			{
				obj.Value.Hide();
			}
		}

		public virtual void Show(IMenuScreenElement showInitializator = null)
		{
			if (IsShowed) return;

			_showTimer.Reverse();

			foreach (var obj in _nestedObjects)
			{
				obj.Value.Show(showInitializator);
			}
		}

		public virtual void AllScreensLoaded()
		{
			_nestedObjects.ForEach(o => o.Value.AllScreensLoaded());
		}

		public virtual void LooseTouches()
		{
			foreach (var obj in _nestedObjects)
			{
				obj.Value.LooseTouches();
			}
		}

		public virtual void ResolutionChanged()
		{
			if (PositionProvider != null)
				BasicPosition = PositionProvider.Invoke();

			if (SizeProvider != null)
				BasicSize = SizeProvider.Invoke();

			foreach (var obj in _nestedObjects)
			{
				obj.Value.ResolutionChanged();
			}
		}

		public float GetShowValue()
		{
			return (float)_showTimer.GetShowValue();
		}

		public override void Update()
		{
			if(IsHidden) return;

			ForEachNestedObjects(o => o.Update());

			_showTimer.Update();
			base.Update();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if(IsHidden) return;

			for (var i = _nestedObjects.Count - 1; i >= 0; i--)
			{
				var obj = _nestedObjects[i];
				obj.Value.Draw(spriteBatch);
			}
		}
	}
}
