using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout {
    internal class MenuButton : GameObj {

        string text;
        SpriteFont font;

        public bool isVisible;
        

        Vector2 boxpos;
        Vector2 boxscale;

        public MenuButton(Vector2 Apos, string Atext, SpriteFont Afont, float Ascale) {
            pos = Apos;
            text = Atext;
            tex = Texture2D.FromFile(Game1.gd, "imgs/block.png");
            font = Afont;
            scale = new Vector2(Ascale, Ascale);

            
            boxscale = new Vector2(scale.X * 0.21f * text.Length, scale.Y / 3f);
            boxpos = new Vector2(pos.X, pos.Y + 5 *boxscale.Y);

            isVisible = true;
        }

        override public void Draw() {
            if (isVisible) {
                sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp,null, null, null, null);
                sb.Draw(tex, boxpos, null, Color.PeachPuff, 0f, Vector2.Zero, boxscale, SpriteEffects.None, 0f);
                sb.DrawString(font, text, pos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                sb.End();
            }
        }

        public bool Click(MouseState Astate) {
            if (isVisible) {
                if (Astate.Y > boxpos.Y && Astate.Y < boxpos.Y + tex.Height * boxscale.Y) {
                    if (Astate.X > boxpos.X && Astate.X < boxpos.X + tex.Width * boxscale.X) {
                    
                        if (Astate.LeftButton == ButtonState.Pressed) {                      
                            isVisible = false;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
