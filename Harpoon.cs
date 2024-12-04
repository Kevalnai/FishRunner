using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishRunner
{
    public class Harpoon
    {
        public Vector2 position;
        public int speed = 15;
        public bool isActive = false;

        public Harpoon(Vector2 startPosition)
        {
            position = startPosition;
        }

        // Update Harpoon position and check if it's out of bounds
        public void Update()
        {
            if (isActive)
            {
                position.X += speed; // Move harpoon to the right
                if (position.X > 1200) // If out of bounds, deactivate
                {
                    isActive = false;
                }
            }
        }
    }
}
