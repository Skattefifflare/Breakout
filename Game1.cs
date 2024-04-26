using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Breakout {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public static GraphicsDevice gd;
        SpriteFont font;        
        List<Level> levels;        
        

        private int currentLevel;

        // bools for printing text
        private bool gameStarted;
        private bool nextLevel;
        private bool gameWon;

        
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
            
            Helper.shader = Content.Load<Effect>("blockshader");
            Helper.timer = 0;
            
            currentLevel = 0; // levelindex in list

            CreateLevels();

            gameStarted = false;
            nextLevel = false;
            gameWon = false;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Helper.gametime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Helper.totalgametime += Helper.gametime;

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            if (kstate.IsKeyDown(Keys.Space)) {
                gameStarted = true;
            }

            if (gameStarted && !nextLevel && !gameWon) {
                levels[currentLevel].Update(kstate);

                if (levels[currentLevel].CheckWin()) {
                    currentLevel++;
                    if (currentLevel > levels.Count) {
                        gameWon = true;
                    }
                    nextLevel = true;
                }
            }
            

            base.Update(gameTime);
        }

        void CreateLevels() {
            levels = new List<Level>();
            DirectoryInfo d = new DirectoryInfo("../../../levels/");
            int levelindex = 1;
            foreach (var file in d.GetFiles("*.txt")) {
                levels.Add(new Level(levelindex));
                levelindex++;
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(39, 42, 53));

            if (!gameStarted) {
                PrintText("PRESS 'SPACE' TO BEGIN");
            }
            else if (nextLevel) {
                if (Helper.timer < 3) {
                    PrintText("LEVEL COMPLETED!");
                    Helper.timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else {
                    Helper.timer = 0;
                    nextLevel = false;
                }
        
            }
            else if (gameWon) {
                if (Helper.timer < 5) {
                    PrintText($"YOU WON!!!");
                    Helper.timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else {
                    Helper.timer = 0;
                    Exit();
                }                
            }
            else {
                levels[currentLevel].Draw();                
            }
            
            base.Draw(gameTime);
        }

        private void PrintText(string text) {           
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, text, new Vector2(Helper.screenwidth / 2 - text.Length*10, Helper.screenheight / 2 - 10), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
    }
}
