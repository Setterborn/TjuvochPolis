using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThugs
{
    class Stuff
    {
        internal static Random rnd = new();
        public string Name { get; set; }
        public int Value { get; set; }
        public int Strenght { get; set; }

        public static List<Stuff> GetInventory()
        {
            List<Stuff> Items = new();
            Items.Add(new Backpack());
            Items.Add(new Phone());
            Items.Add(new Wallet());
            Items.Add(new Watch());
            return Items;
        }
        public static List<Stuff> GetWeapons()
        {
            List<Stuff> weapons = new();
            for (int i = 0; i < rnd.Next(1,4); i++)
            {
                switch (rnd.Next(1,5))
                {
                    case 1:
                        weapons.Add(new Knife());
                        break;
                    case 2:
                        weapons.Add(new Peperspray());
                        break;
                    case 3:
                        weapons.Add(new Baton());
                        break;
                    case 4:
                       weapons.Add(new Gun());
                        break;
                }
            }
            return weapons;
        }
    }
    class Backpack : Stuff
    {
        public Backpack()
        {
            Name = "Backpack";
            Value = rnd.Next(80, 250);
        }
    }
    class Phone : Stuff
    {
        public Phone()
        {
            Name = "Phone";
            Value = rnd.Next(300, 1000);
        }
    }
    class Wallet : Stuff
    {
        public Wallet()
        {
            Name = "Wallet";
            Value = rnd.Next(25, 2500);
        }
    }
    class Watch : Stuff
    {
        public Watch()
        {
            Name = "Watch";
            Value = rnd.Next(100, 7500);
        }
    }
    class Knife : Stuff
    {
        public Knife()
        {
            Name = "Knife";
            Strenght = 3;
        }
    }
    class Peperspray : Stuff
    {
        public Peperspray()
        {
            Name = "Peperspray";
            Strenght = 1;
        }
    }
    class Baton : Stuff
    {
        public Baton()
        {
            Name = "Baton";
            Strenght = 2;
        }
    }
    class Gun : Stuff
    {
        public Gun()
        {
            Name = "Gun";
            Strenght = 4;
        }
    }
    class Nothing : Stuff
    {
        public Nothing()
        {
            Name = "Nothing";
            Value = 0;
            Strenght = 0;
        }
    }
}
