using ACS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACS
{
	public class Context
	{

		public int ActorCount { get { return actors.Count; } }
		public float DeltaTime { get; private set; }
		public EventSystem Eventsystem { get; }

		private const int DEFAULT_POOL_CAPACITY = 10;

		private readonly List<Actor> actors;
		private readonly List<BaseSystem> systems;
		private readonly Dictionary<Type, IComponentPool> pools;
		private int actorIdCounter;

		public Context()
		{
			actorIdCounter = 0;
			actors = new List<Actor>();
			systems = new List<BaseSystem>();
			pools = new Dictionary<Type, IComponentPool>();
			Eventsystem = new EventSystem(this);
			AddSystem(Eventsystem);
		}

		public void Destroy()
		{
			for (int i = actors.Count - 1 ; i >= 0 ; i--)
			{
				var actor = actors[i];
				actors.RemoveAt(i);
				actor.RemoveAllComponents();
				actor.OnComponentAdded -= OnActorComponentAdded;
				actor.OnComponentRemoved -= OnActorComponentRemoved;
			}

			for (int i = systems.Count - 1 ; i >= 0 ; i--)
			{
				var system = systems[i];
				system.Clean();
				systems.RemoveAt(i);
			}
		}

		public void AddSystem(BaseSystem system)
		{
			systems.Add(system);
			foreach (var actor in actors)
			{
				system.ProccessActor(actor);
			}
		}

		public void RemoveSystem(BaseSystem system)
		{
			systems.Remove(system);
			system.Clean();
		}

		public Actor CreateActor()
		{
			var actor = new Actor(++actorIdCounter, this);
			actors.Add(actor);
			actor.OnComponentAdded += OnActorComponentAdded;
			actor.OnComponentRemoved += OnActorComponentRemoved;
			return actor;
		}

		private void RemoveActor(Actor actor)
		{
			actors.Remove(actor);
			actor.RemoveAllComponents();
			actor.OnComponentAdded -= OnActorComponentAdded;
			actor.OnComponentRemoved -= OnActorComponentRemoved;
		}

		public void Update(float deltaTime)
		{
			this.DeltaTime = deltaTime;
			for (int i = 0 ; i < systems.Count ; i++)
			{
				if (systems[i].enabled)
				{
					systems[i].Update();
				}
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

		private void OnActorComponentAdded(Actor actor, IComponent component)
		{
			foreach (var sys in systems)
			{
				sys.ProccessActor(actor);
			}
		}

		private void OnActorComponentRemoved(Actor actor, IComponent component)
		{
			foreach (var sys in systems)
			{
				sys.ProccessActor(actor);
			}
		}
	}
}
