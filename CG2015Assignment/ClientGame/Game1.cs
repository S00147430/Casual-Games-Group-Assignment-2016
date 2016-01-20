using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.AspNet.SignalR.Client;

namespace ClientGame
{
    public enum gameStates { Main, Waiting, Pause, InGame };

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static IHubProxy proxy;

        public User currentUser = new User();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Variables

        Texture2D _texture; // Player texture
        Texture2D background;   // Backgroung in character selection
        Texture2D gameBackground;   // Background in game
        public Rectangle _rectangle; // Player rectangle

        DisplayMenu pauseMenu;
        MainMenu mainMenu;

        Texture2D spriteContinueBttn;
        Texture2D spriteLoginBttn;
        Texture2D spriteRegBttn;
        Texture2D spritePlayBttn;
        SpriteFont defaultFont;

        SpriteFont font;
        Vector2 fontPosition;   // Font variables

        Vector2 _origin;
        public Vector2 _position;
        float rotation;

        Vector2 _velocity;
        float tangentialVelocity = 5f;
        float friction = 0.1f;  // Lenght of time a ship will continue to drift before fully stops

        int ammo;
        int ammoFont;
        int power;  // Ship variables
        int health;
        bool canFire = true;

        //enum GameState { MainMenu, Playing }    // States

        public gameStates currentGameState = gameStates.Main;

        Vector2 distance;

        List<Projectiles> projectiles = new List<Projectiles>();

        KeyboardState pastKey;  // Helps to keep projectiles to be fired at once

        Camera camera;

        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1536;
            graphics.PreferredBackBufferHeight = 768;   // Size of screen
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }


        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
            HubConnection connection = new HubConnection("http://localhost:56859");
            proxy = connection.CreateHubProxy("UserInputHub");
            connection.Start().Wait();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("SpriteFonts/font");    // Loads in font

            _texture = Content.Load<Texture2D>("Sprites/Player");   // Loads in texture
            _position = new Vector2(768, 384);  // Sets position

            background = Content.Load<Texture2D>("Sprites/characterselect");
            gameBackground = Content.Load<Texture2D>("Sprites/space_background");
            spriteContinueBttn = Content.Load<Texture2D>("Sprites/ContinueBttn");
            spriteRegBttn = Content.Load<Texture2D>("Sprites/RegBttn");
            spriteLoginBttn = Content.Load<Texture2D>("Sprites/LoginBttn");
            spritePlayBttn = Content.Load<Texture2D>("Sprites/PlayBttn");

            mainMenu = new MainMenu(spritePlayBttn, spriteRegBttn, spriteLoginBttn);
            pauseMenu = new DisplayMenu(spriteContinueBttn);
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            fontPosition = new Vector2(_position.X - 720, _position.Y - 380);   // Font position set to follow player

            MouseState mouse = Mouse.GetState();
            IsMouseVisible = true;

            //rotation = (float)Math.Atan2(distance.Y, distance.X);     Set for player to look at mouse and rotate with it accordingly



            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                rotation -= 0.1f;       // Rotates ship to the left when A is pressed
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                rotation += 0.1f;       // Rotates ship to the right when D is pressed
            }


            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);    // Rectangle is set to player position and texture size
            _position = _velocity + _position;
            _origin = new Vector2(_rectangle.Width / 2, _rectangle.Height / 2); // Center of player texture



            switch (currentGameState)
            {
                case gameStates.Waiting:

                    camera.center = new Vector2(0, 0);

                    if (Keyboard.GetState().IsKeyDown(Keys.D1))  // If 1 is pressed in character selection menu
                    {
                        currentGameState = gameStates.InGame;
                        _texture = Content.Load<Texture2D>("Sprites/Player");   // Sets player texture to this
                        ammoFont = 15;  // This is there to keep count of current ammo
                        _position = new Vector2(300, 250);
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D2)) // If 2 is pressed in character selection menu
                    {
                        currentGameState = gameStates.InGame;
                        _texture = Content.Load<Texture2D>("Sprites/Player2");  // Sets player texture to this
                        ammoFont = 10;
                        _position = new Vector2(400, 350);  // Sets position
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D3))
                    {
                        currentGameState = gameStates.InGame;
                        _texture = Content.Load<Texture2D>("Sprites/Player3");
                        ammoFont = 3;
                        _position = new Vector2(500, 450);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D4))
                    {
                        currentGameState = gameStates.InGame;
                        _texture = Content.Load<Texture2D>("Sprites/Player4");
                        ammoFont = 30;
                        _position = new Vector2(600, 550);
                    }
                    break;

                case gameStates.InGame:

                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        _velocity.X = (float)Math.Cos(rotation) * tangentialVelocity;   // Lets the player move forward according to the direction it is facing
                        _velocity.Y = (float)Math.Sin(rotation) * tangentialVelocity;
                    }
                    else if (_velocity != Vector2.Zero)
                    {
                        Vector2 i = _velocity;

                        _velocity = i -= friction * i; // Lets the ship drift for a while after W key has been let go
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
                    {
                        ammoFont--;
                        if (ammoFont <= 0)      // Changes ammo count for font
                        {
                            ammoFont = 0;
                        }
                        Shooting(); // Updates the Shooting method
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentGameState = gameStates.Pause;
                    }
                    break;

                case gameStates.Pause:

                    pauseMenu.Update(gameTime, this);
                    break;

                case gameStates.Main:

                    mainMenu.Update(this);
                    break;
            }


            pastKey = Keyboard.GetState();  // Updates the variable
            UpdateProjectiles();

            camera.Update(gameTime, this);

            #region Boundaries
            if (_position.X < -275)
            {
                _position.X = -275;
            }
            if (_position.Y < -510)          // Keeps the player from going off screen
            {
                _position.Y = -510;
            }

            if (_position.X > 2525)
            {
                _position.X = 2525;
            }

            if (_position.Y > 1500)
            {
                _position.Y = 1500;
            }
            #endregion

            base.Update(gameTime);
        }

        public void UpdateProjectiles()
        {



            foreach (Projectiles projectile in projectiles)
            {

                projectile.texture = Content.Load<Texture2D>("Sprites/projectile_blue");

                if (_texture == Content.Load<Texture2D>("Sprites/Player2"))
                {
                    projectile.texture = Content.Load<Texture2D>("Sprites/projectile_blue_super");
                }
                if (_texture == Content.Load<Texture2D>("Sprites/Player3"))
                {
                    projectile.texture = Content.Load<Texture2D>("Sprites/projectile_green");
                }
                if (_texture == Content.Load<Texture2D>("Sprites/Player4"))
                {
                    projectile.texture = Content.Load<Texture2D>("Sprites/projectile_red");
                }

                projectile.position += projectile.velocity; // Projectile position is added and set to it's velocity
                if (Vector2.Distance(projectile.position, _position) > 2500) // Turns projectile invisible if distance between player is big enough
                    projectile.isVisible = false;

            }
            //for (int i = 0; i < projectiles.Count; i++)
            //{
            //    if (!projectiles[i].isVisible)
            //    {
            //        projectiles.RemoveAt(i);          // Used to remove not needed projectiles, not needed after ammo added into game
            //        //i--;
            //    }
            //}

        }

        #region ProjectileCode
        public void Shooting()
        {
            Projectiles newProjectile = new Projectiles();

            newProjectile.velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 5f + _velocity;
            newProjectile.rotation = (float)Math.Atan2(newProjectile.velocity.Y, newProjectile.velocity.X); // Projectile rotation set to direction the player sprite is facing
            newProjectile.position = _position + newProjectile.velocity * 5;
            newProjectile.isVisible = true;

            if (_texture == Content.Load<Texture2D>("Player"))
            {
                ammo = 15;
                power = 10;
            }
            else if (_texture == Content.Load<Texture2D>("Player2"))
            {
                ammo = 10;
                power = 20;
            }
            else if (_texture == Content.Load<Texture2D>("Player3"))        // Variables set according to which ship has been picked
            {
                ammo = 3;
                power = 80;
            }
            else if (_texture == Content.Load<Texture2D>("Player4"))
            {
                ammo = 30;
                power = 5;
            }

            if (projectiles.Count() < ammo && canFire == true)  // If the count of projectiles is less than the ammo value and bool set to true
                projectiles.Add(newProjectile); // A new projectile will be added
            if (projectiles.Count() == ammo)    // However if the count is equal to ammo value (15)
            {
                canFire = false;    // Can no longer fire projectiles
            }


        }
        #endregion ProjectileCode

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);    // Camera

            switch (currentGameState)
            {
                case gameStates.Waiting:
                    spriteBatch.Draw(background, new Vector2(0, 0), Color.White);   // Background in character selection
                    break;


                case gameStates.InGame:
                    spriteBatch.Draw(gameBackground, new Vector2(-500, -500));  // Backgroind in game

                    spriteBatch.Draw(_texture, _position, null, Color.White, rotation, _origin, 1f, SpriteEffects.None, 0); // Player drawn according to variables

                    spriteBatch.DrawString(font, Convert.ToString("Ammo : " + ammoFont), fontPosition, Color.White);    // Font for ammo

                    foreach (Projectiles projectile in projectiles)
                        projectile.Draw(spriteBatch);
                    break;

                case gameStates.Main:
                    mainMenu.Draw(spriteBatch, GraphicsDevice);
                    break;

                case gameStates.Pause:
                    pauseMenu.Draw(spriteBatch, font, _texture, currentUser, ammoFont, GraphicsDevice);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
