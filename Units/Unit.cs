using Game.Objects;

namespace Game.Units
{
    class Unit
    {
        public GameObject UnitIcon { get; protected set; }

        public int Damage { get; protected set; }

        public int HealthPoints { get; set; }

        public int MaxHealthPoints { get; set; }

        public int Energy { get; set; }

        public Unit()
        {
            this.UnitIcon = GameObject.Enemy;
            this.HealthPoints = 5;
            this.Damage = 2;
            this.Energy = 2;
        }
    }
}
