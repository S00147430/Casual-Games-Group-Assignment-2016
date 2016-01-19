using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CG2015Assignment
{
    /*

    / s00143557 - Pearce Warrillow
    / This class is to represent a PAUSE MENU, which can be called up during gameplay.
    / Marking scheme says to display GAME DATA, so I'm assuming this means showing each player's HEALTH and SCORE.
    / (This could also mean displaying HIGH SCORES, but that can be done in a seperate menu if needed.)

    */


    class DisplayMenu
    {

        //Data should be brought down from the SERVER, more specifically from the PLAYERLIST. Four slots, one for each of the four players.
        //If no data available, say N/A (Not Applicable)
        //If possible, display in order of score/health, so that pausing the game shows who's 'winning' or 'losing' at that time.

        public DisplayMenu()
        {
            //code to interperate display goes here, feed in playerStates and display
        }


        public void Update(GameTime gameTime, Game1 game)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                //In full game, Changes Gamestate to InGame or equivalent.
                //As of this writing, my attemptsto pass through an enum state change have been fruitless, so for now just closes the game.
                game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice gd)//GraphicsDevice will not be passed to this draw in the finished version, purely for test purposes
        {
            //Just changes background color for now, so I know the state is being changed

            gd.Clear(Color.Firebrick);

            //spriteBatch.Begin();
            //spriteBatch.End();

        }
    }


}
