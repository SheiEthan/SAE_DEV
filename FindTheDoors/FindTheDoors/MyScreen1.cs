using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindTheDoors
{
    public class MyScreen1 : GameScreen
    {
        private Game1 _myGame;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Texture2D _texturePerso;
        public Vector2 _positionPerso;
        private int _sensPerso;
        private KeyboardState _keyboardState;
        private bool test;
        private SpriteFont _police;
        private Texture2D _texturecoeur;
        private Texture2D _textureClef;
        private Vector2 _positioncoeur;
        private Vector2 _positionClef;
        public int _nbCoeur = 3;
        private int _tailleFenetre;
        private int _taillePerso;
        public int _nbMechant = 5;
        private Texture2D[] _textureMechant = new Texture2D[5];
        private Texture2D _textureSlimeIcone;
        private Vector2 _positionSlimeIcone;
        private Vector2[] _positionMechant = new Vector2[5];
        private Texture2D _textureTrappe;
        public Vector2 _positionTrappe;
        private Texture2D[] _textureMur = new Texture2D[5];
        private Vector2[] _positionMur = new Vector2[5];
        private int[] _etatMur = new int[5];
        private int _nbMur = 5;
        private Random rnd = new Random();
        private Texture2D[,] _textureCase = new Texture2D[20, 20];
        private Vector2[,] _positionCase = new Vector2[20, 20];
        private int _nbCase = 20;
        private int _choixMonstre;
        public bool MenuReouvert = false;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public MyScreen1(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            base.Initialize();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Random rnd = new Random();
            _positionPerso = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);
            _positioncoeur = new Vector2(20, 405);
            _positionClef = new Vector2(325, 400);
            _positionSlimeIcone = new Vector2(90, 400);
            _tailleFenetre = 400;
            _taillePerso = 20;
            _nbMechant = 5;
            for (int i = 0; i < _nbMechant; i++)
            {
                _positionMechant[i] = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);
                if (_positionMechant[i].X == _positionPerso.X && _positionMechant[i].Y == _positionPerso.Y)
                    _positionMechant[i] = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);
            }
            for (int i = 0; i < _nbMur; i++)
            {
                _positionMur[i] = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);
                if (_positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                    _positionMur[i] = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);
            }
            _positionTrappe = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);
            for (int i = 0; i < _nbCase; i++)
            {
                for (int j = 0; j < _nbCase; j++)
                {
                    _positionCase[i, j] = new Vector2(i * 20, j * 20);
                }
            }
            _nbCoeur = 3;
            test = true;
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("sable");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _texturePerso = Content.Load<Texture2D>("HeroFace");
            _texturecoeur = Content.Load<Texture2D>("Hearth1");
            _textureClef = Content.Load<Texture2D>("Key2");
            _textureSlimeIcone = Content.Load<Texture2D>("Slime");
            for (int i = 0; i < _nbMechant; i++)
            {
                _textureMechant[i] = Content.Load<Texture2D>("Slime");
            }
            for (int i = 0; i < _nbMur; i++)
            {
                _textureMur[i] = Content.Load<Texture2D>("bush");
            }
            _police = Content.Load<SpriteFont>("Font");
            _textureTrappe = Content.Load<Texture2D>("Exit");


            for (int i = 0; i < _nbCase; i++)
            {
                for (int j = 0; j < _nbCase; j++)
                {
                    _textureCase[i, j] = Content.Load<Texture2D>("case");
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();
            // si fleche droite
            if (_keyboardState.IsKeyUp(Keys.D) || _keyboardState.IsKeyUp(Keys.Q) && (_keyboardState.IsKeyUp(Keys.Z) || _keyboardState.IsKeyUp(Keys.S)))
            {
                _sensPerso = 0;

            }


            if (_positionPerso.X >= _tailleFenetre)
            {
                _positionPerso = new Vector2(0, _positionPerso.Y);
                for (int i = 0; i < _nbMur; i++)
                {
                    if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                    {

                        _positionPerso.X = _tailleFenetre;

                    }
                }

            }
            if (_positionPerso.X < 0)
            {
                _positionPerso = new Vector2(_tailleFenetre - _taillePerso, _positionPerso.Y);
                for (int i = 0; i < _nbMur; i++)
                {
                    if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                    {

                        _positionPerso.X = 0;

                    }
                }
            }
            if (_positionPerso.Y >= _tailleFenetre)
            {
                _positionPerso = new Vector2(_positionPerso.X, 0);
                for (int i = 0; i < _nbMur; i++)
                {
                    if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                    {

                        _positionPerso.Y = _tailleFenetre - _taillePerso;

                    }
                }
            }
            if (_positionPerso.Y < 0)
            {
                _positionPerso = new Vector2(_positionPerso.X, _tailleFenetre - _taillePerso);
                for (int i = 0; i < _nbMur; i++)
                {
                    if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                    {

                        _positionPerso.Y = 0;

                    }
                }
            }
            if (_keyboardState.IsKeyDown(Keys.D) && !(_keyboardState.IsKeyDown(Keys.Q)))
            {
                if (test)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        _sensPerso = 20;
                        _texturePerso = Content.Load<Texture2D>("HeroDroit");
                        _positionPerso.X += _sensPerso;
                    }
                    for (int i = 0; i < _nbMur; i++)
                    {
                        if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                        {

                            _positionPerso.X -= 20;
                            _etatMur[i]++;
                            if(_etatMur[i]>= 3)
                            {
                                _positionMur[i] = new Vector2(-800000000, -8000000);
                            }
                        }
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
                    for (int i = 0; i < _nbMur; i++)
                    {
                        if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                        {

                            _positionPerso.X += 20;
                            _etatMur[i]++;
                            if (_etatMur[i] >= 3)
                            {
                                _positionMur[i] = new Vector2(-800000000, -8000000);
                            }

                        }
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
                    for (int i = 0; i < _nbMur; i++)
                    {
                        if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                        {

                            _positionPerso.Y -= 20;
                            _etatMur[i]++;
                            if (_etatMur[i] >= 3)
                            {
                                _positionMur[i] = new Vector2(-800000000, -8000000);
                            }

                        }
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
                    for (int i = 0; i < _nbMur; i++)
                    {
                        if (test && _positionMur[i].X == _positionPerso.X && _positionMur[i].Y == _positionPerso.Y)
                        {

                            _positionPerso.Y += 20;
                            _etatMur[i]++;
                            if (_etatMur[i] >= 3)
                            {
                                _positionMur[i] = new Vector2(-800000000, -8000000);
                            }

                        }
                    }
                    test = false;
                }
            }
            for (int i = 0; i < _nbMechant; i++)
            {
                if (test && _positionMechant[i].X == _positionPerso.X && _positionMechant[i].Y == _positionPerso.Y)
                {
                    _nbCoeur--;
                    _positionMechant[i] = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);

                }
            }


            if (!test)
            {

                for (int i = 0; i < _nbMechant; i++)
                {
                    if (!test && _positionMechant[i].X == _positionPerso.X && _positionMechant[i].Y == _positionPerso.Y)
                    {
                        _positionMechant[i] = new Vector2((rnd.Next(0, 20)) * 20, (rnd.Next(0, 20)) * 20);
                        _nbMechant -= 1;
                        if (_nbMechant == 0)
                        {
                            _textureClef = Content.Load<Texture2D>("Key1");
                        }
                    }
                   
                    _choixMonstre = rnd.Next(1, 3);
                    if (!test && _positionMechant[i].X == _positionPerso.X)
                    {
                        if (_positionMechant[i].Y > _positionPerso.Y)
                        {
                            _positionMechant[i].Y -= 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].Y += 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i != j)
                                {
                                    _positionMechant[i].Y += 20;
                                }
                            }

                        }
                        if (_positionMechant[i].Y < _positionPerso.Y)
                        {
                            _positionMechant[i].Y += 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].Y -= 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i != j)
                                {
                                    _positionMechant[i].Y -= 20;
                                }
                            }
                        }
                    }
                    else if (_choixMonstre == 2)
                    {
                        if (_positionMechant[i].Y > _positionPerso.Y)
                        {
                            _positionMechant[i].Y -= 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].Y += 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i != j)
                                {
                                    _positionMechant[i].Y += 20;
                                }
                            }


                        }
                        if (_positionMechant[i].Y < _positionPerso.Y)
                        {
                            _positionMechant[i].Y += 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].Y -= 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i != j)
                                {
                                    _positionMechant[i].Y -= 20;
                                }
                            }

                        }
                    }
                    if (!test && _positionMechant[i].Y == _positionPerso.Y)
                    {
                        if (_positionMechant[i].X > _positionPerso.X)
                        {
                            _positionMechant[i].X -= 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].X += 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i != j)
                                {
                                    _positionMechant[i].X += 20;
                                }
                            }

                        }
                        if (_positionMechant[i].X < _positionPerso.X)
                        {
                            _positionMechant[i].X += 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].X -= 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i != j)
                                {
                                    _positionMechant[i].X -= 20;
                                }
                            }
                        }
                    }
                    else if (_choixMonstre == 1)
                    {
                        if (_positionMechant[i].X > _positionPerso.X)
                        {
                            _positionMechant[i].X -= 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].X += 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i != j)
                                {
                                    _positionMechant[i].X += 20;
                                }
                            }

                        }
                        if (_positionMechant[i].X < _positionPerso.X)
                        {
                            _positionMechant[i].X += 20;
                            for (int j = 0; j < _nbMur; j++)
                            {
                                if (_positionMechant[i].X == _positionMur[j].X && _positionMur[j].Y == _positionMechant[i].Y)
                                {
                                    _positionMechant[i].X -= 20;
                                }
                            }
                            for (int j = 0; j < _nbMechant; j++)
                            {
                                if (_positionMechant[i].X == _positionMechant[j].X && _positionMechant[j].Y == _positionMechant[i].Y && i!=j)
                                {
                                    _positionMechant[i].X -= 20;
                                }
                            }

                        }
                    }
                    
                }
                
                Thread.Sleep(250);
                test = true;


            }


            _tiledMapRenderer.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(35, 35, 35, 255));
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturecoeur, _positioncoeur, null, Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_police, $"{_nbCoeur}", new Vector2(60, 407), Color.White);
            _spriteBatch.Draw(_textureClef, _positionClef, null, Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0);
            _spriteBatch.Draw(_textureTrappe, _positionTrappe, Color.White);
            _spriteBatch.Draw(_textureSlimeIcone, _positionSlimeIcone, null, Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_police, $"{_nbMechant}", new Vector2(140, 410), Color.White);

            for (int i = 0; i < _nbCase; i++)
            {
                for (int j = 0; j < _nbCase; j++)
                {
                    _spriteBatch.Draw(_textureCase[i, j], _positionCase[i, j], Color.White);
                }
            }

            for (int i = 0; i < _nbMur; i++)
            {
                _spriteBatch.Draw(_textureMur[i], _positionMur[i], Color.White);
            }
            for (int i = 0; i < _nbMechant; i++)
            {
                _spriteBatch.Draw(_textureMechant[i], _positionMechant[i], Color.White);
            }
            _spriteBatch.Draw(_texturePerso, _positionPerso, Color.White);


            _spriteBatch.End();
        }
    }
}