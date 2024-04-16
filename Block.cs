using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    internal class Block {
        public Vector2 pos;
        public Texture2D tex;
        public Vector2 scale;


        public Block(Vector2 _pos, Texture2D _tex) {
            tex = _tex;
            pos = _pos;
        }

        public void Draw(SpriteBatch sb) {
            sb.Begin();
            sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            sb.End();

        }
    }
}
