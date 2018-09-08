using ACS;
using System;
using TestApp.Components;

namespace TestApp.Systems
{
	public class DebugPositionSystem : BaseSystem
	{
		public int id;

		public DebugPositionSystem(Context context) : base(context)
		{
			id = 1;
			Require<Position>();
		}

		protected override void OnUpdate()
		{
			Actor actor = null;
			if (actors.TryGetValue(id, out actor))
			{
				Position pos = actor.GetComponent<Position>();
				Console.Write("Actor(" + id + ") : ");
				Console.WriteLine(pos);
			}
		}
	}
}
