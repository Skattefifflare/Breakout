using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    internal class GameObj {
        public Vector2 pos;
        public Vector2 scale;
        public Texture2D tex;

        protected SpriteBatch sb = new SpriteBatch(Game1.gd);

        virtual public void Draw() {
            sb.Begin();
            sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.End();
        }
    }
}
