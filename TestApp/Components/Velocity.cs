using ACS;

namespace TestApp.Components
{
	public class Velocity : IComponent
	{
		public float x;
		public float y;

		public void Reset()
		{
			x = 0f;
			y = 0f;
		}
	}
}
