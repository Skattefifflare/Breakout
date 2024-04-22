using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout {
    internal class MenuButton : GameObj{

        string text;
        SpriteFont font;

        public bool isVisible;
        public bool isClicked;

        public MenuButton(Vector2 Apos, string Atext, SpriteFont Afont) { 
            pos = Apos;           
            text = Atext;       
            tex = Texture2D.FromFile(Game1.gd, "imgs/block.png");
            font = Afont;
            scale = new Vector2((text.Length*10.5f)/tex.Width, .5f);

            isVisible = false;
            isClicked = false;
        }

        public void Draw() {
            if (isVisible) {
                sb.Begin();
                sb.Draw(tex, pos - new Vector2(2, 2), null, Color.PeachPuff, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                sb.DrawString(font, text, pos, Color.Black);
                sb.End();
            }          
        }

        public void Click(MouseState Astate) {
            if (isVisible) {
                if (Astate.X> pos.X && Astate.X < pos.X+tex.Width*scale.X) {
                    if (Astate.Y > pos.Y && Astate.X < pos.Y+tex.Height*scale.Y) {
                        if (Astate.LeftButton == ButtonState.Pressed) {
                            isClicked = true;
                            isVisible = false;
                        }
                        isClicked = true;
                        isVisible = false;
                    }
                }
            }
        }
    }
}
