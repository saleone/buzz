using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Buzz
{
    public class Sprite {
        public Texture2D Texture { get; private set; }
        public Vector2 OriginOffset { get; private set; }

        public Sprite(Texture2D texture) {
            Texture = texture;
            OriginOffset = new Vector2 {
                X = Texture.Width / 2,
                Y = Texture.Height / 2 
            };
        }
    }
    public static class StaticSprites {

        public static Sprite ParticleNeutral { get; set; }
        public static Sprite ParticlePositive { get; set; }
        public static Sprite ParticleNegative { get; set; }
    }
}