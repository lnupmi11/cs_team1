namespace Game.Units
{
    interface IMage
    {
        uint ManaPoints { get; set; }

        bool MagickAttack(ref Unit _attackedUnit, ref uint _unitIPosition, ref uint _unitJPosition,
            ref uint _attackedUnitIPosition, ref uint _attackedUnitJPosition);
    }
}
