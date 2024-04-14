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
            Troop firstCountry = new Troop();
            Troop secondCountry = new Troop();
            firstCountry.GetTroop(0, 10, 10, firstCountry);
            secondCountry.GetTroop(10, 0, 10, secondCountry);

            Console.WriteLine($"Взвод первой страны:\nКоличество солдат - {firstCountry.CountMilitary()}");
            Console.WriteLine($"Взвод первой страны:\nКоличество солдат - {secondCountry.CountMilitary()}");

            Console.WriteLine("Для продолжения нажмите ENTER");
            Console.ReadKey();

            SimulateFight(firstCountry, secondCountry);

            Console.ReadKey();
        }

        static void SimulateFight(Troop firstCountry, Troop secondCountry)
        {
            while (firstCountry.CheakAlievs() && secondCountry.CheakAlievs())
            {
                var firstAttacker = firstCountry.GetAttacker();
                var secondAttacker = secondCountry.GetAttacker();
                var firstDefender = firstCountry.GetDefender();
                var secondDefender = secondCountry.GetDefender();

                firstDefender.TakeDamage(secondAttacker.Damage);
                secondDefender.TakeDamage(firstAttacker.Damage);


                firstCountry.TryActiveAbility();
                secondCountry.TryActiveAbility();
            }

            if (firstCountry.CheakAlievs() == false && secondCountry.CheakAlievs() == false)
                Console.WriteLine("\nНичья");
            else if (firstCountry.CheakAlievs() == false)
                Console.WriteLine("\nВторой взвод выиграл");
            else
                Console.WriteLine("\nПервый взвод выиграл");

            Console.WriteLine($"\n\nИнформация после боя:\nЧисленность первого взвода - {firstCountry.CountMilitary()}\nЧисленность второго взвода - {secondCountry.CountMilitary()}");
        }
    }

    class Troop
    {
        private List<Unit> _allMilitary;
        private bool _activeAbility;
        private Random rnd;

        public Troop()
        {
            _activeAbility = false;
            rnd = new Random();
            _allMilitary = new List<Unit>();
        }

        public void GetTroop(int countSniper, int countMachineGunner, int countShooter, Troop country)
        {
            for (int i = 0; i < countSniper; i++)
                _allMilitary.Add(new Sniper(100, 150, 10, country));

            for (int i = 0; i < countMachineGunner; i++)
                _allMilitary.Add(new MachineGunner(200, 100, 50, country));

            for (int i = 0; i < countShooter; i++)
                _allMilitary.Add(new Shooter(100, 50, 20, country));
        }

        public bool CheakAlievs() =>
            _allMilitary.Count > 0;

        public void TryActiveAbility()
        {
            if (_activeAbility == false && _allMilitary.Count <= 10)
                ActiveAbility();
        }

        public void ActiveAbility()
        {
            foreach (var unit in _allMilitary)
                unit.Ability();

            _activeAbility = true;
        }

        public int CountMilitary() =>
            _allMilitary.Count;

        public Unit GetAttacker()
        {
            return _allMilitary[rnd.Next(_allMilitary.Count)];
        }

        public Unit GetDefender()
        {
            return _allMilitary[rnd.Next(_allMilitary.Count)];
        }

        public void MilitaryDeath(Unit military) =>
                _allMilitary.Remove(military);
    }

    abstract class Unit
    {
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }
        private Troop _country;

        public Unit(int health, int damage, int armor, Troop country)
        {
            Health = health;
            Damage = damage;
            Armor = armor;
            _country = country;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage * (100 - Armor) / 100;
            if (Health <= 0)
                _country.MilitaryDeath(this);
        }

        public abstract void Ability();
    }

    class MachineGunner : Unit
    {
        public MachineGunner(int health, int damage, int armor, Troop country) : base(health, damage, armor, country) { }

        public override void Ability()
        {
            Health += 100;
        }
    }

    class Shooter : Unit
    {
        public Shooter(int health, int damage, int armor, Troop country) : base(health, damage, armor, country) { }

        public override void Ability()
        {
            Armor += 15;
        }
    }

    class Sniper : Unit
    {
        public Sniper(int health, int damage, int armor, Troop country) : base(health, damage, armor, country) { }

        public override void Ability()
        {
            Damage += 50;
        }
    }
}
