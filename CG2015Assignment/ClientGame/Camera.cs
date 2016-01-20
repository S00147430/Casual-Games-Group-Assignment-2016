using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientGame
{
    class Camera
    {
        public Matrix Transform;
        Viewport viewport;
        public Vector2 center;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(GameTime gameTime, Game1 player)
        {
            center = new Vector2(player._position.X + (player._rectangle.Width / 2) - 854,  // Centers camera on player
              player._position.Y + (player._rectangle.Height / 2) - 470);
            //center = new Vector2(0,0);
            Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));  // Zooming
        }
    }
}
