using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyFights
{
    public class Opponent
    {
        int damage;
        public Opponent(int damage)
        {
            this.damage = damage;
        }

        public int Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        //public void UseWeapon()
        //{
        //    if (damage > 0)
        //    {
        //        damage -= 1;
        //    }
        //}
    }
}
