using GameEngine.GamePrimitives;
using GameEngine.ObjectPrimitives;
using GameEngine.Options;
using GameEngine.PropertiesEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Effects.Tail
{
	public class TextureSender : ContainerGameObject
	{
		private readonly BaseObject _owner;

		#region Frequency
		/// <summary>
		/// Represents the number of sent textures pres second.
		/// </summary>
		public int Frequency { get; set; }

		private float NextGenTime => 1000f / Frequency;

		private float _nextTextureGenTime;
		#endregion

		public float Direction { get; set; }
		public float Velocity { get; set; }

		public TextureSender(BasicLevel level, BaseObject owner, int frequency, Vector2 size, float velocity = 0.1f, IWorldObject parent = null, MyTexture2D texture = null)
			: base(level, owner.BasicPosition, size, parent, texture)
		{
			_owner = owner;
			Frequency = frequency;
			Velocity = velocity;
		}

		public override void Update()
		{
			base.Update();
			UpdateGeneration();
		}

		private void UpdateGeneration()
		{
			_nextTextureGenTime -= (float)GeneralOptions.GameTime.ElapsedGameTime.TotalMilliseconds;
			if (_nextTextureGenTime <= 0)
			{
				_nextTextureGenTime = NextGenTime;

				GeneratePart();
			}
		}

		private void GeneratePart()
		{
			var part = new TextureDisolve(Level, _owner.GetLocalPosition(), BasicSize, this,
				Velocity, _owner.GetLocalRotation() + MathHelper.PiOver2, 0, Texture);
			part.ColorChanger.ResetColor(ColorChanger);
			part.SmoothOpacity.Friction = SmoothOpacity.Friction;
			AddObject(part);
		}
	}
}
