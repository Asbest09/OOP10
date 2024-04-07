using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OOP10
{
    class Program
    {
        static void Main(string[] args)
        {
            Troop firstCountry = new Troop();
            Troop secondCountry = new Troop();
            firstCountry.GettingTroop(0, 10, 10);
            secondCountry.GettingTroop(10, 0, 10);

            Console.WriteLine($"Взвод первой страны:\nКоличество солдат - {firstCountry.CountMilitary()}");
            Console.WriteLine($"Взвод первой страны:\nКоличество солдат - {secondCountry.CountMilitary()}");

            Console.WriteLine("Для продолжения нажмите ENTER");
            Console.ReadKey();
 
            SimulateFight(firstCountry, secondCountry);

            Console.ReadKey();
        }

        static void SimulateFight(Troop firstCountry, Troop secondCountry)
        {
            Random rnd = new Random();

            while (firstCountry.CheakAlievs() && secondCountry.CheakAlievs())
            {
                var firstAttacker = firstCountry.GetAttacker();
                var secondAttacker = secondCountry.GetAttacker();
                var firstDefender = firstCountry.GetDefender();
                var secondDefender = secondCountry.GetDefender();

                firstDefender.TakeDamage(firstDefender.Armor * secondAttacker.Damage / 100);
                secondDefender.TakeDamage(secondDefender.Armor * firstAttacker.Damage / 100);

                firstCountry.CheakMilitaryDeath(firstDefender);
                secondCountry.CheakMilitaryDeath(secondDefender);

                firstCountry.CheakActiveAbility();
                secondCountry.CheakActiveAbility();
            }

            if (firstCountry.CheakAlievs() && secondCountry.CheakAlievs())
                Console.WriteLine("\nНичья");
            else if (firstCountry.CheakAlievs())
                Console.WriteLine("\nВторой взвод выиграл");
            else
                Console.WriteLine("\nПервый взвод выиграл");

            Console.WriteLine($"\n\nИнформация после боя:\nЧисленность первого взвода - {firstCountry.CountMilitary()}\nЧисленность второго взвода - {secondCountry.CountMilitary()}");
        }
    }

    class Troop
    {
        private List<Unit> AllMilitary;
        private bool _activeAbiliry;
        private Random rnd;

        public Troop()
        {
            _activeAbiliry = false;
            rnd = new Random();
            AllMilitary = new List<Unit>();
        }

        public void GettingTroop(int countSniper, int countMachineGunner, int countShooter)
        {
            for (int i = 0; i < countSniper; i++)
                AllMilitary.Add(new Sniper(100, 150, 10));

            for (int i = 0; i < countMachineGunner; i++)
                AllMilitary.Add(new MachineGunner(200, 100, 50));

            for (int i = 0; i < countShooter; i++)
                AllMilitary.Add(new Shooter(100, 50, 20));
        }

        public bool CheakAlievs()
        {
            if (AllMilitary.Count <= 0)
                return false;
            else
                return true;
        }

        public void CheakActiveAbility()
        {
            if (_activeAbiliry == false && AllMilitary.Count <= 10)
                ActiveAbility();
        }

        public void ActiveAbility()
        {
            foreach (var unit in AllMilitary)
                unit.Ability();

            _activeAbiliry = true;
        }

        public int CountMilitary()
        {
            return AllMilitary.Count;
        }

        public Unit GetAttacker()
        {
            return AllMilitary[rnd.Next(AllMilitary.Count)];
        }

        public Unit GetDefender()
        {
            return AllMilitary[rnd.Next(AllMilitary.Count)];
        }

        public void CheakMilitaryDeath(Unit military)
        {
            if (military.Health <= 0)
                AllMilitary.Remove(military);
        }
    }

    abstract class Unit
    {
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }

        public Unit(int health, int damage, int armor) 
        {
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public abstract void Ability();
    }

    class MachineGunner : Unit
    {
        public MachineGunner(int health, int damage, int armor) : base(health, damage, armor) { }

        public override void Ability()
        {
            Health += 100;
        }
    }

    class Shooter : Unit
    {
        public Shooter(int health, int damage, int armor) : base(health, damage, armor) { }

        public override void Ability()
        {
            Armor += 15;
        }
    }

    class Sniper : Unit
    {
        public Sniper(int health, int damage, int armor) : base(health, damage, armor) { }

        public override void Ability()
        {
            Damage += 50;
        }
    }
}
