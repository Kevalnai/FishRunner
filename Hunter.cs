using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishRunner
{
    public class Hunter
    {
        public Vector2 position;
        public int speed = 10;
        public int radius = 25;
        public bool isShooting = false;
        public Harpoon currentHarpoon;

        public Hunter(Vector2 startPosition)
        {
            position = startPosition;
            currentHarpoon = new Harpoon(position);
        }

        // Update Hunter's position and shooting logic
        public void Update()
        {
            // Move left and right using arrow keys
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
                position.X -= speed;
            if (state.IsKeyDown(Keys.Right))
                position.X += speed;

            if (state.IsKeyDown(Keys.Up))
            {
                position = new Vector2(position.X, position.Y - speed);  // Move up
            }
            if (state.IsKeyDown(Keys.Down))
            {
                position = new Vector2(position.X, position.Y + speed);  // Move down
            }


            // Shoot harpoon if spacebar is pressed
            if (state.IsKeyDown(Keys.Space) && !currentHarpoon.isActive)
            {
                currentHarpoon = new Harpoon(position);
                currentHarpoon.isActive = true;
            }

            // Update harpoon position
            currentHarpoon.Update();
        }
    }
}
