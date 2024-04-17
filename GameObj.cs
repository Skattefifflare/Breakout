using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout {
    internal class GameObj {
        public Vector2 pos;
        public Vector2 scale;
        public Texture2D tex;

        protected SpriteBatch sb = new SpriteBatch(Game1.gd);
    }
}
