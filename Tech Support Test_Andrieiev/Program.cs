using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tech_Support_Test_Andrieiev
{
    internal class Program
    {
        public enum PopulationColor
        {
            Red,
            Green,
            Blue
        }

        static void Main(string[] args)
        {
            (int[] totalPopulation, int mainPopulation) = PopulationInput();

            int resultMeetings = MinMeetings(totalPopulation, mainPopulation);

            if (resultMeetings < 0)
                Console.WriteLine($"Solution with the input data is impossible");
            else if (resultMeetings == 0)
                Console.WriteLine($"Hedgehogs are already the right color, meetings are not required.");
            else
                Console.WriteLine($"The minimum meetings is - {resultMeetings}, to turn {(PopulationColor)mainPopulation} population");

            Console.ReadKey();
        }

        static public (int[], int) PopulationInput()
        {
            int totalRed, totalGreen, totalBlue;
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Enter RED population: ");
                    totalRed = Convert.ToInt32(Console.ReadLine());

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"\nEnter GREEN population: ");
                    totalGreen = Convert.ToInt32(Console.ReadLine());

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"\nEnter BLUE population: ");
                    totalBlue = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Gray;

                    int populationSum = totalRed + totalGreen + totalBlue;
                }
                catch (OverflowException exc)
                {
                    Console.WriteLine(exc.Message);
                    totalRed = totalGreen = totalBlue = -1;
                }

                if (totalRed < 0 || totalGreen < 0 || totalBlue < 0)
                    Console.WriteLine("Incorrect input");
                else
                    break;
            }


            int mainPopulation;
            while (true)
            {
                Console.Write($"\nEnter main population (0 - red, 1 - green, 2 - blue): ");
                mainPopulation = Convert.ToInt32(Console.ReadLine());
                if (mainPopulation == (int)PopulationColor.Red || mainPopulation == (int)PopulationColor.Green || mainPopulation == (int)PopulationColor.Blue)
                    break;
                else
                    Console.WriteLine("Incorrect input");
            }

            return (new int[3] { totalRed, totalGreen, totalBlue }, mainPopulation);
        }


        static public int MinMeetings(int[] totalPopulation, int mainPopulation)
        {
            //  Selection of non-main populations
            int firstNotMainPopulation = -1, secondNotMainPopulation = -1;
            for (int i = 0; i < 3; i++)
            {
                if (i == mainPopulation)
                    continue;
                else if (firstNotMainPopulation < 0)
                    firstNotMainPopulation = i;
                else if (secondNotMainPopulation < 0)
                    secondNotMainPopulation = i;
            }

            int populationSum = totalPopulation[0] + totalPopulation[1] + totalPopulation[2];

            if (populationSum == 0)     //  each population is 0
                return -1;
            else if (totalPopulation[mainPopulation] == populationSum)      //   all hedgehogs are already in the target population
                return 0;
            else if (totalPopulation[firstNotMainPopulation] == populationSum || totalPopulation[secondNotMainPopulation] == populationSum)//  all hedgehogs are in only one, non-target population
                return -1;
            else if (totalPopulation[firstNotMainPopulation] == totalPopulation[secondNotMainPopulation])   //  the number of hedgehogs in non-target populations is equal to each other
                return totalPopulation[firstNotMainPopulation];
            else if (Math.Abs((totalPopulation[firstNotMainPopulation] - totalPopulation[secondNotMainPopulation])) % 3 != 0)   //  if the difference between non-target groups is not divisible by 3, without a remainder - there is no solution
                return -1;
            else       //If the previous cases did not work out, then the solution is equal to the maximum value among two non-target populations
                return Math.Max(totalPopulation[firstNotMainPopulation], totalPopulation[secondNotMainPopulation]);
        }
    }
}