using System.Net.Http.Headers;
using System.Xml;

internal class Program
{
    static string[,] matrix = new string[4, 4];
    static int x = 0;
    static int y = 0;
    static int countMove = 0;
    private static void Main(string[] args)
    {

        Random random = new Random();
        x = random.Next(0,4);
        y = random.Next(0,4);

        int[] rand = new int[15];
        int index = 0;
        int randValue = -1;

        

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == x && j == y)
                {
                    matrix[i, j] = "";
                    continue;
                }
                    
                randValue = random.Next(1, 16);
                
                while ( IsTherArray(rand, randValue))
                {
                    randValue = random.Next(1, 16);
                }
               
                matrix[i, j] = Convert.ToString(randValue);
                rand[index] = randValue;
                index++;
            }
        }
        PrintTable(matrix);

        GameRun(matrix, x, y);

    }

    static bool IsTherArray(int[] array, int value) 
    { 
        foreach (int i in array)
        {
            if (i == value)
                return true;
        }
        return false;
    }

    // Show Table
    static void PrintTable(string[,] matrix)
    {
        Console.WriteLine("=============== 15 Game ===============\n");
        Console.WriteLine($"|====| |====| |====| |====|\tMove: {countMove}");
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int num = 0;
                bool isParese = int.TryParse(matrix[i, j], out num);
                if (isParese)
                {
                    if (num <= 9)
                        Console.Write("| " + matrix[i, j] + "  | ");
                    else
                        Console.Write("| " + matrix[i, j] + " | ");
                } else
                {
                    Console.Write("| " + matrix[i, j] + "   | ");
                }
                
            }
            Console.WriteLine("\n|====| |====| |====| |====|");
        }
    }

    // Game Run
    static void GameRun(string[,] matrix, int x, int y)
    {
        Console.WriteLine();
        Console.WriteLine($"w : ↑\ts : ↓");
        Console.WriteLine($"a : <\td : >");

        string pushElem = "";
        var info = Console.ReadKey();
        Console.WriteLine();
        switch (info.KeyChar)
        {
            case 'w':
                {
                    if (x + 1 < matrix.GetLength(1))
                    {
                        pushElem = matrix[x + 1, y];
                        countMove++;
                    }
                } break;
            case 'a':
                {
                    if (y + 1 < matrix.GetLength(0))
                    {
                        pushElem = matrix[x, y + 1];
                        countMove++;
                    }
                }
                break;
            case 's':
                {
                    if (x - 1 >= 0)
                    {
                        pushElem = matrix[x - 1, y];
                        countMove++;
                    }
                }
                break;
            case 'd':
                {
                    if (y - 1 >= 0)
                    {
                        pushElem = matrix[x, y - 1];
                        countMove++;
                    }
                }
                break;
        }

        int indexI = 0, indexJ = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (matrix[i, j] == pushElem)
                {
                    indexI = i;
                    indexJ = j;
                    break;
                }
            }
        }
        Console.WriteLine($"IndexI = {indexI}\nIndexJ = {indexJ}");

        if ((x == indexI && Math.Abs(y - indexJ) == 1) || (y == indexJ && Math.Abs(x - indexI) == 1))
        {
            string temp = matrix[indexI, indexJ];
            matrix[indexI, indexJ] = matrix[x, y];
            matrix[x, y] = temp;
            x = indexI;
            y = indexJ;
        }

        Console.Clear();
        
        if (Check(matrix))
        {
            Console.WriteLine("Siz g'alab qozondingiz!");
            PrintTable(matrix);
            return;
        }
        PrintTable(matrix);
        GameRun(matrix, x, y);
        return;
    }

    // Check Table
    static bool Check(string[,] matrix)
    {
        string str = "";

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                str += matrix[i, j];
            }
        }
        return str == "123456789101112131415";
    }
}