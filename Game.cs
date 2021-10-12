using CopsAndThugs.Characters;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CopsAndThugs
{
    class Game
    {
        private static Random rnd = new Random();
        private static Character[,,] MyArray = new Character[Playground.playgroundSizeX, Playground.playgroundSizeY, 3];
        private static int ThugsLeft { get; set; }
        private static bool MyBool { get; set; }
        private static string OutputCharacter { get; set; }
        private static int stolenValue { get; set; }
        private static int repossesedValue { get; set; }


        public Game()
        {
            ThugsLeft = 0;
            MyBool = false;
            OutputCharacter = "";
        }

        public static void Run()
        {
            
            Cop.AddCop();
            Thug.AddThug();
            Citizen.AddCitizen();
            Fill3DArray(MyArray);
            PlacingCharacters();
            Console.SetWindowSize(200, 70);
            do
            {
                Print3DArray(MyArray);
                GetAction();
                ThugsLeft = 0;
                foreach (Character shuno in Character.list) { if (shuno is Thug) ThugsLeft++; }
                Console.SetCursorPosition(0, 60);
                Console.WriteLine($"{ThugsLeft}     Thugs left");
                
            } while (ThugsLeft > 0);

            Console.Clear();
            Console.WriteLine($"Stolen value: {stolenValue} \nRepossesed value: {repossesedValue}");
            Console.ReadKey();
        }

        private static void GetAction()
        {
            int thugs = 0;
            bool removeBool = false;
            foreach (Character shuno in Character.list)
            {
                Console.SetCursorPosition(0, 50);
                switch (shuno)
                {
                    case Cop:
                        if (MyArray[shuno.PositionX, shuno.PositionY, 1] is Thug)
                        {
                            Console.WriteLine($"Cop with {shuno.Strenght} strenght fights thug with {MyArray[shuno.PositionX, shuno.PositionY, 1].Strenght} strenght");
                            Console.Write("Cop uses ");
                            PrintWeapons(shuno.Weapons);
                            Console.Write("Thug uses ");
                            PrintWeapons(MyArray[shuno.PositionX, shuno.PositionY, 1].Weapons);
                            
                            if (shuno.Strenght > MyArray[shuno.PositionX, shuno.PositionY, 1].Strenght)
                            {
                                Console.WriteLine($"Cop won,caught thug and confiscated thugs possesions                               ");
                                PrintInventory(MyArray[shuno.PositionX, shuno.PositionY, 1].Weapons);
                                PrintInventory(MyArray[shuno.PositionX, shuno.PositionY, 1].Inventory);
                                
                                foreach (Stuff item in MyArray[shuno.PositionX, shuno.PositionY, 1].Inventory)
                                {
                                    repossesedValue += item.Value;
                                }
                                shuno.Weapons.AddRange(MyArray[shuno.PositionX, shuno.PositionY, 1].Weapons);
                                shuno.Inventory.AddRange(MyArray[shuno.PositionX, shuno.PositionY, 1].Inventory);
                                shuno.GetStrenghtFromWeapons();
                                MyArray[shuno.PositionX, shuno.PositionY, 1] = new Default();

                                removeBool = true;

                                
                            }
                            else
                            {
                                Console.WriteLine($"Thug won and got away                              "); 

                            }
                            Thread.Sleep(7000);
                            Console.Clear();
                        }
                        MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] = new Default();
                        shuno.Move(Playground.playgroundSizeX, Playground.playgroundSizeY);
                        MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] = shuno;
                        break;
                    case Thug:
                        if (MyArray[shuno.PositionX, shuno.PositionY, 2] is Citizen)
                        {
                            Stuff stolenItem;
                            if (MyArray[shuno.PositionX, shuno.PositionY, 2].Inventory.Count > 0)
                            {
                                stolenItem = MyArray[shuno.PositionX, shuno.PositionY, 2].Inventory[rnd.Next(0, MyArray[shuno.PositionX, shuno.PositionY, 2].Inventory.Count)];
                                shuno.Inventory.Add(stolenItem);
                                stolenValue += stolenItem.Value;
                            }
                            else
                            {
                                stolenItem = new Nothing();
                            }
                            Console.WriteLine($"Thug stole:{stolenItem.Name} value: {stolenItem.Value}                                               ");
                            MyArray[shuno.PositionX, shuno.PositionY, 2].Inventory.Clear();
                            Thread.Sleep(800);
                        }
                        if (MyArray[shuno.PositionX, shuno.PositionY, 0] is Cop)
                        {
                            Console.WriteLine($"Cop with {MyArray[shuno.PositionX, shuno.PositionY, 0].Strenght} strenght fights thug with {shuno.Strenght} strenght");
                            Console.Write("Cop uses ");
                            PrintWeapons(MyArray[shuno.PositionX, shuno.PositionY, 0].Weapons);
                            Console.Write("Thug uses ");
                            PrintWeapons(shuno.Weapons);
                            if (shuno.Strenght < MyArray[shuno.PositionX, shuno.PositionY, 0].Strenght)
                            {
                                Console.WriteLine($"Cop won,caught thug and confiscated thugs possesions                               ");
                                PrintInventory(shuno.Weapons);
                                PrintInventory(shuno.Inventory);
                                
                                foreach (Stuff item in shuno.Inventory)
                                {
                                    repossesedValue += item.Value;
                                }
                                MyArray[shuno.PositionX, shuno.PositionY, 0].Weapons.AddRange(shuno.Weapons);
                                MyArray[shuno.PositionX, shuno.PositionY, 0].Inventory.AddRange(shuno.Inventory);
                                MyArray[shuno.PositionX, shuno.PositionY, 0].GetStrenghtFromWeapons();
                                MyArray[shuno.PositionX, shuno.PositionY, 1] = new Default();
                                
                                removeBool = true;
                            }
                            else
                            {
                                Console.WriteLine("Thug won and got away                              ");
                            }
                            Thread.Sleep(7000);
                            Console.Clear();
                        }
                        MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] = new Default();
                        shuno.Move(Playground.playgroundSizeX, Playground.playgroundSizeY);
                        MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] = shuno;
                        break;
                    case Citizen:
                        if (MyArray[shuno.PositionX, shuno.PositionY, 1] is Thug)
                        {
                            Stuff stolenItem;
                            if (shuno.Inventory.Count > 0)
                            {
                                stolenItem = shuno.Inventory[rnd.Next(0, shuno.Inventory.Count)];
                                MyArray[shuno.PositionX, shuno.PositionY, 1].Inventory.Add(stolenItem);
                                stolenValue += stolenItem.Value;
                            }
                            else
                            {
                                stolenItem = new Nothing();
                            }
                            

                            Console.WriteLine($"Thug stole:{stolenItem.Name} value: {stolenItem.Value}                                                ");
                            MyArray[shuno.PositionX, shuno.PositionY, 1].Inventory.Clear();
                            Thread.Sleep(800);
                        }
                        MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] = new Default();
                        shuno.Move(Playground.playgroundSizeX, Playground.playgroundSizeY);
                        MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] = shuno;
                        break;
                    default:
                        break;
                }
            }
            foreach (Character c in MyArray)
            {
                if(c is Thug) { thugs++; }
            }
            Console.WriteLine(thugs);
            if (removeBool)
            {
                RemoveThug();
            }
        }

        private static void RemoveThug()
        {
            foreach (Character c in Character.list) { if (c is Thug) { Character.list.Remove(c); break; } }
        }

        public static void PlacingCharacters()
        {
            foreach (Character shuno in Character.list)
            {
                switch (shuno)
                {
                    case Cop:
                        do
                        {
                            MyBool = true;
                            if (MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] is Cop)
                            {
                                shuno.PositionX = rnd.Next(1, Playground.playgroundSizeX - 1);
                                shuno.PositionY = rnd.Next(1, Playground.playgroundSizeY - 1);
                            }
                            else
                            {
                                MyBool = false;
                            }
                        } while (MyBool);
                       
                        break;
                    case Thug:
                        do
                        {
                            MyBool = true;
                            if (MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] is Thug)
                            {
                                shuno.PositionX = rnd.Next(1, Playground.playgroundSizeX - 1);
                                shuno.PositionY = rnd.Next(1, Playground.playgroundSizeY - 1);
                            }
                            else
                            {
                                MyBool = false;
                            }
                        } while (MyBool);
                        
                        break;
                    case Citizen:
                        {
                            do
                            {
                                MyBool = true;
                                if (MyArray[shuno.PositionX, shuno.PositionY, shuno.CharacterId] is Citizen)
                                {
                                    shuno.PositionX = rnd.Next(1, Playground.playgroundSizeX - 1);
                                    shuno.PositionY = rnd.Next(1, Playground.playgroundSizeY - 1);
                                }
                                else
                                {
                                    MyBool = false;
                                }
                            } while (MyBool);
                            
                            break;
                        }
                }
            }
        }
        public static void Print3DArray(Character[,,] array)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        OutputCharacter = MyArray[j, i, k].ToString();
                        Console.Write(OutputCharacter);
                    }
                }
                Console.SetCursorPosition(0, i + 1);
            }
        }
        public static void Fill3DArray(Character[,,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        array[i, j, k] = new Default();
                    }
                }
            }
        }
        public static void PrintInventory(List<Stuff> inputInventory)
        {
            int value = 0;
            if (inputInventory.Count == 0) { Console.WriteLine("                                           "); }
            else 
            { 
                foreach (Stuff item in inputInventory)
                {
                    Console.Write(item.Name + ",");
                }
                foreach (Stuff item in inputInventory)
                {
                    value += item.Value;
                }
                Console.WriteLine($"\nValue: {value}                                                             ");
            }
        }
        public static void PrintWeapons(List<Stuff> inputWeaponlist)
        {
            if (inputWeaponlist.Count == 0) { Console.WriteLine("no weapons"); }
            foreach (Stuff item in inputWeaponlist)
            {
                Console.Write(item.Name + ",");
            }
            Console.WriteLine(" ");
        }

        private static void ClearAfterCursorYPosition(int y)
        {
            Console.SetCursorPosition(0, y);
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine();
            }
        }
    }
}
