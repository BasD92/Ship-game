using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shipGame
{
    class Dock : GameObject
    {
        //Constructor
        public Dock(Vector2 pos, Texture2D _texture) : base (pos, _texture) //Send parameters to base
        {
            
        }

        public void UnloadChests()
        {
            Console.WriteLine("Lading wordt gelost!");
        }
    }
}
