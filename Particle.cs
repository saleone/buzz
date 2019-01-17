using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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

        public float Scale { get; set; }

        public Particle(ParticleType charge, Vector2 pos, Vector2? root = null, float scale = 0.25f)
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
            Scale = scale;
        }

        public virtual void Update(GameTime time) { 
            if (!player) return;

            position.X = (float)BuzzWorld.Graphics.PreferredBackBufferWidth / 2; 
            position.Y = (float)BuzzWorld.Graphics.PreferredBackBufferHeight / 2;
        }

        public virtual void Draw(GameTime time, SpriteBatch spriteBatch) {
            spriteBatch.Begin(transformMatrix: Matrix.CreateScale(Scale));
            spriteBatch.Draw(sprite.Texture, Position - sprite.OriginOffset * Scale, Color.White);
            spriteBatch.End();
        }

    }

}