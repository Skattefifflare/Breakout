using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout {
    internal class Ball {
        public Vector2 pos;
        public Vector2 dir;       
        public static Texture2D tex = Texture2D.FromFile(Game1.gd, "imgs/boll2.png");

        
        public Ball(Vector2 Apos, Vector2 Adir) {
            pos = Apos;
            dir = Adir;
        }

        public void Draw(SpriteBatch sb) {
            sb.Begin();
            sb.Draw(tex ,pos, null,  Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            sb.End();
        }

        public void WallCollision(float width, float height) {
            if (pos.X < 0 || pos.X + tex.Width > width) {
                dir.X *= -1;
            }
            if (pos.Y < 0 || pos.Y + tex.Height > height) {
                dir.Y *= -1;
            }
        }

        public void Update() {
            pos += dir * Helper.gametime;
            WallCollision(Helper.screenwidth, Helper.screenheight);
        }
    }
}
