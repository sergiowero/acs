
namespace ACS.Events
{
	public interface IEventListener
	{
		void OnEvent(IEvent @event);
	}
}
