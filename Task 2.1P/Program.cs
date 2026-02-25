using System;
using System.Collections;

namespace SIT331_1_1P
{
    // Add an enumeration method for each direction the robot faces
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    // Create a Robot class which completes all required commands and implements all program constraints
    public class Robot
    {

        public int X { get; set; }
        public int Y { get; set; }
        public Direction D { get; set; }
        public int mapSize { get; } // Get the N size of map
        public bool placed { get; private set; } // Ensure robot doesn't move until 'placed' equals true

        // Constructor for Robot class
        public Robot(int mapsize)
        {
            mapSize = mapsize;
            placed = false;
        }

        // Place the robot using the x,y,d parameters
        public void Place(int x, int y, Direction d)
        {
            X = x;
            Y = y;
            D = d;
            placed = true;
        }

        // Turn the robot
        public void Turn(string turnDirection)
        {
            if (turnDirection == "Left")
            {
                D = (Direction)(((int)D + 3) % 4);
            }
            else if (turnDirection == "Right")
            {
                D = (Direction)(((int)D + 1) % 4);
            }
        }

        // Move the robot one space depending on the direction it is facing
        public void Move()
        {
            int newX = X;
            int newY = Y;

            switch (D)
            {
                case Direction.NORTH:
                    newY++;
                    break;
                case Direction.EAST:
                    newX++;
                    break;
                case Direction.SOUTH:
                    newY--;
                    break;
                case Direction.WEST:
                    newX--;
                    break;
            }

            if (validPosition(newX, newY))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                Console.WriteLine("Invalid move! Robot is at the edge of the map and cannot fall off.");
            }
        }

        // Ensure the robot is in a valid position
        public bool validPosition(int x, int y)
        {
            return x >= 0 && x < mapSize && y >= 0 && y < mapSize;
        }
    }

    // The Program class creates an instance of a robot and a map size N
    public class Program
    {
        static void Main(string[] args)
        {
            // Put a do while in here??
            Console.Write("Select a map size for the robot to move within: ");
            int mapSize = int.Parse(Console.ReadLine());
            var robot = new Robot(mapSize);
            // Display an error message explaining map size constraints
            if (mapSize > 100 | mapSize < 2)
            {
                Console.WriteLine("Invalid map size entered! Map size must be larger than 2 and smaller than 100.");
                return;
            }
            // The below methods call all available robot commands
            while (true)
            {
                Console.Write("Enter a command for the Robot: ");
                string command = Console.ReadLine().Trim().ToUpper();
                // If PLACE command is selected
                if (command.StartsWith("PLACE"))
                {
                    string xVal = command.Split('(', ',')[1];
                    string yVal = command.Split('(', ',')[2];
                    string dVal = command.Split('(', ',', ')')[3];
                    string[] values = command.Split(',');
                    // Check the correct value formats were entered and place the robot if values are valid
                    if (values.Length == 3 && (int.TryParse(xVal, out int x)) && (int.TryParse(yVal, out int y)) && Enum.TryParse(dVal, out Direction d))
                    {
                        robot.Place(x,y,d);
                        Console.WriteLine($"Robot Placed.\nCurrent Position: ({robot.X}, {robot.Y}), Facing: {robot.D}");
                    }
                    // Display error message if input is invalid.
                    else
                    {
                        Console.WriteLine("Invalid values entered for PLACE command. Please enter values in the format: PLACE(x,y,d)");
                    }
                }
                // Once robot is placed, LEFT, RIGHT & MOVE selections can be selected as below
                if (robot.placed)
                {
                    // If MOVE robot is selected
                    if (command == "MOVE")
                    {
                        robot.Move();
                        Console.WriteLine($"Current Position: ({robot.X}, {robot.Y}), Facing: {robot.D}");
                    }
                    // To turn robot LEFT
                    else if (command == "LEFT")
                    {
                        robot.Turn("Left");
                        Console.WriteLine($"Current Position: ({robot.X}, {robot.Y}), Facing: {robot.D}");
                    }
                    // To turn robot RIGHT
                    else if (command == "RIGHT")
                    {
                        robot.Turn("Right");
                        Console.WriteLine($"Current Position: ({robot.X}, {robot.Y}), Facing: {robot.D}");
                    }
                    // Display an invalid command error message if anything else was entered
                    else if(command.StartsWith("PLACE") != true)
                    {
                        Console.WriteLine("Invalid Command!");
                    }
                }
                // Error message is displayed if user tries to enter a command when robot is not yet placed
                else
                {
                    Console.WriteLine("Robot must be placed first before it can complete any other commands.");
                }
            }
        }
    }
}







