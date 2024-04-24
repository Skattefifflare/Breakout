using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout {
    internal class Ball : GameObj {

        public Vector2 dir;
        public Vector2 OGpos;
        public Ball(Vector2 Apos, Vector2 Adir, float Ascale) {
            pos = Apos;
            OGpos = pos;
            scale = new Vector2(Ascale, Ascale);
            dir = Adir * scale;
            tex = Texture2D.FromFile(Game1.gd, "imgs/boll2.png");

        }

        //public void Draw() {
        //    sb.Begin();
        //    sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        //    sb.End();
        //}      

        public bool WallCollision() {
            
            if (pos.Y + tex.Height * scale.Y > Helper.screenheight) {
                Reset();
                return true;
            }
            else {
                if (pos.X < 0 || pos.X + tex.Width * scale.X > Helper.screenwidth) {
                    pos -= dir * Helper.gametime;
                    dir.X *= -1;
                }
                if (pos.Y < 0) {
                    pos -= dir * Helper.gametime;
                    dir.Y *= -1;
                }
                return false;
            }
        }

        public void Update() {
            pos += dir * Helper.gametime;
            
        }
        public void Reset() {
            pos = OGpos;

        }

    }
}
