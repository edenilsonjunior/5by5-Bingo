// Amount of players in game
int playerAmount = ReadInt("Enter the amount of players:", 2);

// How many cards each player has
int cardsPerPlayer = ReadInt("Enter how many cards per player:", 1);

/*
 * Represents every card in game
 * Lines: Represents each player 
 * Columns: Represents each card game
 */
int[,][,] cards = new int[playerAmount, cardsPerPlayer][,];

/*
 * playersNames: array that contains each name from players
 * playersPoints: array that keeps players points
 */
string[] playersNames = new string[playerAmount];
int[] playersPoints = new int[playerAmount];

/*
 * Array for already sorted numbers and his index
 */
int[] sortedNumbers = new int[99];
int indexSortedNumbers = 0;

// Variables that keeps the status of each victory
bool hasBingoVertically = false;
int winnerVertically = -1;
int[,] cardWinnerVertically = null;

bool hasBingoHorizontally = false;
int winnerHorizontally = -1;
int[,] cardWinnerHorizontally = null;

bool hasTotallyBingo = false;
int winnerTotally = -1;
int[,] cardWinnerTotally = null;

// main loop stop condition
bool gameOver = false;

// Read players names
for (int i = 0; i < playerAmount; i++)
{
    Console.Write($"Enter {i + 1} player name: ");
    playersNames[i] = Console.ReadLine();
}

/*-----Functions-----*/

/*
 * Read a int from default input and returns it
 * Parameters: 
 * string title: title to be printed
 * int minimum: minimum value that can be returned
 */
int ReadInt(string title, int minimum)
{
    int result;
    do
    {
        Console.WriteLine(title);
        Console.Write("R: ");
        result = int.Parse(Console.ReadLine());
    } while (result < minimum);

    return result;
}

/*
 * Function that returns a new matrix 5x5 with sorted numbers
 * between 1-99 that are not repeated
 */
int[,] CreateCard()
{
    int[,] matrix = new int[5, 5];
    int number;

    for (int line = 0; line < 5; line++)
    {
        for (int column = 0; column < 5; column++)
        {
            do
            {
                number = new Random().Next(1, 100);

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
 * Instantiate a new matriz for every card in game
 */
void PopulateCards()
{
    for (int player = 0; player < playerAmount; player++)
    {
        for (int card = 0; card < cardsPerPlayer; card++)
        {
            cards[player, card] = CreateCard();
        }
    }
}

/*
 * Method to check if is bingo in a column
 */
bool IsBingoVertically(int[,] card)
{
    bool bingo = false;

    for (int l = 0; l < 5; l++)
    {
        int count = 0;
        for (int c = 0; c < 5; c++)
        {
            if (card[c, l] < 0)
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

/*
 * Method to check if is bingo in a line
 */
bool IsBingoHorizontally(int[,] card)
{
    bool bingo = false;

    for (int l = 0; l < 5; l++)
    {
        int count = 0;
        for (int c = 0; c < 5; c++)
        {
            if (card[l, c] < 0)
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

/*
 * Method to check if is bingo in a entire card
 */
bool IsTotallyBingo(int[,] card)
{
    bool bingo = false;
    int count = 0;

    for (int l = 0; l < 5; l++)
    {
        for (int c = 0; c < 5; c++)
        {
            if (card[c, l] < 0)
                count++;
        }
    }
    if (count == 25)
        bingo = true;

    return bingo;
}

/*
 * Negate the number of all tables that have a corresponding match
 */
void CheckCards(int number)
{
    for (int player = 0; player < playerAmount; player++)
    {
        for (int card = 0; card < cardsPerPlayer; card++)
        {
            for (int line = 0; line < 5; line++)
            {
                for (int column = 0; column < 5; column++)
                {
                    if (cards[player, card][line, column] == number)
                    {
                        cards[player, card][line, column] = number * -1;
                    }
                }
            }
        }
    }
}

/*
 * Every round this function is claimed
 * Sorts an number and check for bingos
 */
void Round()
{
    // Sorts a number and check for bingos
    int number = SortNumber();
    sortedNumbers[indexSortedNumbers++] = number;

    CheckCards(number);

    // Check for Horizontally winners
    if (!hasBingoHorizontally)
    {
        for (int player = 0; player < playerAmount && !hasBingoHorizontally; player++)
        {
            for (int card = 0; card < cardsPerPlayer && !hasBingoHorizontally; card++)
            {
                if (IsBingoHorizontally(cards[player, card]))
                {
                    hasBingoHorizontally = true;
                    winnerHorizontally = player;
                    cardWinnerHorizontally = cards[player, card];
                    playersPoints[player] += 1;
                }
            }
        }
    }

    // Check for Vertically winners
    if (!hasBingoVertically)
    {
        for (int player = 0; player < playerAmount && !hasBingoVertically; player++)
        {
            for (int card = 0; card < cardsPerPlayer && !hasBingoVertically; card++)
            {
                if (IsBingoVertically(cards[player, card]))
                {
                    hasBingoVertically = true;
                    winnerVertically = player;
                    cardWinnerVertically = cards[player, card];
                    playersPoints[player] += 1;
                }
            }
        }
    }

    // Check for Entire card winners
    if (!hasTotallyBingo)
    {
        for (int player = 0; player < playerAmount && !hasTotallyBingo; player++)
        {
            for (int card = 0; card < cardsPerPlayer && !hasTotallyBingo; card++)
            {
                if (IsTotallyBingo(cards[player, card]))
                {
                    hasTotallyBingo = true;
                    winnerTotally = player;
                    cardWinnerTotally = cards[player, card];
                    playersPoints[player] += 5;
                    gameOver = true;
                }
            }
        }
    }

}

/*
 * Sort a number between 1 and 99 without repeating
 */
int SortNumber()
{
    int number;
    bool contains;

    do
    {
        contains = false;
        number = new Random().Next(1, 100);

        for (int i = 0; i < sortedNumbers.Length; i++)
        {
            if (sortedNumbers[i] == number)
            {
                contains = true;
            }
        }

    } while (contains);

    return number;
}

/*
 * Print all cards in a row for a specific player
 */
void PrintCardByPlayer(int player)
{

    for (int line = 0; line < 5; line++)
    {
        for (int cardIndex = 0; cardIndex < cardsPerPlayer; cardIndex++)
        {
            for (int column = 0; column < 5; column++)
            {
                if (column == 0)
                {
                    Console.Write("| ");
                }

                if (cards[player, cardIndex][line, column] > 0)
                {
                    Console.Write($"{cards[player, cardIndex][line, column]:00} ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    int num = cards[player, cardIndex][line, column] * -1;
                    Console.Write($"{num:00}");
                    Console.ResetColor();
                    Console.Write(" ");
                }

                if (column == 4)
                {
                    Console.Write("| ");
                }
            }
        }
        Console.WriteLine();
    }
}

/*
 * Print the corresponding cards
 */
void PrintCardWinner(int[,] matrix, string title, int winner)
{
    Console.WriteLine(title);
    Console.WriteLine($"Winner: {playersNames[winner]}");
    for (int l = 0; l < 5; l++)
    {
        for (int c = 0; c < 5; c++)
        {
            if (matrix[l, c] > 0)
            {
                Console.Write($"{matrix[l, c]:00} ");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                int num = matrix[l, c] * -1;
                Console.Write($"{num:00}");
                Console.ResetColor();
                Console.Write(" ");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine("======================================");
}

/*
 * Creates a secoundary array with the players points and names and sort it
 */
void PrintScoreBoard()
{
    string[] names = new string[playerAmount];
    int[] points = new int[playerAmount];

    // Making a copy to dont change original arrays
    for (int i = 0; i < playerAmount; i++)
    {
        names[i] = playersNames[i];
        points[i] = playersPoints[i];
    }


    for (int i = 0; i < playerAmount; i++)
    {
        for (int j = i + 1; j < playerAmount; j++)
        {
            if (points[i] < points[j])
            {
                int tempPoints = points[i];
                points[i] = points[j];
                points[j] = tempPoints;

                string tempNames = names[i];
                names[i] = names[j];
                names[j] = tempNames;

            }
        }
    }

    Console.WriteLine("\n==============ScoreBoard==============");
    for (int i = 0; i < playerAmount; i++)
    {
        int pPoint = points[i];

        string pName = names[i];
        Console.WriteLine($"{pName}: {pPoint}");
    }
}

/*
 * Prints every sorted numbers
 */
void PrintSortedNumbers()
{
    if (indexSortedNumbers > 0)
    {
        Console.Write($"\n\nSorted numbers({indexSortedNumbers}): ");
        for (int i = 0; i < indexSortedNumbers; i++)
        {
            if(i == indexSortedNumbers - 1)
                Console.Write($"{sortedNumbers[i]}");
            else
                Console.Write($"{sortedNumbers[i]}-");
        }
    }
    Console.WriteLine("\n======================================");
}

/*
 * Print winners if exists
 */
void PrintWinners()
{
    if (hasBingoHorizontally)
        PrintCardWinner(cardWinnerHorizontally, "Card winner in horizontally:", winnerHorizontally);

    if (hasBingoVertically)
        PrintCardWinner(cardWinnerVertically, "Card winner in vertically:", winnerVertically);

    if (hasTotallyBingo)
        PrintCardWinner(cardWinnerTotally, "Card winner in totally:", winnerTotally);
}

/*
 * Prints logo in console
 */
void PrintLogo()
{
    Console.WriteLine(".______    __  .__   __.   _______   ______    __  \r\n|   _  \\  |  | |  \\ |  |  /  _____| /  __  \\  |  | \r\n|  |_)  | |  | |   \\|  | |  |  __  |  |  |  | |  | \r\n|   _  <  |  | |  . `  | |  | |_ | |  |  |  | |  | \r\n|  |_)  | |  | |  |\\   | |  |__| | |  `--'  | |__| \r\n|______/  |__| |__| \\__|  \\______|  \\______/  (__) \r\n                                                   ");
}

void PrintEveryoneCards()
{
    // Print every card from each player
    for (int player = 0; player < playerAmount; player++)
    {
        Console.WriteLine($"{playersNames[player]}: ");
        PrintCardByPlayer(player);

        for (int i = 0; i < cardsPerPlayer; i++)
        {
            Console.Write("===================");
        }
        Console.WriteLine();
    }
}




/*-----Main game execution-----*/
PopulateCards();

/*
 * Main loop that runs until the game is over
 */
while (!gameOver)
{
    // Clear console and print logo
    Console.Clear();
    PrintLogo();

    // Print sorted numbers
    PrintSortedNumbers();

    // Print winners if exists
    PrintWinners();

    // Print score board
    PrintEveryoneCards();

    Console.ReadKey();
    Round();
}

// Game over with all bingos and score board
if (gameOver)
{
    Console.Clear();
    PrintLogo();

    PrintSortedNumbers();

    Console.WriteLine("\n==============All bingos==============");
    PrintWinners();

    PrintScoreBoard();

    Console.Write("Press any key to continue...");
    Console.ReadKey();
}