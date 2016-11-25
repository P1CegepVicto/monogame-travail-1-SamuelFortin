using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class GameObject
    {
        public bool estVivant;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 vitesse;
        Rectangle RectCollision;

        public Rectangle GetRect()
        {
            RectCollision.X = (int)this.position.X;
            RectCollision.Y = (int)this.position.Y;
            RectCollision.Width = this.sprite.Width;
            RectCollision.Height = this.sprite.Height;

            return RectCollision;
        }
    }
}
