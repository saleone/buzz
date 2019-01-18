using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Buzz {
    public enum ParticleType { Neutral, Positive, Negative }

   public class Particle {
        protected ParticleType charge = ParticleType.Neutral;
        public ParticleType Charge { 
            get => charge;
            set {
                if (!player) return;
                charge = value; 

                switch (value) {
                    case ParticleType.Positive:
                        sprite = StaticSprites.ParticlePositive;
                        break;
                    case ParticleType.Negative:
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
        
        public Particle(ParticleType charge, Vector2 pos, Vector2? root = null)
        {
            if (root == null) {
                root = BuzzWorld.Center;
            }

            // player is not allowed to move 
            player = root == pos;
            if (player) 
            {
                position = pos;
                this.charge = charge;
            }

            Position = pos;
            Charge = charge;
        }

        public virtual void Update(GameTime time) { 
            if (!player) return;
            
            msFromChargeFlip += (uint)time.ElapsedGameTime.Milliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && msFromChargeFlip >= ChargeFlipFreq) {
                Charge = (Charge == ParticleType.Positive)  ? ParticleType.Negative : ParticleType.Positive;
            }
        }

        public virtual void Draw(GameTime time, SpriteBatch spriteBatch) {
            // TODO: convert this to using with IDisposable hack?
            spriteBatch.Begin();
            spriteBatch.Draw(sprite.Texture, Position - sprite.OriginOffset, Color.White);
            spriteBatch.End();
        }

    }

}