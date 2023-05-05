using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_Support_Test2_Andrieiev
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isFirstBoardValid = ValidSolution(new int[,] {
                                        { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                                        { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
                                        { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                                        { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                                        { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                                        { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                                        { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                                        { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                                        { 3, 4, 5, 2, 8, 6, 1, 7, 9 }}); // True

            bool isSecondBoardValid = ValidSolution(new int[,] {
                                        { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                                        { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
                                        { 1, 0, 0, 3, 4, 2, 5, 6, 0 },
                                        { 8, 5, 9, 7, 6, 1, 0, 2, 0 },
                                        { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                                        { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                                        { 9, 0, 1, 5, 3, 7, 2, 1, 4 },
                                        { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                                        { 3, 0, 0, 4, 8, 1, 1, 7, 9 }}); // False

            Console.WriteLine($"Is first sudoku board valid - {isFirstBoardValid}");
            Console.WriteLine($"Is second sudoku board valid - {isSecondBoardValid}");

            Console.ReadKey();
        }

        public static bool ValidSolution(int[,] board)
        {
            bool isValidSolution = CheckRowsAndCols(board);
            if (isValidSolution == false)
                return false;

            isValidSolution = CheckSubBoxes(board);
            if (isValidSolution == false)
                return false;

            return true;
        }

        public static bool CheckRowsAndCols(int[,] board)
        {
            HashSet<int> rowSet = new HashSet<int>();
            HashSet<int> colSet = new HashSet<int>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // check row number
                    int rowNumber = board[i, j];
                    if (rowSet.Contains(rowNumber) || rowNumber == 0) return false;
                    rowSet.Add(rowNumber);

                    // check col number
                    int colNumber = board[j, i];
                    if (colSet.Contains(colNumber) || colNumber == 0) return false;
                    if (colNumber != 0) colSet.Add(colNumber);
                }
                rowSet.Clear();
                colSet.Clear();
            }

            return true;
        }

        public static bool CheckSubBoxes(int[,] board)
        {
            HashSet<int> subBoxSet = new HashSet<int>();
            int rowMin = 0;
            int rowMax = 2;
            int colMin = 0;
            int colMax = 2;
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    for (int i = rowMin; i <= rowMax; i++)
                    {
                        for (int j = colMin; j <= colMax; j++)
                        {
                            int subBoxNumber = board[i, j];
                            if (subBoxSet.Contains(subBoxNumber) || subBoxNumber == 0) return false;
                            subBoxSet.Add(subBoxNumber);
                        }
                    }
                    subBoxSet.Clear();
                    colMin += 3;
                    colMax += 3;
                }
                rowMin += 3;
                rowMax += 3;
                colMin = 0;
                colMax = 2;
            }
            return true;
        }
    }
}
