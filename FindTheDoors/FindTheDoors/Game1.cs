﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Graphics;
using System;

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
        private KeyboardState _keyboardState;
        private bool test;
        private SpriteFont _police;
        private Texture2D _texturecoeur;
        private Texture2D _textureClef;
        private Vector2 _positioncoeur;
        private Vector2 _positionClef;
        private int _nbCoeur;
        private int _tailleFenetre;
        private int _taillePerso;
        private int _nbMechant = 5;
        private int _tailleMechant;
        private Texture2D[] _textureMechant = new Texture2D[5];
        private Vector2[] _positionMechant = new Vector2[5];
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
            _nbMechant = 5;
            _positionPerso = new Vector2(0,0);
            _positioncoeur = new Vector2(20, 405);
            _positionClef = new Vector2(325, 400);
            _tailleFenetre = 400;
            _taillePerso = 20;
            _tailleMechant = 20;
      
              Random rnd = new Random();
            for (int i = 0; i < _nbMechant; i++)
            {
                _positionMechant[i] = new Vector2((rnd.Next(0,20))*20, (rnd.Next(0,20))*20);
            }
            _nbCoeur = 3;
            test = true;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("sable");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _texturePerso = Content.Load<Texture2D>("HeroFace");
            _texturecoeur = Content.Load<Texture2D>("Hearth1");
            _textureClef = Content.Load<Texture2D>("Key2");
            for (int i = 0; i < _nbMechant; i++)
            {
                _textureMechant[i] = Content.Load<Texture2D>("Slime");
            }
            _police = Content.Load<SpriteFont>("Font");
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
            if (_positionPerso.X >= _tailleFenetre)
            {
                _positionPerso = new Vector2(0, _positionPerso.Y);
            }
            if (_positionPerso.X < 0)
            {
                _positionPerso = new Vector2(_tailleFenetre - _taillePerso, _positionPerso.Y);
            }
            if (_positionPerso.Y >= _tailleFenetre)
            {
                _positionPerso = new Vector2(_positionPerso.X, 0);
            }
            if (_positionPerso.Y < 0)
            {
                _positionPerso = new Vector2(_positionPerso.X, _tailleFenetre - _taillePerso);
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
            if (_keyboardState.IsKeyDown(Keys.T))
            { test = true; }
            base.Update(gameTime);
            _tiledMapRenderer.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturecoeur, _positioncoeur, null, Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_police, $"{_nbCoeur}", new Vector2(60, 407), Color.White);
            _spriteBatch.Draw(_textureClef, _positionClef, null, Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0);
            
            for (int i = 0; i < _nbMechant; i++)
            {
                _spriteBatch.Draw(_textureMechant[i], _positionMechant[i], Color.White);
            }
            _spriteBatch.Draw(_texturePerso, _positionPerso, Color.White);


            _spriteBatch.End(); 
            base.Draw(gameTime);

            
        }
    }
}