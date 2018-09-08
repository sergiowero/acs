using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS
{
	public class Actor
	{
		public delegate void ActorComponnetDelegate(Actor actor, IComponent component);

		public event ActorComponnetDelegate OnComponentAdded;
		public event ActorComponnetDelegate OnComponentRemoved;

		public readonly int id;

		private readonly Context context;
		private readonly Dictionary<Type, IComponent> componentsMap;

		internal Actor(int id, Context context)
		{
			this.id = id;
			this.context = context;
			componentsMap = new Dictionary<Type, IComponent>();
		}

		public TComponent GetComponent<TComponent>() where TComponent : class, IComponent
		{
			IComponent comp = null;
			if (componentsMap.TryGetValue(typeof(TComponent), out comp))
			{
				return (TComponent)comp;
			}
			return null;
		}

		public bool HasComponents(IEnumerable<Type> types)
		{
			int count = componentsMap.Select(x => x.Key).Intersect(types).Count();
			return count == types.Count();
		}

		public TComponent AddComponent<TComponent>() where TComponent : class, IComponent, new()
		{
			var component = context.GetOrCreateComponent<TComponent>();
			componentsMap[component.GetType()] = component;
			OnComponentAdded?.Invoke(this, component);
			return component;
		}

		public void RemoveComponent<TComponent>(TComponent component) where TComponent : class, IComponent, new()
		{
			if (componentsMap.Remove(component.GetType()))
			{
				context.RecycleComponent(component);
				OnComponentRemoved?.Invoke(this, component);
			}
		}

		internal void RemoveAllComponents()
		{
			foreach (var comp in componentsMap)
			{
				context.RecycleComponent(comp.Value);
				OnComponentRemoved?.Invoke(this, comp.Value);
			}
			componentsMap.Clear();
		}
	}
}
