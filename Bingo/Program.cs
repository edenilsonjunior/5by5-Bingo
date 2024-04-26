


/*-----Functions-----*/
/*
 * Function that returns a new matrix with sorted numbers
 * between 1-99 that are not repeated
 */
int[,] SortMatrix()
{
    int[,] matrix = new int[5, 5];
    int number;

    Random random = new Random();

    for (int line = 0; line < 5; line++)
    {
        for (int column = 0; column < 5; column++)
        {
            /*
             * Executes do-while loop while the random number is already in matrix
             */
            do
            {
                number = random.Next(1, 100);

            } while (IsRepeated(matrix, number));

            matrix[line, column] = number;
        }
    }

    return matrix;
}

/*
 * Function that returns if the number is already in the matrix
 * Returns: 
 *  true if the number is already in the matrix
 *  false if the number is not in the matrix
 */
bool IsRepeated(int[,] matrix, int number)
{
    bool repeated = false;

    for (int line = 0; line < 5; line++)
    {
        for (int column = 0; column < 5; column++)
        {
            if (matrix[line, column] == number)
            {
                repeated = true;
            }
        }
    }
    return repeated;
}

/*
 * Sort and matrix for every card([][]) in the array
 */
void PopulateCards(int[][,] cards, int cardsSize)
{
    for (int i = 0; i < cardsSize; i++)
    {
        cards[i] = SortMatrix();
    }
}

/*
 * Prints que matrix in the Console
 */
void PrintMatrix(int[,] matrix, int lines, int columns, string title)
{
    Console.WriteLine(title);
    for (int l = 0; l < lines; l++)
    {
        for (int c = 0; c < columns; c++)
        {
            Console.Write($"{matrix[l, c]:00} ");
        }
        Console.WriteLine();
    }
}

bool IsBingoVertically(int[,] cards)
{
    bool bingo = false;

    for (int l = 0; l < 5; l++)
    {
        int count = 0;
        for (int c = 0; c < 5; c++)
        {
            if (cards[c, l] < 0)
                count++;
        }

        if (count == 5)
        {
            bingo = true;
            break;
        }
    }

    return bingo;
}




bool IsBingoHorizontally(int[,] cards)
{
    bool bingo = false;

    for (int l = 0; l < 5; l++)
    {
        int count = 0;
        for (int c = 0; c < 5; c++)
        {
            if (cards[l, c] < 0)
                count++;
        }
        if (count == 5)
        {
            bingo = true;
            break;
        }
    }

    return bingo;
}

bool IsTotallyBingo(int[,] cards)
{
    bool bingo = false;
    int count = 0;

    for (int l = 0; l < 5; l++)
    {
        for (int c = 0; c < 5; c++)
        {
            if (cards[c, l] < 0)
                count++;
        }
    }
    if (count == 25)
        bingo = true;

    return bingo;
}


// Amount of players
const int playerAmount = 3;

/*
 * Array that represents players names
 * Array size = amount of players
 * Each index of array = represents the name of player
 */
string[] playersNames = new string[playerAmount];


/*
 * Array that represents players cards
 * Array size = amount of players
 * Each index of array = represents how many cards each player has
 */
int[] playersCards = new int[playerAmount] { 1, 2, 3 };


/* Array that represents players points
 * Array size = amount of players
 * Each index of array = represens how many points each player has
 */
int[] playersPoints = new int[playerAmount];


// Represents how many cards there are
int cardsSize = 0;

for (int i = 0; i < playersCards.Length; i++)
{
    cardsSize += playersCards[i];
}


/*
 * Each player can have multiple cards, the control is made by var count
 */
int[][,] cards = new int[cardsSize][,];


/*
PopulateCards(cards, cardsSize);

for (int i = 0; i < cardsSize; i++)
{
    PrintMatrix(cards[0], 5, 5, $"Matrix {i + 1}");
    Console.WriteLine("\n");
}*/


int[,] bingo = { { -1, -2, -3, -4, -5 },
                 { -1, -2, -3, -4, -5 },
                 { -1, -2, -3, -4, -5 },
                 { -1, -2, -3, -4, -5 },
                 { -1, -2, -3, -4, -5 }
               };

Console.WriteLine($"bingo horizontal: {IsBingoHorizontally(bingo)}");
Console.WriteLine($"bingo vertical: {IsBingoVertically(bingo)}");
Console.WriteLine($"bingo total: {IsTotallyBingo(bingo)}");