using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO; 
namespace Snake
{
  //Creates a data structure position that is made out of a row and a column. 
    struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    class Program
    {
        static public void SaveScore(string username, int score)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\admin\\Documents\\GitHub\\DevTools4\\Snake\\scoreboard.txt"); 
            string entry; 
            entry = "{0}: {1} points"; 
            entry = string.Format(entry, username, score.ToString()); 
            sw.WriteLine(entry);
            sw.Close(); 
        }
        
        static void Main(string[] args)
        {
            
			//intialise different variable
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            int lastFoodTime = 0;
            int foodDissapearTime = 16000;
            int negativePoints = 0;
            string userName;
            Console.WriteLine("Enter your name: ");
            userName = Console.ReadLine();
            Console.Clear(); 
            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };
            double sleepTime = 100;
            int direction = right;
            Random randomNumbersGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight;
            lastFoodTime = Environment.TickCount;

			//intialise the position of first 5 obstacle
            List<Position> obstacles = new List<Position>()
            {
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth)),
            };

			//produce obstacles item on certain position with Cyan coloured "="
            foreach (Position obstacle in obstacles)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.col, obstacle.row);
                Console.Write("=");
            }

			//Declare a new variable snakeElements
            Queue<Position> snakeElements = new Queue<Position>();
			//Initialise the length of the snake
            for (int i = 0; i <= 3; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
            
            // Creates new food at a random position (as long as the position has not been taken by an obstacles or the snake)
            Position food;
            do
            {
                food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth));
            }
            while (snakeElements.Contains(food) || obstacles.Contains(food));
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");

            // Redraws the snake
            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");
            }

            int userPoints = 0;
            // Loops the game till it ends
            while (true)
            {
                userPoints = (snakeElements.Count - 4) * 100 - negativePoints;
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("Score: " + userPoints + " ");
            
                // Increment 1
                negativePoints++;

                // Detects key inputs to change direction 
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != right) direction = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left) direction = right;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = up;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up) direction = down;
                    }
                }

                Position snakeHead = snakeElements.Last();  // Sets the last element of the snake as the snake head 
                Position nextDirection = directions[direction]; // Sets the direction the snake is moving

                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                    snakeHead.col + nextDirection.col); // Sets the new position of snake head based on the snake's direction 
                
                // Makes the snake come out from the other side of the window when it passes through the edge of the window 
                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;

                // When snake hits the obstacle or the snake itself, console will show "Game over!" and the total amount of points gathered
                if (snakeElements.Contains(snakeNewHead) || obstacles.Contains(snakeNewHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    string gameover = "Game over!";
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                    Console.Write(new string(' ', (Console.WindowWidth - gameover.Length) / 2));
                    Console.WriteLine(gameover);
<<<<<<< HEAD
                    userPoints = (snakeElements.Count - 4) * 100 - negativePoints;
                    //if (userPoints < 0) userPoints = 0;
||||||| 1df7aa4
                    int userPoints = (snakeElements.Count - 6) * 100 - negativePoints;
                    //if (userPoints < 0) userPoints = 0;
=======
                    int userPoints = (snakeElements.Count - 6) * 100 - negativePoints;
                    if (userPoints < 0) userPoints = 0;
>>>>>>> 9599bc77ed13bf9dc2d2db0ccc0a8fa0f531a12c
                    userPoints = Math.Max(userPoints, 0);
<<<<<<< HEAD
                    
                    Console.Write(new string(' ', (Console.WindowWidth - 20) / 2));
                    Console.WriteLine("Your points are: {0}", userPoints);
||||||| 1df7aa4

                    string finalPoints = "Your points are: {0}";
                    Console.Write(new string(' ', (Console.WindowWidth - finalPoints.Length) / 2));
                    Console.WriteLine(finalPoints, userPoints);
=======
                    SaveScore(userName, userPoints); 
                    string finalPoints = "Your points are: {0}";
                    Console.Write(new string(' ', (Console.WindowWidth - finalPoints.Length) / 2));
                    Console.WriteLine(finalPoints, userPoints);
>>>>>>> 9599bc77ed13bf9dc2d2db0ccc0a8fa0f531a12c
                    Console.ReadLine(); 
                    return;
                }
                
                // Draws the snake's first body after the snake head in every frame
                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");

                // Adds the snake head into the snake element
                snakeElements.Enqueue(snakeNewHead);

                // Draws the snake head depending on which way the snake is facing
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (direction == right) Console.Write(">");
                if (direction == left) Console.Write("<");
                if (direction == up) Console.Write("^");
                if (direction == down) Console.Write("v");
                
                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    //If the snake's head intercepts the location of the food
                    // feeding the snake
                    do
                    {
                        //Generates a new food at a random postion, that is not in the location of the snake'sbody or the obstacles. 
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) || obstacles.Contains(food));
                    //Resets the timer
                    lastFoodTime = Environment.TickCount;
                    //Sets the cursor position to the food's row and column
                    Console.SetCursorPosition(food.col, food.row);
                    //Sets the color of the food
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    //Writes an @ in the console which serves as food
                    Console.Write("@");
                    sleepTime--;

                    //Creates a new Position named obstacle
                    Position obstacle = new Position();
                    do
                    {
                        //When the snake collides with the food, it sets obstacle to a new random position that does not contain the snake, the obstacle or food
                        obstacle = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(obstacle) ||
                        obstacles.Contains(obstacle) ||
                        (food.row != obstacle.row && food.col != obstacle.row));

                    //Adds the obstacle into an array of obstacles
                    obstacles.Add(obstacle);
                    //Sets the cursor position the the obstacles column and row
                    Console.SetCursorPosition(obstacle.col, obstacle.row);
                    //Sets the color of the obstacle
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    //Writes = in the console as visualisation of the obstacle
                    Console.Write("=");
                }
                else
                {
                    // moving...
                    //Removes the last position of the snake by writing a space, pushing the snake forwards
                    Position last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");
                }

                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    //If the time limit has expired, Negative points are increased by 50 
                    negativePoints = negativePoints + 50;
                    //Sets the cursor position to where the food was and deletes it by writing a space over it
                    Console.SetCursorPosition(food.col, food.row);
                    Console.Write(" ");
                    do
                    {
                        //Creates a new food at another random postition that does not contain the snake's body or obstacles
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) || obstacles.Contains(food));
                    //Resets the timer
                    lastFoodTime = Environment.TickCount;
                }

                //Sets the cursor position to the new column and row with the food
                Console.SetCursorPosition(food.col, food.row);
                //Change the color to yellow
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Writes an @ to represent food
                Console.Write("@");

                sleepTime -= 0.01;

                Thread.Sleep((int)sleepTime);
            }
        }
    }
}
