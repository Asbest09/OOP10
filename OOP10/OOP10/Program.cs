using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OOP10
{
    class Program
    {
        static void Main(string[] args)
        {
            Troop firstCountry = new Troop(new List<Unit> { new MachineGunner(), new MachineGunner(), new MachineGunner(), new MachineGunner(), new MachineGunner(), new MachineGunner(), new MachineGunner(), new MachineGunner(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new MachineGunner(), new MachineGunner() });
            Troop secondCountry = new Troop(new List<Unit> { new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Shooter(), new Sniper(), new Sniper(), new Sniper(), new Sniper(), new Sniper(), new Sniper(), new Sniper(), new Sniper(), new Sniper(), new Sniper()});

            ShowInfo(firstCountry, secondCountry);

            Console.WriteLine("Для продолжения нажмите ENTER");
            Console.ReadKey();

            SimulateFight(firstCountry, secondCountry);

            Console.ReadKey();
        }

        static void ShowInfo(Troop firstCountry, Troop secondCountry)
        {
            Console.WriteLine($"Взвод первой страны:\nКоличество солдат - {firstCountry.AllMilitary.Count}\n");
            Console.WriteLine($"Взвод второй страны:\nКоличество солдат - {secondCountry.AllMilitary.Count}");
        }

        static void SimulateFight(Troop firstCountry, Troop secondCountry)
        {
            Random rnd = new Random();

            while (firstCountry.AllMilitary.Count > 0 && secondCountry.AllMilitary.Count > 0)
            {
                int indexFirstDefender = rnd.Next(firstCountry.AllMilitary.Count);
                int indexSecondDefender = rnd.Next(firstCountry.AllMilitary.Count);
                var firstAttacker = firstCountry.AllMilitary[rnd.Next(firstCountry.AllMilitary.Count)];
                var secondAttacker = secondCountry.AllMilitary[rnd.Next(secondCountry.AllMilitary.Count)];
                var firstDefender = firstCountry.AllMilitary[indexFirstDefender];
                var secondDefender = secondCountry.AllMilitary[indexSecondDefender];

                firstDefender.TakeDamage(firstDefender.Armor * secondAttacker.Damage / 100);
                secondDefender.TakeDamage(secondDefender.Armor * firstAttacker.Damage / 100);

                if (firstDefender.Health <= 0)
                    firstCountry.AllMilitary.RemoveAt(indexFirstDefender);
                if (secondDefender.Health <= 0)
                    secondCountry.AllMilitary.RemoveAt(indexSecondDefender);

                if(firstCountry.AllMilitary.Count <= 10)
                    ActiveAbility(firstCountry.AllMilitary);
                if (secondCountry.AllMilitary.Count <= 10)
                    ActiveAbility(secondCountry.AllMilitary);
            }

            if (firstCountry.AllMilitary.Count <= 0 && secondCountry.AllMilitary.Count <= 0)
                Console.WriteLine("\nНичья");
            else if (firstCountry.AllMilitary.Count <= 0)
                Console.WriteLine("\nВторой взвод выиграл");
            else
                Console.WriteLine("\nПервый взвод выиграл");

            Console.WriteLine($"\n\nИнформация после боя:\nЧисленность первого взвода - {firstCountry.AllMilitary.Count}\nЧисленность второго взвода - {secondCountry.AllMilitary.Count}");
        }

        private static void ActiveAbility(List<Unit> AllMilitary)
        {
            foreach (var unit in AllMilitary)
                unit.Ability();
        }
    }

    class Troop
    {
        public List<Unit> AllMilitary;

        public Troop(List<Unit> allMilitary)
        {
            AllMilitary = allMilitary;
        }
    }

    abstract class Unit
    {
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public abstract void Ability();
    }

    class MachineGunner : Unit
    {
        public MachineGunner()
        {
            Health = 200;
            Damage = 100;
            Armor = 50;
        }

        public override void Ability()
        {
            Health += 100;
        }
    }

    class Shooter : Unit
    {
        public Shooter()
        {
            Health = 100;
            Damage = 50;
            Armor = 20;
        }

        public override void Ability()
        {
            Armor += 15;
        }
    }

    class Sniper : Unit
    {
        public Sniper()
        {
            Health = 100;
            Damage = 150;
            Armor = 10;
        }

        public override void Ability()
        {
            Damage += 50;
        }
    }
}
