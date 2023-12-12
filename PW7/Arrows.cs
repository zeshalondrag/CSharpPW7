namespace ConsoleApp10
{
    internal class LightArrows
    {
        public static int[] Arrows(ConsoleKeyInfo key, int[] pos)
        {
            int prevPosition = 2;

            if ((key.Key == ConsoleKey.UpArrow) || (key.Key == ConsoleKey.W))
            {
                if (pos[0] <= pos[2])
                {
                    pos[0] = pos[2];
                    prevPosition = pos[2];
                }

                else
                {
                    prevPosition = pos[0];
                    pos[0]--;
                }
            }

            else if ((key.Key == ConsoleKey.DownArrow) || (key.Key == ConsoleKey.S))
            {
                if (pos[0] >= pos[1])
                {
                    pos[0] = pos[1];
                    prevPosition = pos[1];
                }

                else
                {
                    prevPosition = pos[0];
                    pos[0]++;
                }
            }

            Console.SetCursorPosition(0, prevPosition);
            Console.Write("  ");
            Console.SetCursorPosition(0, pos[0]);
            Console.Write("->");

            return pos;
        }
    }
}
