using System.Collections.Generic;
using System.Linq;

namespace ACS
{
	public abstract class System
	{
		protected readonly Context context;

		public System(Context context)
		{
			this.context = context;
		}

		protected IEnumerable<Actor> GetActors<TMeta>() where TMeta : struct
		{
			return Enumerable.Empty<Actor>();
		}

		protected abstract void OnUpdate();

		public void Update()
		{
			OnUpdate();
		}
	}
}
