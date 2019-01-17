using System;

namespace Buzz
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BuzzWorld())
                game.Run();
        }
    }
}
