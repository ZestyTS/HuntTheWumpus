using HuntTheWumpus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Helper
{
    public static class UserInput
    {
        public static int GetInteger(int min, int max)
        {
            while (true)
            {
                int xPos = Console.CursorLeft;
                var input = Console.ReadLine();
                if (input == null)
                    continue;

                try
                {
                    if (int.TryParse(input, out int number))
                        if (number >= min && number <= max)
                            return number;
                        else
                            throw new ArgumentOutOfRangeException(input);
                    else
                        throw new ArgumentOutOfRangeException(input);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Error: {input} is not a valid selection.  Press any key to continue.");
                    Console.ReadKey();
                    ClearInput(xPos);
                }
            }
        }
        public static string GetString()
        {
            while (true)
            {
                int xPos = Console.CursorLeft;
                var input = Console.ReadLine();
                if (input == null)
                    continue;

                try
                {
                    if (!string.IsNullOrEmpty(input))
                        return input;
                    else
                        throw new ArgumentNullException(input);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Error: input is empty or null.  Press any key to continue.");
                    Console.ReadKey();
                    ClearInput(xPos);
                }
            }
        }
        public static void ClearInput(int cursorPos)
        {
            int y = Console.CursorTop - 1;
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.BufferWidth));

            Console.SetCursorPosition(cursorPos, y - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(cursorPos, y - 1);
        }

        public static string UserAction(int arrowQuantity)
        {
            Console.Write((arrowQuantity > 0 ? "Shoot (S), " : "") + "Move (M), Purchase Arrows (A), or Purchase Secret (P): ");
            int xPos = Console.CursorLeft;

            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;

                try
                {
                    if (arrowQuantity > 0)
                    {
                        if (input.ToUpper() == "S" || input.ToUpper() == "M" || input.ToUpper() == "A" || input.ToUpper() == "P")
                        {
                            return input.ToUpper();
                        }
                        else
                        {
                            throw new ArgumentException(input);
                        }
                    } 
                    else
                    {
                        if (input.ToUpper() == "M" || input.ToUpper() == "A" || input.ToUpper() == "P")
                        {
                            return input.ToUpper();
                        }
                        else
                        {
                            throw new ArgumentException(input);
                        }
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Error. {input} is not a valid selection.  Press any key to continue");
                    Console.ReadKey();
                    ClearInput(xPos);
                }
            }
        }

        public static int GetTarget(List<int> connected)
        {
            while (true)
            {
                var xPos = Console.CursorLeft;
                var input = Console.ReadLine();
                try
                {
                    if (int.TryParse(input, out int number))
                    {
                        if (connected.Contains(number))
                        {
                            return number;
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(input);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"\tError: {input} is not a valid selection.  Press any key to continue.");
                    Console.ReadKey();
                    ClearInput(xPos);
                }
            }
        }
    }
}
