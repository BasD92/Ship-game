using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shipGame
{
    class Chest : GameObject
    {
        //Constructor 
        public Chest(Vector2 _position, Texture2D _texture) : base (_position, _texture) //Send parameters to base
        {
            
        }

        public void CatchChests()
        {
            Console.WriteLine("Het schatkistje is opgepakt!");
        }
    }
}
