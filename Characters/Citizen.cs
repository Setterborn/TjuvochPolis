using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThugs.Characters
{
    public class Citizen : Character
    {
        public Citizen(int playgroundSizeX, int playgroundSizeY)
        {
            Random rnd = new();
            PositionX = rnd.Next(0, playgroundSizeX - 5);
            PositionY = rnd.Next(0, playgroundSizeY - 5);
            DirectionX = rnd.Next(-1, 2);
            DirectionY = rnd.Next(-1, 2);
            Strenght = rnd.Next(1, 4);
            CharacterId = 2;
            Inventory = Stuff.GetInventory();
        }
        public static void AddCitizen()
        {
            Random rnd = new();
            for (int i = 0; i <= rnd.Next(5, 20); i++)
            {
                list.Add(new Citizen(Playground.playgroundSizeX-10, Playground.playgroundSizeY-5));
            }
        }

        public override string ToString()
        {
            return "Cit ";
        }
        
    }
}
