using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheDoors
{


    public class ScreenMenu : GameScreen
    {
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;

        // texture du menu avec 3 boutons
        private Texture2D _textBoutons;

        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] lesBoutons;
        public bool DejaJouer = false;

        public ScreenMenu(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[3];
            lesBoutons[0] = new Rectangle(135,287,130,35);
            lesBoutons[1] = new Rectangle(135,342,130,35);
            lesBoutons[2] = new Rectangle(135, 397,130,35);

        }
        public override void LoadContent()
        {
            _textBoutons = Content.Load<Texture2D>("page_scaled_2x_pngcrushed");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            MouseState _mouseState = Mouse.GetState();
            
            for (int i = 0; i < lesBoutons.Length; i++)
            {
                // si le clic correspond à un des 3 boutons
                if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                {
                    if (i == 0)
                    {
                        _textBoutons = Content.Load<Texture2D>("jouer_scaled_2x_pngcrushed");
                        if (i == 0 && _mouseState.LeftButton == ButtonState.Pressed)
                            _myGame.Etat = Game1.Etats.Play;
                    }
                    if (i == 1)
                    {
                        _textBoutons = Content.Load<Texture2D>("option_scaled_2x_pngcrushed");
                        if (i == 1 && _mouseState.LeftButton == ButtonState.Pressed)
                            _myGame.Etat = Game1.Etats.Controls;
                    }
                    if (i == 2)
                    {
                        _textBoutons = Content.Load<Texture2D>("quitter(1)_scaled_2x_pngcrushed");
                        if (i == 2 && _mouseState.LeftButton == ButtonState.Pressed)
                            _myGame.Etat = Game1.Etats.Quit;
                    }

                    // on change l'état défini dans Game1 en fonction du bouton cliqué                    
                    break;
                    }
                _textBoutons = Content.Load<Texture2D>("page_scaled_2x_pngcrushed");
            }


        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(35, 35, 35, 255));
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textBoutons, new Vector2(0, 0), Color.White);
            _myGame.SpriteBatch.End();


        }
    }

}
