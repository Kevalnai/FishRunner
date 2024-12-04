using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishRunner
{
    public class Controller
    {
        public int score = 0;

        // Check collision between harpoon and fish
        public bool CheckCollision(Harpoon harpoon, Fish fish)
        {
            int fishRadius = Fish.radius;
            int harpoonRadius = 5;
            int distance = (int)Vector2.Distance(harpoon.position, fish.position);

            if (distance < fishRadius + harpoonRadius)
            {
                return true; // Collision detected
            }
            return false;
        }

        // End of game logic
        public string GetGameOverMessage(bool gameWon)
        {
            if (gameWon)
                return "You Won! Great Job!";
            else
                return "Game Over! Try Again!";
        }
    }
}
