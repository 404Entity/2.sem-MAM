using System;
using System.Data.SQLite;


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
            if (!System.IO.File.Exists("C:\\TowerDefence\\TowerDefence.db"))
            {
                System.IO.Directory.CreateDirectory("C:\\TowerDefence");
                SQLiteConnection.CreateFile("C:\\TowerDefence\\TowerDefence.db");
            }
            Database._Instance.CreateTables();

            using (var game = GameWorld._Instance)
                game.Run();
        }
    }
#endif
}
