using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Buzz
{
    public class BuzzWorld : Game
    {
        public static GraphicsDeviceManager Graphics { get; private set; }

        public static readonly int WindowWidth = 700;
        public static readonly int WindowHeight = 700;

        SpriteBatch spriteBatch;

        public static Vector2 Center { get; private set;}

        public static Particle PlayerParticle {get; private set;}

        public BuzzWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 700;
            Graphics.PreferredBackBufferHeight = 700;
            Graphics.ApplyChanges();

            Window.AllowUserResizing = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Center = new Vector2{
                X = Graphics.PreferredBackBufferWidth / 2,
                Y = Graphics.PreferredBackBufferHeight / 2
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            StaticSprites.ParticleNeutral = new Sprite(Content.Load<Texture2D>("particle-neutral"));
            StaticSprites.ParticlePositive = new Sprite(Content.Load<Texture2D>("particle-positive"));
            StaticSprites.ParticleNegative = new Sprite(Content.Load<Texture2D>("particle-negative"));
            
            PlayerParticle = new Particle(Charge.Positive, Center);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            PlayerParticle.Update(gameTime);

            if (ParticleSpawner.LiveParticles.Count < 10)
                ParticleSpawner.CreateParticle();

            foreach (Particle p in ParticleSpawner.LiveParticles)
                p.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            PlayerParticle.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();
            foreach (Particle p in ParticleSpawner.LiveParticles)
                p.Draw(gameTime, spriteBatch, useBatch: false);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
