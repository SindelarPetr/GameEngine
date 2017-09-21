using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.MathEngine
{
	public static class CollectionHelpers
	{
		public static void ForEach<T>(this IEnumerable<T> inCollection, Action<T> doAction)
		{
			if (doAction == null) throw new ArgumentNullException(nameof(doAction));

			for (int i = 0; i < inCollection.Count(); i++)
			{
				var count = inCollection.Count();

				doAction(inCollection.ElementAt(i));


				i += inCollection.Count() - count;
			}
		}
		public static void ForEachReverse<T>(this IEnumerable<T> inCollection, Action<T> doAction)
		{
			if (doAction == null) throw new ArgumentNullException(nameof(doAction));

			for (int i = inCollection.Count() - 1; i >= 0; i--)
			{
				var count = inCollection.Count();

				doAction(inCollection.ElementAt(i));


				i += inCollection.Count() - count;
			}
		}
	}
}
