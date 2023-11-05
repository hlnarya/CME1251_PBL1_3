using System;

namespace CHECKERSCRYPTO
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] board = new string[8, 8] { { "o", "o", "o", ".", ".", ".", ".", "." },
                                                 { "o", "o", "o", ".", ".", ".", ".", "." },
                                                 { "o", "o", "o", ".", ".", ".", ".", "." },
                                                 { ".", ".", ".", ".", ".", ".", ".", "." },
                                                 { ".", ".", ".", ".", ".", ".", ".", "." },
                                                 { ".", ".", ".", ".", ".", "x", "x", "x" },
                                                 { ".", ".", ".", ".", ".", "x", "x", "x" },
                                                 { ".", ".", ".", ".", ".", "x", "x", "x" } };
            int[] jxo = new int[18] { 0, 0, 0, 1, 1, 1, 2, 2, 2, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
            //ixo : i for x and o pieces
            int[] ixo = new int[18] { 0, 1, 2, 0, 1, 2, 0, 1, 2, 5, 6, 7, 5, 6, 7, 5, 6, 7 };
            //jxo : j for x and o pieces
            //represent exact location of all 18 "x" an "o" 
            //string[] gameplay = new string[72001];
            //int temp = 0;
            int turn = 1;
            bool wincontrol = false;
            bool oturn = false;
            int icurse = 7, jcurse = 7;//will represent cursor position
            int selectedxID = 9;//identity of every selected X
            bool chain = false;//human chain
            bool chaino = false;//bot chain
            int winner = 0;
            bool selection = false;
            int OID = 0;//every chosen O has a unique identity
            while (wincontrol == false)
            {
                //drawing board
                Console.Clear();
                for (int j = -2; j < 9; j++)
                {
                    if (j > -1 && j < 8)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write((j + 1) + " ");
                        Console.ResetColor();
                    }
                    if (j == -1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("  1 2 3 4 5 6 7 8   ");
                        Console.ResetColor();
                    }
                    if (j == 8)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("                    ");
                        Console.ResetColor();
                    }
                    for (int i = -2; i < 8; i++)
                    {
                        if (j == -2)
                        {
                            if (i > -1 && i < 8)
                                Console.Write(" ");
                            else
                                Console.Write((" "));
                        }
                        if (j >= 0 && j <= 7 && i <= 7 && i >= 0)
                        {
                            if ((j + i) % 2 == 0)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.Black;

                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            if (ixo[selectedxID] == i && jxo[selectedxID] == j && selection == true)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;


                            }

                            Console.Write(board[i, j] + " ");
                            Console.ResetColor();
                        }

                        if (j > -1 && j < 8 && i == 7)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write("  ");
                            Console.ResetColor();
                        }
                    }
                    Console.Write("\n");
                }
                Console.SetCursorPosition(23, 1);
                Console.Write("Turn: " + turn);
                Console.SetCursorPosition(23, 2);
                Console.Write("Player's turn");
                Console.SetCursorPosition(icurse * 2 + 2, jcurse + 2);
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (icurse < 7)
                            icurse++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (jcurse > 0)
                            jcurse--;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (icurse > 0)
                            icurse--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (jcurse < 7)
                            jcurse++;
                        break;
                    case ConsoleKey.Z:
                        if (selection == false)
                        {
                            for (int xid = 9; xid < 18; xid++)
                            {
                                //reprresent every "x" with a number
                                if (jcurse == jxo[xid] && icurse == ixo[xid] && chain == false)
                                {
                                    selectedxID = xid;
                                    selection = true;
                                    break;
                                }
                            }
                            
                            if (jcurse == jxo[selectedxID] && icurse == ixo[selectedxID] && chain == true)
                                selection = true;
                        }
                        break;
                    case ConsoleKey.X:
                        if (selection == true)
                        {   //controlling all valid movements during move
                            if (icurse == ixo[selectedxID])
                            {
                                if (jcurse == jxo[selectedxID] + 1 && board[icurse, jcurse] == "." && chain == false)
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    jxo[selectedxID] = jcurse;
                                    oturn = true;
                                    turn++;
                                }
                                else if (jcurse == jxo[selectedxID] - 1 && board[icurse, jcurse] == "." && chain == false)
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    jxo[selectedxID] = jcurse;
                                    oturn = true;
                                    turn++;
                                }
                                else if (jcurse == jxo[selectedxID] - 2 && board[icurse, jcurse] == "." && board[icurse, jcurse + 1] != ".")
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    jxo[selectedxID] = jcurse;
                                    chain = true;
                                }
                                else if (jcurse == jxo[selectedxID] + 2 && board[icurse, jcurse] == "." && board[icurse, jcurse - 1] != ".")
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    jxo[selectedxID] = jcurse;
                                    chain = true;
                                }

                            }
                            else if (icurse == ixo[selectedxID] + 1 && chain == false)
                            {
                                if (jcurse == jxo[selectedxID] && board[icurse, jcurse] == ".")
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    ixo[selectedxID] = icurse;
                                    oturn = true;
                                    turn++;
                                }

                            }
                            else if (icurse == ixo[selectedxID] + 2)
                            {
                                if (jcurse == jxo[selectedxID] && board[icurse, jcurse] == "." && board[icurse - 1, jcurse] != ".")
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    ixo[selectedxID] = icurse;
                                    chain = true;
                                }

                            }
                            else if (icurse == ixo[selectedxID] - 1 && chain == false)
                            {
                                if (jcurse == jxo[selectedxID] && board[icurse, jcurse] == ".")
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    ixo[selectedxID] = icurse;
                                    oturn = true;
                                    turn++;
                                }

                            }
                            else if (icurse == ixo[selectedxID] - 2)
                            {
                                if (jcurse == jxo[selectedxID] && board[icurse, jcurse] == "." && board[icurse + 1, jcurse] != ".")
                                {
                                    board[icurse, jcurse] = "x";
                                    board[ixo[selectedxID], jxo[selectedxID]] = ".";
                                    ixo[selectedxID] = icurse;
                                    chain = true;
                                }

                            }

                        }
                        while (oturn == true)
                        {
                            //human wincontrol
                            wincontrol = true;
                            winner = 1;
                            for (int w = 0; w < 9; w++)
                            {
                                if (ixo[w + 9] >= 3 || jxo[w + 9] >= 3)
                                {
                                    wincontrol = false;
                                    winner = 0;
                                }
                            }
                            if (wincontrol == true)
                                break;
                            Random number = new Random();
                            if (chaino == false)
                            {
                                OID = number.Next(0, 9);
                            }
                            int move = number.Next(0, 88);
                            int chaindes = number.Next(0, 7);
                            //to move in these four directions more than others
                            if (move >= 8 && move < 28)
                                move = 8;
                            if (move >= 28 && move < 48)
                                move = 9;
                            if (move >= 48 && move < 68)
                                move = 10;
                            if (move >= 68 && move < 88)
                                move = 11;

                            switch (move)
                            {
                                case 0:
                                    if (jxo[OID] > 0 && (ixo[OID] < 5 || jxo[OID] < 5))
                                        if (board[ixo[OID], jxo[OID] - 1] == "." && chaino == false)
                                        {
                                            board[ixo[OID], jxo[OID] - 1] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            jxo[OID]--;
                                            oturn = false;
                                            turn++;

                                        }
                                    break;
                                case 1:
                                    if (jxo[OID] > 1 && (ixo[OID] < 5 || jxo[OID] < 5))
                                        if (board[ixo[OID], jxo[OID] - 2] == "." && board[ixo[OID], jxo[OID] - 1] != ".")
                                        {
                                            board[ixo[OID], jxo[OID] - 2] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            jxo[OID] = jxo[OID] - 2;

                                            chaino = true;

                                        }
                                    break;
                                case 2:
                                    if (jxo[OID] < 7)
                                        if (board[ixo[OID], jxo[OID] + 1] == "." && chaino == false)
                                        {
                                            board[ixo[OID], jxo[OID] + 1] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            jxo[OID]++;
                                            oturn = false;
                                            turn++;

                                        }
                                    break;
                                case 3:
                                    if (jxo[OID] < 6)
                                        if (board[ixo[OID], jxo[OID] + 2] == "." && board[ixo[OID], jxo[OID] + 1] != ".")
                                        {
                                            board[ixo[OID], jxo[OID] + 2] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            jxo[OID] = jxo[OID] + 2;

                                            chaino = true;

                                        }
                                    break;
                                case 4:
                                    if (ixo[OID] > 0 && (ixo[OID] < 5 || jxo[OID] < 5))
                                        if (board[ixo[OID] - 1, jxo[OID]] == "." && chaino == false)
                                        {
                                            board[ixo[OID] - 1, jxo[OID]] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            ixo[OID]--;
                                            oturn = false;
                                            turn++;

                                        }
                                    break;
                                case 5:
                                    if (ixo[OID] > 1 && (ixo[OID] < 5 || jxo[OID] < 5))
                                        if (board[ixo[OID] - 2, jxo[OID]] == "." && board[ixo[OID] - 1, jxo[OID]] != ".")
                                        {
                                            board[ixo[OID] - 2, jxo[OID]] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            ixo[OID] = ixo[OID] - 2;

                                            chaino = true;

                                        }
                                    break;
                                case 6:
                                    if (ixo[OID] < 7)
                                        if (board[ixo[OID] + 1, jxo[OID]] == "." && chaino == false)
                                        {
                                            board[ixo[OID] + 1, jxo[OID]] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            ixo[OID]++;
                                            oturn = false;
                                            turn++;

                                        }
                                    break;
                                case 7:
                                    if (ixo[OID] < 6)
                                        if (board[ixo[OID] + 2, jxo[OID]] == "." && board[ixo[OID] + 1, jxo[OID]] != ".")
                                        {
                                            board[ixo[OID] + 2, jxo[OID]] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            ixo[OID] = ixo[OID] + 2;

                                            chaino = true;

                                        }
                                    break;
                                case 8:
                                    if (jxo[OID] < 6)
                                        if (board[ixo[OID], jxo[OID] + 1] == "." && chaino == false)
                                        {
                                            board[ixo[OID], jxo[OID] + 1] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            jxo[OID]++;
                                            oturn = false;
                                            turn++;

                                        }
                                    break;
                                case 9:
                                    if (jxo[OID] < 5)
                                        if (board[ixo[OID], jxo[OID] + 2] == "." && board[ixo[OID], jxo[OID] + 1] != ".")
                                        {
                                            board[ixo[OID], jxo[OID] + 2] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            jxo[OID] = jxo[OID] + 2;

                                            chaino = true;

                                        }
                                    break;

                                case 10:
                                    if (ixo[OID] < 6)
                                        if (board[ixo[OID] + 1, jxo[OID]] == "." && chaino == false)
                                        {
                                            board[ixo[OID] + 1, jxo[OID]] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            ixo[OID]++;
                                            oturn = false;
                                            turn++;

                                        }
                                    break;
                                case 11:
                                    if (ixo[OID] < 5)
                                        if (board[ixo[OID] + 2, jxo[OID]] == "." && board[ixo[OID] + 1, jxo[OID]] != ".")
                                        {
                                            board[ixo[OID] + 2, jxo[OID]] = "o";
                                            board[ixo[OID], jxo[OID]] = ".";
                                            ixo[OID] = ixo[OID] + 2;

                                            chaino = true;
                                        }
                                    break;
                            }
                            if (chaino == true)
                            {
                                if (chaindes == 0)
                                {
                                    oturn = false;
                                    turn++;
                                    chaino = false;

                                }
                            }
                            wincontrol = true;
                            winner = 2;
                            for (int w = 0; w < 9; w++)
                            {
                                if (ixo[w] < 5 || jxo[w] < 5)
                                {
                                    wincontrol = false;
                                    winner = 0;
                                }
                            }
                        }
                        selection = false;
                        break;
                    case ConsoleKey.C:
                        if (chain == true)
                        {
                            turn++;
                            selection = false;
                            oturn = true;
                            chain = false;
                            while (oturn == true)
                            {
                                wincontrol = true;
                                winner = 1;
                                for (int w = 0; w < 9; w++)
                                {
                                    if (ixo[w + 9] >= 3 || jxo[w + 9] >= 3)
                                    {
                                        wincontrol = false;
                                        winner = 0;
                                    }
                                }
                                if (wincontrol == true)
                                    break;
                                Random number = new Random();
                                if (chaino == false)
                                {
                                    OID = number.Next(0, 9);
                                }
                                int move = number.Next(0, 88);
                                int chaindes = number.Next(0, 7);
                                if (move >= 8 && move < 28)
                                    move = 8;
                                if (move >= 28 && move < 48)
                                    move = 9;
                                if (move >= 48 && move < 68)
                                    move = 10;
                                if (move >= 68 && move < 88)
                                    move = 11;

                                switch (move)
                                {
                                    case 0:
                                        if (jxo[OID] > 0 && (ixo[OID] < 5 || jxo[OID] < 5))
                                            if (board[ixo[OID], jxo[OID] - 1] == "." && chaino == false)
                                            {
                                                board[ixo[OID], jxo[OID] - 1] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                jxo[OID]--;
                                                oturn = false;
                                                turn++;

                                            }
                                        break;
                                    case 1:
                                        if (jxo[OID] > 1 && (ixo[OID] < 5 || jxo[OID] < 5))
                                            if (board[ixo[OID], jxo[OID] - 2] == "." && board[ixo[OID], jxo[OID] - 1] != ".")
                                            {
                                                board[ixo[OID], jxo[OID] - 2] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                jxo[OID] = jxo[OID] - 2;

                                                chaino = true;

                                            }
                                        break;
                                    case 2:
                                        if (jxo[OID] < 7)
                                            if (board[ixo[OID], jxo[OID] + 1] == "." && chaino == false)
                                            {
                                                board[ixo[OID], jxo[OID] + 1] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                jxo[OID]++;
                                                oturn = false;
                                                turn++;

                                            }
                                        break;
                                    case 3:
                                        if (jxo[OID] < 6)
                                            if (board[ixo[OID], jxo[OID] + 2] == "." && board[ixo[OID], jxo[OID] + 1] != ".")
                                            {
                                                board[ixo[OID], jxo[OID] + 2] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                jxo[OID] = jxo[OID] + 2;

                                                chaino = true;

                                            }
                                        break;
                                    case 4:
                                        if (ixo[OID] > 0 && (ixo[OID] < 5 || jxo[OID] < 5))
                                            if (board[ixo[OID] - 1, jxo[OID]] == "." && chaino == false)
                                            {
                                                board[ixo[OID] - 1, jxo[OID]] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                ixo[OID]--;
                                                oturn = false;
                                                turn++;

                                            }
                                        break;
                                    case 5:
                                        if (ixo[OID] > 1 && (ixo[OID] < 5 || jxo[OID] < 5))
                                            if (board[ixo[OID] - 2, jxo[OID]] == "." && board[ixo[OID] - 1, jxo[OID]] != ".")
                                            {
                                                board[ixo[OID] - 2, jxo[OID]] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                ixo[OID] = ixo[OID] - 2;

                                                chaino = true;

                                            }
                                        break;
                                    case 6:
                                        if (ixo[OID] < 7)
                                            if (board[ixo[OID] + 1, jxo[OID]] == "." && chaino == false)
                                            {
                                                board[ixo[OID] + 1, jxo[OID]] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                ixo[OID]++;
                                                oturn = false;
                                                turn++;

                                            }
                                        break;
                                    case 7:
                                        if (ixo[OID] < 6)
                                            if (board[ixo[OID] + 2, jxo[OID]] == "." && board[ixo[OID] + 1, jxo[OID]] != ".")
                                            {
                                                board[ixo[OID] + 2, jxo[OID]] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                ixo[OID] = ixo[OID] + 2;

                                                chaino = true;

                                            }
                                        break;
                                    case 8:
                                        if (jxo[OID] < 5)
                                            if (board[ixo[OID], jxo[OID] + 1] == "." && chaino == false)
                                            {
                                                board[ixo[OID], jxo[OID] + 1] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                jxo[OID]++;
                                                oturn = false;
                                                turn++;

                                            }
                                        break;
                                    case 9:
                                        if (jxo[OID] < 4)
                                            if (board[ixo[OID], jxo[OID] + 2] == "." && board[ixo[OID], jxo[OID] + 1] != ".")
                                            {
                                                board[ixo[OID], jxo[OID] + 2] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                jxo[OID] = jxo[OID] + 2;

                                                chaino = true;

                                            }
                                        break;

                                    case 10:
                                        if (ixo[OID] < 5)
                                            if (board[ixo[OID] + 1, jxo[OID]] == "." && chaino == false)
                                            {
                                                board[ixo[OID] + 1, jxo[OID]] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                ixo[OID]++;
                                                oturn = false;
                                                turn++;

                                            }
                                        break;
                                    case 11:
                                        if (ixo[OID] < 4)
                                            if (board[ixo[OID] + 2, jxo[OID]] == "." && board[ixo[OID] + 1, jxo[OID]] != ".")
                                            {
                                                board[ixo[OID] + 2, jxo[OID]] = "o";
                                                board[ixo[OID], jxo[OID]] = ".";
                                                ixo[OID] = ixo[OID] + 2;

                                                chaino = true;

                                            }
                                        break;

                                }
                                if (chaino == true)
                                {
                                    if (chaindes == 0)
                                    {
                                        oturn = false;
                                        turn++;
                                        chaino = false;

                                    }
                                }
                                wincontrol = true;
                                winner = 2;
                                for (int w = 0; w < 9; w++)
                                {
                                    if (ixo[w] < 5 || jxo[w] < 5)
                                    {
                                        wincontrol = false;
                                        winner = 0;
                                    }
                                }

                            }

                        }
                        break;
                }

            }
            Console.Clear();
            if (winner == 1)
                Console.WriteLine("You Win!");
            else
                Console.WriteLine("You Lost!");

            Console.ReadKey();

        }
    }
}
