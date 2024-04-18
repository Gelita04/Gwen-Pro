using System;

namespace GameLibrary.Objects
{

    public abstract class Card
    {
        public string Name;

        public Card(string name)
        {
            this.Name = name;
        }
    }

    public class Unit : Card // cartas de unidad, heroes, arqueros y magos
    {
        public int Attack { get; set; }
        public const int Max_Attack = 500;
        public const int Min_Attack = 0;
        public UnitMember Category { get; set; }

        public Unit(string name, int attack, UnitMember category)
            : base(name)
        {
            Attack =
                attack <= Max_Attack && attack >= Min_Attack
                    ? attack
                    : throw new ArgumentException("error en los parametros del constructor");
            Category = category;
        }
    }

    public class Leader : Card //cartas lider
    {
        public string Effect { get; set; }

        public Leader(string name, string effect)
            : base(name)
        { Effect = effect;
        }
    }

    public class Field : Card //cartas de clima
    {
        public string Effect { get; set; }

        public Field(string name, string effect)
            : base(name)
        {
            Effect = effect;
        }
    }

    public class Counter_Field : Card //cartas despeje
    {
        public string Effect { get; set; }

        public Counter_Field(string name, string effect)
            : base(name)
        {
            Effect = effect;
        }
    }

    public class Buff : Card // cartas de aumento
    {
        public string Effect { get; set; }

        public Buff(string name, string effect)
            : base(name)
        {
            Effect = effect;
        }
    }

    public class Wildcard : Card // cartas señuelo
    {
        public Wildcard(string name)
            : base(name)
        {
        }
    }
}

