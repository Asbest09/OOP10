using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP10
{
    class Program
    {
        static void Main(string[] args)
        {
            Troop firstCountry = new Troop(100000, 133, 52, 350);
            Troop secondCountry = new Troop(50000, 200, 63, 700);

            ShowInfo(firstCountry, secondCountry);

            Console.WriteLine("Для продолжения нажмите ENTER");
            Console.ReadKey();

            SimulateFight(firstCountry, secondCountry, firstCountry.Count, secondCountry.Count);

            Console.ReadKey();
        }

        static void ShowInfo(Troop firstCountry, Troop secondCountry)
        {
            Console.WriteLine($"Взвод первой страны:\nКоличество солдат - {firstCountry.Count}\nУрон - {firstCountry.Damage}\nЗдоровье - {firstCountry.Health}\nБроня - {firstCountry.Armor}\n");
            Console.WriteLine($"Взвод второй страны:\nКоличество солдат - {secondCountry.Count}\nУрон - {secondCountry.Damage}\nЗдоровье - {secondCountry.Health}\nБроня - {secondCountry.Armor}");
        }

        static void SimulateFight(Troop firstCountry, Troop secondCountry, int firstCountryCount, int secondCountryCount)
        {
            while (firstCountryCount > 0 && secondCountryCount > 0)
            {
                int tempFirstCountryCount = firstCountryCount;

                firstCountryCount -= secondCountry.Damage * secondCountryCount * (100 - firstCountry.Armor) / 100 / firstCountry.Health;
                secondCountryCount -= firstCountry.Damage * tempFirstCountryCount * (100 - secondCountry.Armor) / 100 / secondCountry.Health;
            }

            if(firstCountryCount <= 0 && secondCountryCount <= 0)
            {
                Console.WriteLine("\nНичья");

                firstCountryCount = 0;
                secondCountryCount = 0;
            }
            else if(firstCountryCount <= 0)
            {
                Console.WriteLine("\nВторой взвод выиграл");

                firstCountryCount = 0;
            }
            else
            {
                Console.WriteLine("\nПервый взвод выиграл");
                secondCountryCount = 0;
            }

            Console.WriteLine($"\n\nИнформация после боя:\nЧисленность первого взвода - {firstCountryCount}\nЧисленность второго взвода - {secondCountryCount}");
        }
    }

    class Troop
    {
        public int Health { get; private set; }
        public int Count { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }

        public Troop(int count, int damage, int armor, int health)
        {
            Count = count;
            Damage = damage;
            Armor = armor;
            Health = health;
        }
    }
}
