using ClientGame.MenuObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ClientGame
{
    class MainMenu
    {
        MenuButton bttnLogin;
        MenuButton bttnRegister;
        MenuButton bttnPlay;

        Login.LoginProgram loginWindow;
        Register.RegisterProgram regWindow;


        public MainMenu(Texture2D sprBtnPlay, Texture2D sprBtnReg, Texture2D sprBtnLogin)
        {
            bttnLogin = new MenuButton(sprBtnLogin, new Microsoft.Xna.Framework.Point(250, 50));
            bttnRegister = new MenuButton(sprBtnReg, new Microsoft.Xna.Framework.Point(250, 250));
            bttnPlay = new MenuButton(sprBtnPlay, new Microsoft.Xna.Framework.Point(250, 450));
        }

        public void Update(Game1 game)
        {
            if (bttnLogin.CheckMouseClick() == true)
            {
                var proc = Process.Start("G:/PearceProject/CGames/AnthonyCode/CG2015Assignment/Login/bin/Debug/Login");
                proc.WaitForExit();
                var exitCode = proc.ExitCode;
            }

            if (bttnRegister.CheckMouseClick() == true)
            {
                var proc = Process.Start("G:/PearceProject/CGames/AnthonyCode/CG2015Assignment/Register/bin/Debug/Register");
                proc.WaitForExit();
                var exitCode = proc.ExitCode;
            }

            if (bttnPlay.CheckMouseClick() == true)
            {
                //Check if player is logged in here before allowing game start
                //if (loginWindow.GetValidated())
                //    game.currentUser.Name = loginWindow.GetName();

                game.currentGameState = gameStates.Waiting;
                //Chat isn't implented because there is no multiplayer gameplay. 
                //This code would be set up to count how many users have joined the chat and if the numbe = 4 leave the chat.
                //var proc = Process.Start("G:/PearceProject/CGames/AnthonyCode/CG2015Assignment/Chat/bin/Debug/Chat");
                //if (ServerSide.ChatHub.no == 4)
                //var exitCode = proc.ExitCode;
            }
        }

        public void Draw(SpriteBatch sb, GraphicsDevice gd)
        {
            gd.Clear(Color.MidnightBlue);
            bttnLogin.Draw(sb);
            bttnRegister.Draw(sb);
            bttnPlay.Draw(sb);
        }
    }
}
