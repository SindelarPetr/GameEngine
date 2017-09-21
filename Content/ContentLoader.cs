using GameEngine.Options;

namespace GameEngine.Content
{
	public class ContentLoader<T>
	{
		#region Operators
		public static implicit operator T(ContentLoader<T> loader) => loader.ContentItem;
		#endregion

		public T ContentItem =>
			_contentItem == null ? _contentItem = GeneralOptions.Content.Load<T>(_path) : _contentItem;

		private T _contentItem;
		private readonly string _path;

		public ContentLoader(string path)
		{
			_path = path;
		}
	}
}
