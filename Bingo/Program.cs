// Amount of players
int playerAmount = 1;

// Represents how many cards there are
int cardsSize = playerAmount * 2;

// Represents the index of where each card begins and ends
int count;

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
int[] playersCards = new int[playerAmount];


/* Array that represents players points
 * Array size = amount of players
 * Each index of array = represens how many points each player has
 */
int[] playersPoints = new int[playerAmount];


/*
 * Each player can have multiple cards, the control is made by var count
 */
int[][,] cards = new int[cardsSize][,];


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

    for(int line = 0; line < 5; line++)
    {
        for(int column = 0; column < 5; column++)
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
 * Prints que matrix in the Console
 */
void PrintMatrix(int[,] matrix, int lines, int columns, string title)
{
    Console.WriteLine(title);
    for (int l = 0; l < lines; l++)
    {
        for (int c = 0; c < columns; c++)
        {
            Console.Write($"{matrix[l, c]} ");
        }
        Console.WriteLine();
    }
}

cards[0] = SortMatrix();
PrintMatrix(cards[0], 5, 5, "Matrix");