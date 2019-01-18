using System;
using System.Collections.Generic;

using MonoGame;
using Microsoft.Xna.Framework;

namespace Buzz 
{
    public static class ParticleSpawner
    {
        public static readonly List<Particle> LiveParticles = new List<Particle>();

        private static readonly Random random = new Random();

        public static void CreateParticle(Vector2? position = null, Charge? charge = null) 
        {
            LiveParticles.Add(new Particle(
                charge ?? ((random.Next(0, 2) == 0) ? Charge.Negative : Charge.Positive), 
                position ?? (new Vector2(random.Next(0, BuzzWorld.WindowWidth), random.Next(0, BuzzWorld.WindowHeight)))
            ));
        } 
    }
}