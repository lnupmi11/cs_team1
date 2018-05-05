using Game.Objects;

namespace Game.Units
{
    class Hero: Unit
    {
        public Hero()
        {
            this.UnitIcon = GameObject.Hero;
            this.HP = 10;
            this.Damage = 3;
            this.RangeDamage = 2;
            this.ShootingRange = 2;
        }
    }
}
