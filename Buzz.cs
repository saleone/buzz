using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Buzz
{
    public class BuzzWorld : Game
    {
        public static GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;

        public static Vector2 Center;

        public Particle playerParticle;

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
            
            playerParticle = new Particle(ParticleType.Neutral, Center);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            playerParticle.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            playerParticle.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
