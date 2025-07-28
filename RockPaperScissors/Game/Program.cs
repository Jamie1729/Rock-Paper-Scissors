

int oppType;
int gamemode;
int numMoves;
Opponant opp;

String[] moveToString = ["", "Rock", "Paper", "Scissors", "Spock", "Lizard"];

Console.WriteLine("Please choose which gamemode you would like to play\n 1 - Original Rock-Paper-Scissors\n 2 - Rock-Paper-Scissors-Lizard-Spock");
while (!(Int32.TryParse(Console.ReadLine(), out gamemode) && 1 <= gamemode && gamemode <= 2))
{
    Console.WriteLine("Invalid Choice, please enter:\n 1 - Original Rock-Paper-Scissors\n 2 - Rock-Paper-Scissors-Lizard-Spock");
}
numMoves = gamemode switch
{
    1 => 3,
    2 => 5,
};

Console.WriteLine("Please choose which opponant you would like to play\n 1 - Random Choice\n 2 - Copy User");
while (!(Int32.TryParse(Console.ReadLine(), out oppType) && 1 <= oppType && oppType <= 2))
{
    Console.WriteLine("Invalid Choice, please enter:\n 1 - Random Choice\n 2 - Copy User");
}
opp = oppType switch
{
    1 => new RandomOpponant(numMoves),
    2 => new CopyOpponant(numMoves),
};

bool playing = true;
while (playing)
{
    int winner;
    int playerMove;
    Console.WriteLine("Please enter your chosen move:"); printAvaliableMoves();
    while (!(Int32.TryParse(Console.ReadLine(), out playerMove) && 1 <= playerMove && playerMove <= numMoves))
    {
        Console.WriteLine("Invalid Choice, please enter:"); printAvaliableMoves();
    }

    int oppMove = opp.getMove();
    opp.updatePlayerMoves(playerMove);
    Console.WriteLine("The computer chose: " + moveToString[oppMove]);

    switch (gamemode)
    {
        case 1:
            winner = checkResult_normal(playerMove, oppMove); break;
        case 2:
            winner = checkResult_fiveway(playerMove, oppMove); break;
        default:
            winner = -1; break;
    }
    
    if      (winner == 0) { Console.WriteLine("The round was a draw!");               }
    else if (winner == 1) { Console.WriteLine("Congratulations, you won the round!"); }
    else                  { Console.WriteLine("Unlucky, you lost the round.");        }


    Console.WriteLine("Would you like to play again? y/n");
    string choice = Console.ReadLine();
    if      (choice == "y") {continue;}
    else if (choice == "n") {break;}
    else { Console.WriteLine("Not a valid entry, I'll assume you meant no."); break; }
}

//Return values: 0 - Draw, 1 - Player Win, 2 - Computer Win
int checkResult_normal(int playerMove, int oppMove)
{
    return Mod_m(playerMove - oppMove ,3);
}
//Return values: 0 - Draw, 1 - Player Win, 2 - Computer Win
int checkResult_fiveway(int playerMove, int oppMove)
{
    if (oppMove == playerMove) { return 0; }
    int diff = Mod_m(playerMove - oppMove, 5);
    if (diff == 1 || diff == 3) { return 1; }
    else { return 2; }
}
//For some reason the % operator in C# gives the remainder not modulus:
// so -2 % 3 = -2 (instead of 1 as one might expect)
int Mod_m(int x, int m)
{
    if (m > 0)
    {
        while (x < 0) { x += m; }
        while (x >= m) { x -= m; }
    }
    else if (m < 0)
    {
        while (x > 0) { x += m; }
        while (x <= m) { x -= m; }
    }
    else
    {
        throw new DivideByZeroException();
    }
    
    return x;
}


//Only prints Lizard and Spock if that gamemode is selected
void printAvaliableMoves()
{
    for (int i = 1; i <= numMoves; i++) {
        Console.WriteLine(i + " : " + moveToString[i]);
    }
}