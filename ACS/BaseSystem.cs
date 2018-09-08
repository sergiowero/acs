using System;
using System.Collections.Generic;

namespace ACS
{
	public abstract class BaseSystem
	{
		public bool enabled;

		protected readonly Context context;
		protected readonly SortedDictionary<int, Actor> actors;
		protected readonly HashSet<Type> requiredComponents;

		public BaseSystem(Context context)
		{
			enabled = true;
			this.context = context;
			actors = new SortedDictionary<int, Actor>();
			requiredComponents = new HashSet<Type>();
		}

		protected void Require<TComponent>() where TComponent : class, IComponent
		{
			requiredComponents.Add(typeof(TComponent));
		}

		public void ProccessActor(Actor actor)
		{
			if (actor.HasComponents(requiredComponents))
			{
				actors[actor.id] = actor;
			}
			else
			{
				actors.Remove(actor.id);
			}
		}

		public void Clean()
		{
			actors.Clear();
			requiredComponents.Clear();
		}

		protected abstract void OnUpdate();

		public void Update()
		{
			OnUpdate();
		}
	}
}
