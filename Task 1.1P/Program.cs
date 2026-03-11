// TASK 1.1P - MOON ROBOT SIMULATOR
enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome To The Moon Robot Simulator!\n");
        // Initialize map size variable
        int mapSize;
        do
        {
            // Prompt user for map size
            Console.Write("Enter a number N >= 2 and <= 100 to create a map of size (N x N): ");
            string mapInput = Console.ReadLine();
            // Display an error message if the number entered is either less than 2 or larger than 100 or input is not an integer
            if (!int.TryParse(mapInput, out mapSize))
                Console.WriteLine("Invalid input!");
            else if (mapSize < 2 || mapSize > 100)
                Console.WriteLine("\nInvalid number entered!");
                               
        } while (mapSize < 2 || mapSize > 100); 
        // Confirm map created
        Console.WriteLine($"Map created! Size: {mapSize} x {mapSize}\n");
        
        // Initialize required variables before placing robot
        bool isPlaced = false;
        int xVal = 0;
        int yVal = 0;
        Direction direction = Direction.NORTH;
        string input;
        do
        {
            // Prompt user to place robot somewhere on the map
            Console.Write("Place the robot on the map (format is PLACE(x,y,dir)): ");
            // Trim the input to remove 'PLACE' and split into x, y and direction values
            input = Console.ReadLine().ToUpper().Trim();
            string[] words = input.Split('(',',',')');
            // Include constraints for respective xVal, yVal and direction values
            if ((words.Length > 3) && words[0] == "PLACE" && int.TryParse(words[1], out int x) && int.TryParse(words[2], out int y) && Enum.TryParse(words[3], out Direction d))
            {
                if (x >= 0 && x < mapSize && y >= 0 && y < mapSize)
                {
                    xVal = x;
                    yVal = y;
                    direction = d;
                    isPlaced = true;
                }
                else
                    Console.WriteLine("Cannot place robot outside range of map! Please enter correct x/y values.\n");
            }
            else
                Console.WriteLine("Invalid format! Enter in the format PLACE(number,number,direction)\n");
        } while (!isPlaced);
        // Confirm robot placed
        Console.WriteLine("\nRobot Placed!");
        // Once robot is placed, prompt user for command to move robot
        string command = "";
        do
        {
            Console.WriteLine($"Robot Currently at ({xVal}, {yVal}) Facing: {direction}\n");
            Console.Write("Enter a command for the robot ('move', 'left', 'right' or enter 'Q' to quit simulator): ");
            command = Console.ReadLine().ToUpper();
            // Complete respective robot commands depending on input (move, left or right) and include constraint errors
            switch (command)
            {
                case "MOVE":
                    // Moves the robot 1 space in the direction it is facing
                    Console.WriteLine($"Moving one space {direction}\n");
                        switch(direction)
                        {
                            case Direction.NORTH:
                                if (yVal + 1 < mapSize)
                                    yVal++;
                                else
                                    Console.WriteLine("Cannot complete move! Robot will fall off map.");
                                break;
                            case Direction.EAST:
                                if (xVal + 1 < mapSize)
                                    xVal++;
                                else
                                    Console.WriteLine("Cannot complete move! Robot will fall off map.");
                                break;
                            case Direction.SOUTH:
                                if (yVal - 1 >= 0)
                                    yVal--;
                                else
                                    Console.WriteLine("Cannot complete move! Robot will fall off map.");
                                break;
                            case Direction.WEST:
                                if (xVal - 1 >= 0)
                                    xVal--;
                                else
                                    Console.WriteLine("Cannot complete move! Robot will fall off map.");
                                break;
                        }
                    break;
                case "LEFT":
                    direction = (Direction)(((int)direction + 3) % 4);
                    Console.WriteLine($"Turned left to face {direction}");
                    break;
                case "RIGHT":
                    direction = (Direction)(((int)direction + 1) % 4);
                    Console.WriteLine($"Turned right to face {direction}");
                    break;
                case "Q":
                    Console.WriteLine("Q selected.. Program ended!\n");
                    return;
                default:
                    Console.WriteLine("Invalid selection made!");
                    break;
            }
        }
        while(command != "Q");
    }
}

