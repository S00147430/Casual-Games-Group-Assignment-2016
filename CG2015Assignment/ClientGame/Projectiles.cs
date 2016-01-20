using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientGame
{
    class Projectiles
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;        // Variables
        public Vector2 origin;
        public float rotation;
        public bool isVisible;



        public Projectiles()
        {
            texture = null;
            position = new Vector2(0,0);
            
        }

        public Texture2D Texture
        {
            get
            {
                return texture; 
            }
            set { }
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
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
