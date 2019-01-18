using System;

using MonoGame;
using Microsoft.Xna.Framework;

namespace Buzz
{

    public static class Algorithm
    {
        private static readonly float CoulombsConst = 100.0f;
        public static Vector2 CoulombsLaw(Particle p1, Particle p2) =>
            CoulombsLaw(
                p1.Position - p2.Position, 
                p1.Magnitude, p2.Magnitude,
                (p1.Charge == p2.Charge) ? -1 : 1);

        public static Vector2 CoulombsLaw(Vector2 r, float magnitude1, float magnitude2, int direction_factor = 1) 
        {
            if (r.Length() == 0) 
                return Vector2.Zero;
            return CoulombsConst * magnitude1 * magnitude2 / r.LengthSquared() * r * direction_factor;
        }
    }
}