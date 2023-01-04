using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Graphics;

namespace FindTheDoors
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Texture2D _texturePerso;
        private Vector2 _positionPerso;
        private int _sensPerso;
        private int _count;
        private KeyboardState _keyboardState;
        private bool test;
        private Texture2D _texturecoeur1;
        private Texture2D _textureclef;
        private Texture2D _texturecoeur2;
        private Texture2D _texturecoeur3;
        private Vector2 _positioncoeur1;
        private Vector2 _positionclef;
        private Vector2 _positioncoeur2;
        private Vector2 _positioncoeur3;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 400;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.ApplyChanges();
            _positionPerso = new Vector2(50, 50);
            _positioncoeur1 = new Vector2(20, 425);
            _count = 0;
            test = true;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("sable");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _texturePerso = Content.Load<Texture2D>("HeroFace");
            _texturecoeur1 = Content.Load<Texture2D>("Hearth1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();
            // si fleche droite
            if (_keyboardState.IsKeyUp(Keys.D) || _keyboardState.IsKeyUp(Keys.Q) && (_keyboardState.IsKeyUp(Keys.Z) || _keyboardState.IsKeyUp(Keys.S)))
            {
                _sensPerso = 0;
               
            }
            if (_keyboardState.IsKeyDown(Keys.D)  && !(_keyboardState.IsKeyDown(Keys.Q)))
            {
                if (test)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        _sensPerso = 20;
                        _texturePerso = Content.Load<Texture2D>("HeroDroit");
                        _positionPerso.X += _sensPerso;
                    }
                    test = false;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)))
            {
                if (test)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        _sensPerso = -20;
                        _texturePerso = Content.Load<Texture2D>("HeroGauche");
                        _positionPerso.X += _sensPerso;
                    }
                    test = false;
                }
            }
            if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)))
            {
                if (test)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        _sensPerso = 20;
                        _texturePerso = Content.Load<Texture2D>("HeroFace");
                        _positionPerso.Y += _sensPerso;
                    }
                    test = false;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)))
            {
                if (test)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        _sensPerso = -20;
                        _texturePerso = Content.Load<Texture2D>("HeroDos");
                        _positionPerso.Y += _sensPerso;
                    }
                    test = false;
                }
            }

            base.Update(gameTime);
            _tiledMapRenderer.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturePerso, _positionPerso, Color.White);
            _spriteBatch.Draw(_texturecoeur1, _positioncoeur1, Color.White);
            _spriteBatch.Draw(_texturecoeur1,                                  // Texture (Image)
                     _positioncoeur1,                               // Position de l'image
                     null,                                       // Zone de l'image à afficher
                     Color.White,                                // Teinte
                     0,         // Rotation (en rad)
                     new Vector2(0,0),  // Origine
                     3.0f,                                       // Echelle
                     SpriteEffects.None,                         // Effet
                     0);
            _spriteBatch.End(); 
            base.Draw(gameTime);

            
        }
    }
}