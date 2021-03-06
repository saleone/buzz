using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Buzz {
    public enum Charge { Neutral, Positive, Negative }

   public class Particle {
        public const float Mass = 0.1f;
        public float Magnitude {get; private set;}
        public Vector2 Speed {get; set;} = Vector2.Zero;
        public Vector2 Acceleration {get; set;} = Vector2.Zero;
        protected Charge charge = Charge.Neutral;
        public Charge Charge { 
            get => charge;
            set {

                charge = value; 
                switch (value) {
                    case Charge.Positive:
                        sprite = StaticSprites.ParticlePositive;
                        break;
                    case Charge.Negative:
                        sprite = StaticSprites.ParticleNegative;
                        break;
                    default:
                        sprite = StaticSprites.ParticleNeutral;
                        break;
                }
                msFromChargeFlip = 0;
            }
        }

        protected Vector2 position = Vector2.Zero;
        public Vector2 Position {
            get {
                return position;
            }

            set {
                if (player) return;
                position = value;
            }
        }

        private Sprite sprite; 
        private bool player = false;

        public uint ChargeFlipFreq { get; private set; } = 100;
        private uint msFromChargeFlip = 0;
        
        public Particle(Charge charge, Vector2 pos, float magnitude = 1)
        {
            player = BuzzWorld.Center == pos;
            if (player) position = pos;

            Position = pos;
            Charge = charge;
            Magnitude = magnitude;
        }

        public virtual void Update(GameTime time) { 
            
            if (player) {
                msFromChargeFlip += (uint)time.ElapsedGameTime.Milliseconds;
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && msFromChargeFlip >= ChargeFlipFreq) {
                    Charge = (Charge == Charge.Positive)  ? Charge.Negative : Charge.Positive;
                }
                return;
            }

            float elapsedSeconds = (float)time.ElapsedGameTime.TotalSeconds;
            Position += Speed * elapsedSeconds;
            Speed += Acceleration * elapsedSeconds;

            Acceleration = Algorithm.CoulombsLaw(BuzzWorld.PlayerParticle, this);
            foreach (Particle p in ParticleSpawner.LiveParticles)
                Acceleration += Algorithm.CoulombsLaw(p, this);
        }

        public virtual void Draw(GameTime time, SpriteBatch spriteBatch, bool useBatch = true) {
            // TODO: convert this to using with IDisposable hack?
            if (useBatch) spriteBatch.Begin();
            spriteBatch.Draw(sprite.Texture, Position - sprite.OriginOffset, Color.White);
            if (useBatch) spriteBatch.End();
        }

    }

}