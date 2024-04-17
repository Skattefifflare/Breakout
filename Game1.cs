using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace Breakout {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public static GraphicsDevice gd;
        

        SpriteFont font;

        Ball ball;
        Effect blockshader;
        Level level1;
        MenuButton startButton;


        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            gd = GraphicsDevice;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            Helper.screenwidth = _graphics.PreferredBackBufferWidth;
            Helper.screenheight = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font1");    
           

            ball = new Ball(new Vector2(10, 300), new Vector2(300, 200));           
            blockshader = Content.Load<Effect>("blockshader");
            level1 = new Level(1);
            level1.CreateBlocks();

            startButton = new MenuButton(new Vector2(200, 300), "BEGIN", font); // funkar bara när x är litet
            
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Helper.gametime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Helper.totalgametime += Helper.gametime;

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            if (kstate.IsKeyDown(Keys.Space)) {
                startButton.isVisible = true;
            }

            startButton.Click(mstate);

            ball.Update();
            BatchCollision();
           

            base.Update(gameTime);
        }

        void BatchCollision() {
            foreach (var block in level1.blocks) {
                if (SphereAABBCollision(ref ball, block)) {
                    level1.blocks.Remove(block);
                    break;
                }
            }
        }
        bool SphereAABBCollision(ref Ball ball, Block block) {

            float bax = ball.pos.X;
            float bay = ball.pos.Y;

            float blx = block.pos.X;
            float bly = block.pos.Y;

            float blw = block.tex.Width * block.scale.X;
            float blh = block.tex.Height * block.scale.Y;


            Vector2 clampPoint = new Vector2(Math.Clamp(bax+ball.tex.Width/2, blx, blx+blw), Math.Clamp(bay+ball.tex.Height/2, bly, bly+blh)); // den närmaste punkten till bollens mittpunkt som ligger på blockets kanter

            Vector2 ballmidpoint = new Vector2(ball.pos.X+ball.tex.Width/2, ball.pos.Y+ball.tex.Height/2);

            Vector2 diffVec = ballmidpoint - clampPoint; // vektorn mellan bollens mittpunkt och clamppunkten
            

            if (diffVec.Length() <= ball.tex.Width/2) {
                ball.pos -= ball.dir * Helper.gametime;
                diffVec.Normalize();
                double diffRad = Math.Atan2(diffVec.Y, diffVec.X); // omvandla diffvektorn till radianer på enhetscirkeln

                Vector2 dirVec = Vector2.Normalize(ball.dir);
                double dirRad = Math.Atan2(dirVec.Y, dirVec.X);

                double angle = Math.Abs(diffRad - dirRad);
                dirRad += (dirRad > diffRad) ? -angle * 2 : angle * 2;

                Vector2 newDir = new Vector2((float)Math.Cos(dirRad), (float)Math.Sin(dirRad));
                newDir *= -1;

                ball.dir = newDir *ball.dir.Length();
                return true;
            }
            else {
                return false;
            }
            
        }
        
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(39, 42, 53));

            ball.Draw();
            level1.Draw(blockshader);
            startButton.Draw();
           
            base.Draw(gameTime);
        }
    }
}
