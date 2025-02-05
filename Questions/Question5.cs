using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MMGuideTestProject.Questions {
    public class Question5 : IQuestion {
        #region Properties
        private readonly string filePath = @"InputFiles/Question5.txt";
        //private readonly string filePath = @"InputFiles/Question5 - Sample.txt";

        private static readonly int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private static readonly int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
        #endregion

        #region Methods
        public void Execute() {
            try {
                int[,] grid = ReadGridFromFile(filePath);

                if (grid == null) {
                    Console.WriteLine("Error reading grid from file");
                    return;
                }

                Console.WriteLine($"Question 5 Result: { FindLargestRegion(grid) }");
            } catch (Exception ex) {
                Console.WriteLine($"Error in Question 5: {ex.Message}");
            }
        }

        public static int[,] ReadGridFromFile(string filePath) {
            string[] lines = File.ReadAllLines(filePath);
            int rows       = lines.Length;
            int cols       = lines[0].Split(' ').Length;

            int[,] grid = new int[rows, cols];

            for (int i = 0; i < rows; i++) {
                string[] values = lines[i].Split(' ');
                for (int j = 0; j < cols; j++) {
                    grid[i, j] = int.Parse(values[j]);
                }
            }

            return grid;
        }

        public static int FindLargestRegion(int[,] grid) {
            int maxRegion   = 0;
            int rows        = grid.GetLength(0);
            int cols        = grid.GetLength(1);
            bool[,] visited = new bool[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (grid[i, j] == 1 && !visited[i, j]) {
                        maxRegion = Math.Max(maxRegion, BFS(grid, visited, i, j));
                    }
                }
            }

            return maxRegion;
        }

        //Breadth-First Search
        private static int BFS(int[,] grid, bool[,] visited, int startX, int startY) {
            int count = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            Queue<(int, int)> queue = new Queue<(int, int)>();

            queue.Enqueue((startX, startY));
            visited[startX, startY] = true;

            while (queue.Count > 0) {
                var (x, y) = queue.Dequeue();
                count++;

                for (int i = 0; i < 8; i++) { // Check all 8 directions
                    int newX = x + dx[i];
                    int newY = y + dy[i];

                    if (IsValid(grid, visited, newX, newY)) {
                        queue.Enqueue((newX, newY));
                        visited[newX, newY] = true;
                    }
                }
            }

            return count;
        }

        private static bool IsValid(int[,] grid, bool[,] visited, int x, int y) {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            return x >= 0 && x < rows && y >= 0 && y < cols && grid[x, y] == 1 && !visited[x, y];
        }
        #endregion
    }
}

#region Question Description
/*
The challenge appears to be to figure out and count the cells in this extensive connected region, 
considering the possibility of multiple regions in this map. 
Cells are considered connected if they are next to each other, 
either horizontally, vertically, or diagonally.
1 1 0 0
0 1 1 0
0 0 1 0
1 0 0 0

The diagram below depicts two regions of the map. 
Connected regions are filled with X or Y. 
Zeros are replaced with dots for clarity.
X X . .
. X X .
. . X .
Y . . .

Final result: The larger region has 5 cells, marked X.
Find and return the size of the largest connected region within the map.
*/
#endregion