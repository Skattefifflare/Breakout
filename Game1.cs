using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Breakout {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public static GraphicsDevice gd;
        SpriteFont font;        
        List<Level> levels;        
        
        
        int currentLevel;
        
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
            
            currentLevel = 0; // levelindex in list

            CreateLevels();
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Helper.gametime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Helper.totalgametime += Helper.gametime;

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();


            levels[currentLevel].Update(kstate);

            if (levels[currentLevel].CheckWin()) {
                currentLevel++;
                
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

            levels[currentLevel].Draw();
            
            base.Draw(gameTime);
        }
    }
}
