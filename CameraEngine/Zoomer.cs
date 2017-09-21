using GameEngine.ValueHolders;

namespace GameEngine.CameraEngine
{
	public class Zoomer
	{
		public float FinalZoom => BasicZoom * _smoothZoom;

		public float DefaultZoom { get; set; }
		public float BasicZoom { get; set; }

		private readonly SmoothValue _smoothZoom;


		public Zoomer(Camera camera)
		{
			_smoothZoom = new SmoothValue(1f)
			{
				BasicSpeed = 0.00005f,
				Friction = 0.01f
			};

			DefaultZoom = BasicZoom = 1f;
		}

		public void Update()
		{
			_smoothZoom.Update();
		}

		public void ZoomTo(float value)
		{
			_smoothZoom.ValueToGo = value;
		}

		public void ZoomToDefault()
		{
			ZoomTo(DefaultZoom);
		}
	}
}