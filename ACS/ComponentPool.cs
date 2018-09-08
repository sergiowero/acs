using System.Collections.Generic;

namespace ACS
{
	public interface IComponentPool
	{
		IComponent Get();
		void Put(IComponent component);
	}

	internal class ComponentPool<TComponent> : IComponentPool where TComponent : class, IComponent, new()
	{
		private readonly List<TComponent> pool;
		private readonly int capacity;

		public ComponentPool(int capacity)
		{
			this.capacity = capacity;
			pool = new List<TComponent>(capacity);
		}

		public IComponent Get()
		{
			if (pool.Count > 0)
			{
				var component = pool[0];
				pool.RemoveAt(0);
				component.Reset();
				return component;
			}
			else
			{
				return new TComponent();
			}
		}

		public void Put(IComponent component)
		{
			if (component is TComponent comp)
			{
				if (pool.Count < pool.Capacity)
				{
					pool.Add(comp);
				}
			}
		}
	}
}
