using GameEngine.CameraEngine;
using GameEngine.Properties;
using Microsoft.Xna.Framework;

namespace GameEngine.Primitives
{
	public class EffectMakingObject : GameObject
	{
		protected IEffectManager EffectManager { get; set; }

		public EffectMakingObject(Camera camera, IEffectManager effectManager, Vector2 position, Vector2 size, IParentObject parent = null, MyTexture2D texture = null)
			: base(camera, position, size, parent, texture)
		{
			EffectManager = effectManager;
		}
	}
}
