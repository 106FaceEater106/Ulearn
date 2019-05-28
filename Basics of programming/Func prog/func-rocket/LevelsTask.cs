using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();

		public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero", 
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200), 
				(size, v) => Vector.Zero, standardPhysics);

            yield return new Level("Heavy",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) => new Vector(0, 0.9), standardPhysics);

            yield return new Level("Up",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(700, 500),
                (size, v) => new Vector(0, -300.0/(v.Length+300)), standardPhysics);

            yield return new Level("WhiteHole",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) => new Vector(140 * v.Length / (v.Length * v.Length + 1), -140*v.Length/(v.Length*v.Length+1)), standardPhysics);

            yield return new Level("BlackHole",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) => Vector.Zero, standardPhysics);

            yield return new Level("BlackAndWhite",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) => Vector.Zero, standardPhysics);

        }
	}
}