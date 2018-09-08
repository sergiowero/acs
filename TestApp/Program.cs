using ACS;
using System;
using System.Threading;
using TestApp.Components;
using TestApp.Systems;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Context context = new Context();

			Console.WriteLine("Adding Movement system");
			MovementSystem movement = new MovementSystem(context);
			context.AddSystem(movement);

			Console.WriteLine("Adding debug system");
			DebugPositionSystem debugPosition = new DebugPositionSystem(context);
			context.AddSystem(debugPosition);

			CreateActor(context);

			while (!Console.KeyAvailable)
			{
				Thread.Sleep(40);
				context.Update(0.04f);
			}

			context.Destroy();
			context = null;
		}

		static void CreateActor(Context context)
		{
			Actor actor = context.CreateActor();
			var pos = actor.AddComponent<Position>();
			var vel = actor.AddComponent<Velocity>();

			vel.x = 1f;
			vel.y = 1f;

			Console.WriteLine($"Actor created (id={actor.id})");
		}
	}
}
