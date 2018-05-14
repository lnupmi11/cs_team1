using Game.Objects;

namespace Game.Units
{
    class Hero: Unit, IMage, IRanger
    {
        public int RangeDamage { get; protected set; }
        public int ShootingRange { get; protected set; }

        public uint ManaPoints { get; set; }
        public uint MaxManaPoints { get; set; }

        public Hero()
        {
            this.UnitIcon = GameObject.Hero;
            this.HealthPoints = 10;
            this.ManaPoints = 10;
            this.Energy = 2;

            this.Damage = 3;

            this.RangeDamage = 2;
            this.ShootingRange = 2;
        }

        public bool MagickAttack(ref Unit _attackedUnit, ref uint _unitIPosition, ref uint _unitJPosition,
            ref uint _attackedUnitIPosition, ref uint _attackedUnitJPosition)
        {
            //TODO
            throw new System.NotImplementedException();
        }
    }
}
