using System;

namespace RpgTowerDefense
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
       {
            using (var game = GameWorld._Instance)
                game.Run();
        }
    }
#endif
}
