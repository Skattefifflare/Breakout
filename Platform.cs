using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout {
    internal class Platform : GameObj {

        private float speed;
        private float acceleration;

        private Vector2 OGpos;

        public Platform(Vector2 Apos, Vector2 Ascale) {
            pos = Apos;
            OGpos = pos;

            scale = Ascale;
            tex = Texture2D.FromFile(Game1.gd, "../../../imgs/block.png");
            speed = 0;
            acceleration = 20;
        }

        public void Update(KeyboardState Akstate) {

            if (speed < 600) {
                speed += acceleration;
            }
            if (Akstate.GetPressedKeyCount() == 0 || Akstate.GetPressedKeyCount() == 2) {
                speed = 100;
            }
            if (Akstate.IsKeyDown(Keys.Left)) {
                pos.X -= speed * Helper.gametime;
            }
            if (Akstate.IsKeyDown(Keys.Right)) {                
                pos.X += speed * Helper.gametime;                
            }
            
            WallCollision();
        }

        private void WallCollision() {
            if (pos.X < 0) {
                pos.X = 0;
            }
            if (pos.X + tex.Width * scale.X > Helper.screenwidth) {
                pos.X = Helper.screenwidth- tex.Width*scale.X;
            }           
        }
       
        // gör till gemensam
        public void Reset() {
            pos = OGpos;
        }
    }
}
