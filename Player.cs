using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabyFights
{
    public class Player
    {
        int damage;
        public Player(int damage)
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
        public void UsePlayer()
        {
            if (damage > 0)
            {
                damage -= 1;
            }
        }
    }
}
