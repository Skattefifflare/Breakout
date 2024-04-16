using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout {
    internal class Ball {
        public Vector2 pos;
        public Vector2 dir;
        public Texture2D tex;

        public Ball(Vector2 Apos, Vector2 Adir, Texture2D Atex) {
            pos = Apos;
            dir = Adir;
            tex = Atex;
        }

        public void Draw(SpriteBatch sb, float scale) {
            sb.Begin();
            sb.Draw(tex ,pos, null,  Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.End();
        }
    }
}
