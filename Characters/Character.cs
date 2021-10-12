using System;
using System.Collections.Generic;
using CopsAndThugs.Characters;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThugs
{
    public class Character
    {
        internal static List<Character> list = new List<Character>();
        internal static List<Character> Jail { get; set; }
        internal List<Stuff> Inventory { get; set; }
        internal List<Stuff> Weapons { get; set; }
        public int CharacterId { get; set; }
        internal int PositionX { get; set; }
        internal int PositionY { get; set; }
        internal int DirectionX { get; set; }
        internal int DirectionY { get; set; }
        public int Strenght { get; set; }

        public void Move(int playgroundSizeX, int playgroundSizeY)
        {
            PositionX += DirectionX;
            PositionY += DirectionY;
            if (PositionX > playgroundSizeX - 3)
            {
                PositionX = 3;
            }
            if (PositionY > playgroundSizeY - 3)
            {
                PositionY = 3;
            }
            if (PositionX < 3)
            {
                PositionX = playgroundSizeX-3;
            }
            if (PositionY < 3)
            {
                PositionY = playgroundSizeY-3;
            }
        }
        public virtual int GetStrenghtFromWeapons()
        {
            int vw = 0;
            foreach (Stuff weapon in Weapons)
            {
                vw = +weapon.Strenght;
            }
            return vw;
        }
    }
    public class Default : Character
    {
        public override string ToString()
        {
            return " ";
        }
    }

}
