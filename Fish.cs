using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishRunner
{
    public class Fish
    {
        public Vector2 position;
        public int speed;
        public static int radius = 25;

        public Fish(int speed, Vector2 position)
        {
            this.speed = speed;
            this.position = position;
        }

        // Update fish movement
        public void Update()
        {
            position.X -= speed; // Fish move left to right
        }
    }
}
