using System.Collections.Generic;

namespace ACS.Events
{
	internal class DispatcherChannel
	{
		public readonly string name;

		private readonly Queue<IEvent> asyncQueue;
		private readonly List<IEventListener> listeners;

		public DispatcherChannel(string name)
		{
			this.name = name;
			asyncQueue = new Queue<IEvent>();
			listeners = new List<IEventListener>(10);
		}

		public void DispatchEvent(IEvent @event)
		{
			for (int i = 0 ; i < listeners.Count ; i++)
			{
				try
				{
					listeners[i].OnEvent(@event);
				}
				catch
				{

				}
			}
		}

		public void DispatchEventAsync(IEvent @event)
		{
			asyncQueue.Enqueue(@event);
		}

		public void AddListener(IEventListener listener)
		{
			if (!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
		}

		public void RemoveListener(IEventListener listener)
		{
			listeners.Remove(listener);
		}

		public void Update()
		{
			if (asyncQueue.Count > 0)
			{
				var @event = asyncQueue.Dequeue();
				DispatchEvent(@event);
			}
		}
	}
}
