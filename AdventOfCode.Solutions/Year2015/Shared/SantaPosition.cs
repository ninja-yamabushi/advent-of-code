namespace AdventOfCode.Solutions.Year2015.Shared
{
    public class SantaPosition
    {
        private int x;
        private int y;

        public SantaPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string Position => $"{x},{y}";

        public string Move(char direction)
        {
            if (direction == '<') x--;
            if (direction == '>') x++;
            if (direction == '^') y++;
            if (direction == 'v') y--;

            return Position;
        }
    }
}
