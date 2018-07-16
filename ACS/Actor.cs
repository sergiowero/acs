using System;
using System.Collections.Generic;
using System.Text;

namespace ACS
{
	public class Actor
	{
		public readonly int id;

		private readonly Context context;
		private readonly List<IComponent> components;

		internal Actor(int id, Context context)
		{
			this.id = id;
			this.context = context;
			components = new List<IComponent>();
		}

		public TComponent AddComponent<TComponent>() where TComponent : IComponent, new()
		{
			var component = context.GetOrCreateComponent<TComponent>();
			components.Add(component);
			return component;
		}

	}
}
