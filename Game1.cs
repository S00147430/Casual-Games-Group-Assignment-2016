using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project
{


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D _texture;
        Rectangle _rectangle;

        Enemy enemy = new Enemy();

        Vector2 _origin;
        Vector2 _position;
        float rotation;

        Vector2 _velocity;
        float tangentialVelocity = 5f;
        float friction = 0.1f;




        Vector2 distance;

        List<Projectiles> projectiles = new List<Projectiles>();
        KeyboardState pastKey;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1536;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {        
            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _texture = Content.Load<Texture2D>("Player");
            enemy.LoadContent(Content);
            _position = new Vector2(300, 250);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            MouseState mouse = Mouse.GetState();
            IsMouseVisible = true;

            distance.X = mouse.X - _position.X;
            distance.Y = mouse.Y - _position.Y;

            rotation = (float)Math.Atan2(distance.Y, distance.X);

            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            _position = _velocity + _position;
            _origin = new Vector2(_rectangle.Width / 2, _rectangle.Height / 2);


            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _velocity.X = (float)Math.Cos(rotation) * tangentialVelocity;
                _velocity.Y = (float)Math.Sin(rotation) * tangentialVelocity;
            }
            else if(_velocity != Vector2.Zero)
            {
                Vector2 i = _velocity;

                _velocity = i -= friction * i;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
                Shooting();

            UpdateProjectiles();

            base.Update(gameTime);
        }

        public void UpdateProjectiles()
        {
            foreach(Projectiles projectile in projectiles)
            {
                if (enemy.boundingRectangle.Intersects(projectile.boundingRectangle) && enemy.isVisible)
                {
                    projectile.isVisible = false;
                    enemy.isVisible = false;
                }

                projectile.position += projectile.velocity;
                if (Vector2.Distance(projectile.position, _position) > 1500)
                    projectile.isVisible = false;
            }
            for (int i = 0; i < projectiles.Count; i++)
            {
                if(!projectiles[i].isVisible)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shooting()
        {
            Projectiles newProjectile = new Projectiles(Content.Load<Texture2D>("projectile_red"));
            newProjectile.velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 5f + _velocity;
            newProjectile.position = _position + newProjectile.velocity * 5;
            newProjectile.isVisible = true;

            if (projectiles.Count() < 15)
                projectiles.Add(newProjectile);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);//this will arange the objects back to front depending on ther
            spriteBatch.Draw(_texture, _position, null, Color.White, rotation, _origin, 1f, SpriteEffects.None, 0);
            enemy.Draw(spriteBatch);
            foreach (Projectiles projectile in projectiles)
                projectile.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
