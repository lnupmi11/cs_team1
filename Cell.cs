using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyFights
{
    public class Cell
    {
        private bool n_wall;
        private bool s_wall;
        private bool e_wall;
        private bool w_wall;
        private int cellSize = 5;
        private bool visited;
        private bool exit = false;
        private Player player = null;

        public Cell()
        {
            this.n_wall = true;
            this.s_wall = true;
            this.e_wall = true;
            this.w_wall = true;
            this.visited = false;
        }
        #region Get/Set

        public bool N_wall
        {
            get
            {
                return n_wall;
            }

            set
            {
                n_wall = value;
            }
        }

        public bool S_wall
        {
            get
            {
                return s_wall;
            }

            set
            {
                s_wall = value;
            }
        }

        public bool E_wall
        {
            get
            {
                return e_wall;
            }

            set
            {
                e_wall = value;
            }
        }

        public bool W_wall
        {
            get
            {
                return w_wall;
            }

            set
            {
                w_wall = value;
            }
        }

        public int CellSize
        {
            get
            {
                return cellSize;
            }

            set
            {
                cellSize = value;
            }
        }

        public bool Visited
        {
            get
            {
                return visited;
            }

            set
            {
                visited = value;
            }
        }

        public bool Exit
        {
            get
            {
                return exit;
            }

            set
            {
                exit = value;
            }
        }

        public Player Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
        #endregion
    }
}
