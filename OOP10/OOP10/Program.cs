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
            firstCountry.GetTroop(0, 10, 10); 
            secondCountry.GetTroop(10, 0, 10);

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

                firstDefender.TakeDamage(firstDefender.Armor * secondAttacker.Damage / 100);
                secondDefender.TakeDamage(secondDefender.Armor * firstAttacker.Damage / 100);

                firstCountry.CheakMilitaryDeath(firstDefender);
                secondCountry.CheakMilitaryDeath(secondDefender);

                firstCountry.TryActiveAbility();
                secondCountry.TryActiveAbility();
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
        private List<Unit> _allMilitary;
        private bool _activeAbiliry;
        private Random rnd;

        public Troop()
        {
            _activeAbiliry = false;
            rnd = new Random();
            _allMilitary = new List<Unit>();
        }

        public void GetTroop(int countSniper, int countMachineGunner, int countShooter) //можешь сделать его приватным и поместить в конструктор
        {
            for (int i = 0; i < countSniper; i++)
                _allMilitary.Add(new Sniper(100, 150, 10));

            for (int i = 0; i < countMachineGunner; i++)
                _allMilitary.Add(new MachineGunner(200, 100, 50));

            for (int i = 0; i < countShooter; i++)
                _allMilitary.Add(new Shooter(100, 50, 20));
        }

        public bool CheakAlievs() =>
            _allMilitary.Count > 0;
        public void TryActiveAbility()
        {
            if (_activeAbiliry == false && _allMilitary.Count <= 10)
                ActiveAbility();
        }

        public void ActiveAbility()
        {
            foreach (var unit in _allMilitary)
                unit.Ability();

            _activeAbiliry = true;
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

        public void CheakMilitaryDeath(Unit military)
        {
            if (military.Health <= 0)
                _allMilitary.Remove(military);
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
