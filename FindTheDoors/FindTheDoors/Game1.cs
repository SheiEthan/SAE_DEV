using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Graphics;
using System;
using System.Threading;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Screens;

namespace FindTheDoors
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        private SpriteBatch _spriteBatch;
        private MyScreen1 _myScreen1;
        private MyScreen2 _myScreen2;
        private ScreenMenu _screenMenu;
        public enum Etats { Menu, Controls, Play, Quit };
        private Etats etat;

        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }

        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // Par défaut, le 1er état flèche l'écran de menu
            Etat = Etats.Menu;

            // on charge les 2 écrans 
            _screenMenu = new ScreenMenu(this);
            _myScreen1 = new MyScreen1(this);
            _myScreen2 = new MyScreen2(this);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 400;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState _mouseState = Mouse.GetState();
            if (_myScreen1._nbCoeur <= 0)
            {
                _myScreen1._nbCoeur = 3;
                _screenManager.LoadScreen(_myScreen2, new FadeTransition(GraphicsDevice, Color.Black));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play && _screenMenu.DejaJouer == false)
                {
                    _screenManager.LoadScreen(_myScreen1, new FadeTransition(GraphicsDevice, Color.Black));
                    _screenMenu.DejaJouer = true;
                    _myScreen1.MenuReouvert = false;
                }


            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back) && _myScreen1.MenuReouvert == false)
            {
                if (this.Etat == Etats.Play)
                {
                    _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
                    _myScreen1.MenuReouvert = true;
                    _screenMenu.DejaJouer = false;
                }
                    
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}