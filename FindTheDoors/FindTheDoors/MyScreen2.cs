using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDoors
{
    public class MyScreen2 : GameScreen
    {
        private Game1 _myGame;
        private Texture2D _gameOver;

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public MyScreen2(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            _gameOver = Content.Load<Texture2D>("gameover_scaled_2x_pngcrushed");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.FromNonPremultiplied(35, 35, 35, 255)); // on utilise la reference vers
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_gameOver, new Vector2(0, 0), Color.White);
            _myGame.SpriteBatch.End();                                          // Game1 pour chnager le graphisme
        }
    }
}