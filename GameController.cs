using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2
{
    public class GameController
    {
        private Dice dice;
    private Board board;

    public GameController()
    {
        this.dice = new Dice();
        this.board = new Board();
    }
    private  int playerRoll(int player, Random rand)   /// feito por Bruno (para o relatorio)
    {
        string input;
        bool played = false;
        int roll = this.dice.dice(rand);


        do
        {
            //Asks the player to roll the die
            Console.WriteLine($"Player{player}, it's your turn! Press /R to roll the die: ");
            //Reads the input of the player        
            input = Console.ReadLine();

            //if the input is correct (R or r) rolls the die
            if (input == "r" || input == "R")
            {
                //prints the number rolled
                Console.WriteLine($"\nPlayer{player} roll: {roll}; ");
                break;

            }
            //if the input is incorrect asks to press R again with a Error Message
            else
            {
                Console.WriteLine("Invalid input!!");
                Console.WriteLine("Please use /R to roll the die ");
            }
            //changes boolean to true so it can stop the cycle
            played = true;
        } while (played == true);

        //returns the number rolled in the die
        return roll;

    }

    private Boolean movePlayerByDie(int player, int opponent, int moveByDie, int[] board)
    {
        //gets the position of the player on the board
        int position = Array.IndexOf(board, player);
        int positionOpponent = Array.IndexOf(board, opponent);
        bool normalTile = default;
        int newPos = position + moveByDie;

        while (normalTile != true)
        {
            //if the player is on the board moves normally
            if (position != -1)
            {
                if (newPos > 24)
                {
                    newPos = 24 - (newPos - 24);
                }
                if (newPos == 24)
                {
                    Console.WriteLine($"Congratulations!! Player {player} WON");
                    return true;
                }
                board[position] = 0;
                if (board[newPos] == opponent)
                {
                    positionOpponent -= 1;
                    board[positionOpponent] = opponent;
                    Console.WriteLine($"Player {opponent} was there and was moved back 1 position; ");
                }
                if (newPos == 0)
                {
                    normalTile = true;
                }
                else if (board[newPos] == 4)
                {
                    newPos = 0;
                    Console.WriteLine($"Player{player} was in a Cobra Tile! Moved back to the start of the board; ");
                }
                else if (board[newPos] == 5)
                {
                    newPos += 2;
                    Console.WriteLine($"Player{player} was in a Boost Tile! Advanced 2 positions; ");
                }
                else if (board[newPos] == 6)
                {
                    newPos -= 2;
                    Console.WriteLine($"Player{player} was in a U-Turn Tile! Moved back 2 positions; ");
                }
                board[newPos] = player;
            }
            else
            {                                          //se o jogador estiver fora do tabuleiro
                Console.WriteLine(position);
                if (board[newPos] == opponent)
                {
                    if (moveByDie == 1)
                    {
                        board[positionOpponent] = 0;
                    }
                    else
                    {
                        positionOpponent -= 1;
                        board[positionOpponent] = opponent;
                    }
                    Console.WriteLine($"Player {opponent} was there and was moved back 1 position; ");
                }
                if (newPos == 0)
                {
                    normalTile = true;
                }
                else if (board[newPos] == 5)
                {
                    newPos += 2;
                    Console.WriteLine($"Player{player} was in a Boost Tile! Advanced 2 positions; ");
                }
                else if (board[newPos] == 6)
                {
                    newPos += 2;
                    Console.WriteLine($"Player{player} was in a U-Turn Tile! Moved back 2 positions; ");
                }
                board[newPos] = player;
            }
            return false;
        }
        return false;
    }

 
    public void play()
    {
        Random rand = new Random();
        Boolean winner = false;
     
        //Creates player 1
        int player1 = 1;
        //Creates player 2   
        int player2 = 2;
        int moveByDie;

        board.printBoard();
        //while there isn't a winner runs 
        while (winner != true)                                    /// feito por Bruno (para o relatorio)
        {
            moveByDie = playerRoll(player1, rand);
            winner = movePlayerByDie(player1, player2, moveByDie, board.board);
            board.printBoard();
            if (winner == true)
            {
                break;
            }
            moveByDie = playerRoll(player2, rand);
            winner = movePlayerByDie(player2, player1, moveByDie, board.board);
            board.printBoard();
        }
    }
    }
}