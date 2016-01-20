using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientGame.MenuObjects;
using Microsoft.AspNet.SignalR.Client;

namespace ClientGame
{
    /*

    / s00143557 - Pearce Warrillow
    / This class is to represent a PAUSE MENU, which can be called up during gameplay.
    / Marking scheme says to display GAME DATA, so I'm assuming this means showing each player's HEALTH and SCORES.

    */

    
    public class DisplayMenu
    {
        static IHubProxy proxy;
        MenuButton bttnContinue;

        //Data should be brought down from the SERVER, more specifically from the PLAYERLIST. Four slots, one for each of the four players.
        //If no data available, say N/A (Not Applicable)
        //If possible, display in order of score/health, so that pausing the game shows who's 'winning' or 'losing' at that time.

        public DisplayMenu(Texture2D bttnContinueSprite)
        {
            bttnContinue = new MenuButton(bttnContinueSprite, new Point(700, 570));
            HubConnection connection = new HubConnection("http://localhost:56859");
            proxy = connection.CreateHubProxy("UserInputHub");
            connection.Start().Wait();
        }


        public void Update(GameTime gameTime, Game1 game)
        {
            if (bttnContinue.CheckMouseClick())
            {
                game.currentGameState = gameStates.InGame;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D playerSprite, User cu, int ammoCount, GraphicsDevice gd)
            //Can't display current player's info due to various errors in construction
            //Health isn't done, so that won't be displayed either
        {
            gd.Clear(Color.MidnightBlue);
            bttnContinue.Draw(spriteBatch);
            
            spriteBatch.Draw(playerSprite, new Vector2(50, 50), Color.White);

            string info = cu.Name;
            info += "\nAmmo remaining: ";

            spriteBatch.DrawString(font, Convert.ToString(info + ammoCount), new Vector2(270, 50), Color.PaleGoldenrod);
            
            spriteBatch.DrawString(font, "1: " + proxy.Invoke<int>("Request Scoreboard", cu.Name), new Vector2(600, 250), Color.PaleGoldenrod);

        }
    }


}
