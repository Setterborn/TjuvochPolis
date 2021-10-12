using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThugs.Characters
{
    public class Thug : Character
    { 
        public Thug(int playgroundSizeX, int playgroundSizeY)
        {
            Random rnd = new();
            PositionX = rnd.Next(0, playgroundSizeX - 5);
            PositionY = rnd.Next(0, playgroundSizeY - 5);
            DirectionX = rnd.Next(-1, 2);
            DirectionY = rnd.Next(-1, 2);
            Inventory = new();
            Weapons = Stuff.GetWeapons();
            Strenght = GetStrenghtFromWeapons();
            CharacterId = 1;
        }
        public static void AddThug()
        {
            Random rnd = new();
            for (int i = 0; i <= rnd.Next(5, 20); i++)
            {
                list.Add(new Thug(Playground.playgroundSizeX-10, Playground.playgroundSizeY-5));
            }
        }
        public override int GetStrenghtFromWeapons()
        {
            int vw = 0;
            foreach (Stuff weapon in Weapons)
            {
                vw += weapon.Strenght;
            }
            return vw;
        }

        public override string ToString()
        {
            return "Thg ";
        }
        public void PrintInventory()
        {
            foreach (Stuff item in Inventory)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
