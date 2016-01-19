using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    class Enemy
    {
        public Texture2D texture;
        public Vector2 position;
        public bool collides;
        public bool isVisible = true;
        

        public Enemy()
        {
            texture = null;
            position = new Vector2(600, 400);
            collides = false;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("enemy");
        }


        public Rectangle boundingRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
            set { }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            spriteBatch.Draw(texture, position, Color.White);
        }


        public void Update(GameTime gameTime)
        {
            if (position.X <= 0) 
                position.X = 0;

            if (position.X >= 1350 - texture.Width)
                position.X = 1350 - texture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= 800 - texture.Height)
                position.Y = 800 - texture.Height;
        }
    }
}