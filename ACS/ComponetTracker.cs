using System;
using System.Collections.Generic;
using System.Text;

namespace ACS
{
	internal class ComponetTracker
	{
		private const int DEFAULT_LIST_CAPACITY = 100;

		private readonly Dictionary<Type, List<Actor>> componentsMap;

		internal ComponetTracker()
		{
			componentsMap = new Dictionary<Type, List<Actor>>();
		}

		internal void TrackActor(Actor component)
		{
			if (!componentsMap.TryGetValue(component.GetType(), out List<Actor> list))
			{
				list = new List<Actor>(DEFAULT_LIST_CAPACITY);
				componentsMap[component.GetType()] = list;
			}

			list.Add(component);
		}

		internal void ComponentRemoved(Actor actor)
		{
			if (componentsMap.TryGetValue(actor.GetType(), out List<Actor> list))
			{
				list.Remove(actor);
			}
		}

	}
}
