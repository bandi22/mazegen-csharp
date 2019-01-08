using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class Cell
    {
        //cell is visited
        public bool visited;

        //number of the cell
        public int index; 

        //grid coordinates
        public int x; 
        public int y;

        //adjacent cells:
        public Cell toNorth;
        public Cell toSouth;
        public Cell toWest;
        public Cell toEast;

        //intact cell walls
        public bool wallN = true;
        public bool wallS = true;
        public bool wallW = true;
        public bool wallE = true;

        public Cell(int index)
        {
            visited = false;
            this.index = index;
        } //constructor

        public bool CheckAdjacent(Cell cell)
        {
            if ((cell.toNorth == null || cell.toNorth.visited == true) && 
                (cell.toSouth == null || cell.toSouth.visited == true) && 
                (cell.toEast == null || cell.toEast.visited == true) && 
                (cell.toWest == null || cell.toWest.visited == true))
            {
                return false;
            }
            else
            {
                return true;
            }
        } //checkAdjacent - checks if cell has any unvisited adjacent cells

    }
}
