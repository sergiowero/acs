using ACS;
using TestApp.Components;

namespace TestApp.Systems
{
	public class MovementSystem : BaseSystem
	{
		public MovementSystem(Context context) : base(context)
		{
			Require<Position>();
			Require<Velocity>();
		}

		protected override void OnUpdate()
		{
			foreach (var a in actors)
			{
				Position pos = a.Value.GetComponent<Position>();
				Velocity vel = a.Value.GetComponent<Velocity>();

				pos.x = pos.x + vel.x * context.DeltaTime;
				pos.y = pos.y + vel.y * context.DeltaTime;
			}
		}
	}
}
