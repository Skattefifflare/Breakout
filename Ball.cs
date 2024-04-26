using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout {
    internal class Ball : GameObj {

        public Vector2 dir;
        private Vector2 OGpos;
        private Vector2 OGdir;

        private float powerTimer;

        public Ball(Vector2 Apos, Vector2 Adir, float Ascale){
            scale = new Vector2(Ascale, Ascale);

            pos = Apos;
            OGpos = pos;

            dir = Adir * scale;
            OGdir = dir;


            tex = Texture2D.FromFile(Game1.gd, "../../../imgs/boll2.png");

            powerTimer = 0;
        }

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
            dir = OGdir;

        }
    }
}
