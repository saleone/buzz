using System;

using MonoGame;
using Microsoft.Xna.Framework;

namespace Buzz
{

    public static class Algorithm
    {
        private static readonly float CoulombsConst = 10.0f;
        public static Vector2 CoulombsLaw(Particle p1, Particle p2) =>
            CoulombsLaw(p1.Position - p2.Position);

        public static Vector2 CoulombsLaw(Vector2 r) =>
            CoulombsConst / r.Length() * r;
    }
}