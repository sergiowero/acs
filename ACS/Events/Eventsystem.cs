using System.Collections.Generic;

namespace ACS.Events
{
	public class EventSystem : BaseSystem
	{
		private readonly Dictionary<string, DispatcherChannel> channels;

		public EventSystem(Context context) : base(context)
		{
			channels = new Dictionary<string, DispatcherChannel>(20);
		}

		public void DispatchEvent(string channel, IEvent @event)
		{
			GetChannel(channel).DispatchEvent(@event);
		}

		public void DispatchEventAsync(string channel, IEvent @event)
		{
			GetChannel(channel).DispatchEventAsync(@event);
		}

		public void AddListener(string channel, IEventListener listener)
		{
			GetChannel(channel).AddListener(listener);
		}

		public void RemoveListener(string channel, IEventListener listener)
		{
			GetChannel(channel).RemoveListener(listener);
		}

		protected override void OnUpdate()
		{
			foreach (var channel in channels)
			{
				channel.Value.Update();
			}
		}

		private DispatcherChannel GetChannel(string name)
		{
			DispatcherChannel channel = null;

			if (!channels.TryGetValue(name, out channel))
			{
				channel = new DispatcherChannel(name);
				channels[name] = channel;
			}

			return channel;
		}
	}
}
