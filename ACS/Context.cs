using System;
using System.Collections.Generic;
using System.Text;

namespace ACS
{
	public class Context
	{

		public int ActorCount { get { return actors.Count; } }

		private const int DEFAULT_POOL_CAPACITY = 10;
		private static Context context;

		private readonly List<Actor> actors;
		private readonly List<System> systems;
		private readonly Dictionary<Type, IComponentPool> pools;

		private int actorIdCounter;

		public static Context Instance
		{
			get
			{
				if (context == null)
				{
					context = new Context();
				}
				return context;
			}
		}

		private Context()
		{
			actorIdCounter = 0;
			actors = new List<Actor>();
			systems = new List<System>();
			pools = new Dictionary<Type, IComponentPool>();
		}

		public void CleanUp()
		{
			actors.Clear();
			systems.Clear();
		}

		public void AddSystem(System system)
		{
			systems.Add(system);
		}

		public void RemoveSystem(System system)
		{
			systems.Remove(system);
		}

		public Actor CreateActor()
		{
			var actor = new Actor(++actorIdCounter, this);
			actors.Add(actor);
			return actor;
		}

		private void RemoveActor(Actor actor)
		{
			actors.Remove(actor);
		}

		public void Update()
		{
			for (int i = 0 ; i < systems.Count ; i++)
			{
				systems[i].Update();
			}
		}

		internal TComponent GetOrCreateComponent<TComponent>() where TComponent : class, IComponent, new()
		{
			if (pools.TryGetValue(typeof(TComponent), out IComponentPool pool))
			{
				return (TComponent)pool.Get();
			}
			else
			{
				var concretePool = new ComponentPool<TComponent>(DEFAULT_POOL_CAPACITY);
				pools[typeof(TComponent)] = concretePool;
				return (TComponent)concretePool.Get();
			}
		}

		internal void RecycleComponent(IComponent component)
		{
			if (pools.TryGetValue(component.GetType(), out IComponentPool pool))
			{
				pool.Put(component);
			}
		}
	}
}
