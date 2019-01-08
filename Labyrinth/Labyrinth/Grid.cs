using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class Grid
    {
        public Cell[] cells; //declare the grid as an array of cells

        // :: Generating the grid in three steps :: //

        public void Generate(int n)
        {
            cells = new Cell[n * n];

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell(i);
            } //1. initializing cells with indices

            int count = 0;

            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    cells[count].x = j;
                    cells[count].y = k;
                    count++;
                }
            } //2. assigning coordinates to cells

            //3. setting up adjacent cells
            for (int i = 0; i < cells.Length; i++)
            {

                //upper left corner (x0, y0)
                if (cells[i].x == 0 && cells[i].y == 0)
                {
                    cells[i].toNorth = null;
                    cells[i].toWest = null;
                    cells[i].toSouth = cells[i + n];
                    cells[i].toEast = cells[i + 1];
                }

                //upper right corner (x0, yn)
                else if (cells[i].x == 0 && cells[i].y == n - 1)
                {
                    cells[i].toNorth = null;
                    cells[i].toWest = cells[i - 1];
                    cells[i].toEast = null;
                    cells[i].toSouth = cells[n + i];
                }

                //lower left corner
                else if (cells[i].x == n - 1 && cells[i].y == 0)
                {
                    cells[i].toNorth = cells[i - n];
                    cells[i].toEast = cells[i + 1];
                    cells[i].toWest = null;
                    cells[i].toSouth = null;
                }

                //lower right corner
                else if (cells[i].x == (n - 1) && cells[i].y == (n - 1))
                {
                    cells[i].toNorth = cells[i - n];
                    cells[i].toWest = cells[i - 1];
                    cells[i].toEast = null;
                    cells[i].toSouth = null;
                }

                //upper strip
                else if (cells[i].x == 0 && (cells[i].y > 0 && cells[i].y < n - 1))
                {
                    cells[i].toNorth = null;
                    cells[i].toSouth = cells[i + n];
                    cells[i].toWest = cells[i - 1];
                    cells[i].toEast = cells[i + 1];
                }

                //lower strip
                else if (cells[i].x == n - 1 && (cells[i].y > 0 && cells[i].y < n - 1))
                {
                    cells[i].toNorth = cells[i - n];
                    cells[i].toSouth = null;
                    cells[i].toWest = cells[i - 1];
                    cells[i].toEast = cells[i + 1];
                }

                //left strip
                else if (cells[i].y == 0 && (cells[i].x > 0 && cells[i].x < n - 1))
                {
                    cells[i].toNorth = cells[i - n];
                    cells[i].toSouth = cells[i + n];
                    cells[i].toEast = cells[i + 1];
                    cells[i].toWest = null;
                }

                //right strip
                else if (cells[i].y == n - 1 && (cells[i].x > 0 && cells[i].x < n - 1))
                {
                    cells[i].toNorth = cells[i - n];
                    cells[i].toSouth = cells[i + n];
                    cells[i].toWest = cells[i - 1];
                    cells[i].toEast = null;
                }

                //middle
                else
                {
                    cells[i].toNorth = cells[i - n];
                    cells[i].toSouth = cells[i + n];
                    cells[i].toWest = cells[i - 1];
                    cells[i].toEast = cells[i + 1];
                }

            } //for - adjacent cells

        } //generate (grid) method


        // :: Generating the maze using recursive backtracking ::

        public void CreateMaze(Grid grid)
        {

            //random object with random seed + direction integer
            System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
            int dir;

            //initializing a Stack of Cell type and pushing the starting cell (index 0) to the Stack
            Stack<Cell> stack = new Stack<Cell>();
            Cell currentCell = grid.cells[0];
            stack.Push(currentCell);

            //iterate, until the stack is empty:

            while (stack.Count != 0)
            {

                dir = rnd.Next(0, 4); //random direction 0N,1E,2S,3W

                //calling CheckAdjacent method to determine if current cell has any unvisited adjacent cells
                if (currentCell.CheckAdjacent(currentCell) == true) 
                {

                    switch (dir)
                    {
                        case 0: //NORTH
                            if (currentCell.toNorth != null && currentCell.toNorth.visited == false) //if adjacent cell to the north is not visited
                            {
                                currentCell.wallN = false; //remove northern wall of current cell
                                currentCell.toNorth.wallS = false; //remove southern wall of adjacent cell to the north
                                currentCell.visited = true; //set current cell as visited
                                currentCell.toNorth.visited = true; //set adjacent to the north as visited
                                stack.Push(currentCell); //push visited cell to the top of stack
                                currentCell = currentCell.toNorth; //make cell the new current cell
                            } //if
                            break;

                        case 1: //EAST
                            if (currentCell.toEast != null && currentCell.toEast.visited == false)
                            {
                                currentCell.wallE = false;
                                currentCell.toEast.wallW = false;
                                currentCell.visited = true;
                                currentCell.toEast.visited = true;
                                stack.Push(currentCell);
                                currentCell = currentCell.toEast;
                            } //if
                            break;

                        case 2: //SOUTH
                            if (currentCell.toSouth != null && currentCell.toSouth.visited == false)
                            {
                                currentCell.wallS = false;
                                currentCell.toSouth.wallN = false;
                                currentCell.visited = true;
                                currentCell.toSouth.visited = true;
                                stack.Push(currentCell);
                                currentCell = currentCell.toSouth;
                            } //if      
                            break;

                        case 3: //WEST
                            if (currentCell.toWest != null && currentCell.toWest.visited == false)
                            {
                                currentCell.wallW = false;
                                currentCell.toWest.wallE = false;
                                currentCell.visited = true;
                                currentCell.toWest.visited = true;
                                stack.Push(currentCell);
                                currentCell = currentCell.toWest;
                            } //if
                            break;

                        default:
                            break;

                    } //switch

                } //if CheckAdjacent

                else
                {
                    currentCell = stack.Pop(); //remove current cell from stack if it has no adjacent cells that are not previously visited
                }

            } //while (stack is not empty)

        } //CreateMaze

    }
}
