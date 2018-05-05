using Game.Objects;

namespace Game.Units
{
    class Unit
    {
        public GameObject UnitIcon { get; protected set; }

        public int Damage { get; protected set; }
        public int RangeDamage { get; protected set; }
        public int ShootingRange { get; protected set; }
        public int HP { get; set; }

        public Unit()
        {
            this.UnitIcon = GameObject.Exit;
            this.HP = 5;
            this.Damage = 2;
            this.RangeDamage = 1;
            this.ShootingRange = 2;
        }
    }
}
