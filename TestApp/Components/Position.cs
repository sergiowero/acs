﻿using ACS;

namespace TestApp.Components
{
	public class Position : IComponent
	{
		public float x;
		public float y;

		public void Reset()
		{
			x = 0f;
			y = 0f;
		}

		public override string ToString()
		{
			return "Position [" + x + "," + y + "]";
		}
	}
}
