using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace RpgTowerDefense
{
    class Database
    {
        SQLiteConnection sqlite2 = new SQLiteConnection("Data Source=C:\\TowerDefence\\TowerDefence.db");

        public void ReadFromDatabase()
        {
            

        }

        public void SaveGame()
        {
            string AddDatabase = "CREATE TABLE IF NOT EXISTS account (rowID INT, user VARCHAR(20), pass VARCHAR(20))";




            SQLiteCommand command = new SQLiteCommand(AddDatabase, sqlite2);
            command.ExecuteNonQuery();
        }

        public void HighScore(string user, int Score)
        {
            string AddDatabase = "CREATE TABLE IF NOT EXISTS HighScore" +
                "('ID' INTEGER PRIMARY KEY AUTOINCREMENT," +
                "'"+user+"' VARCHAR(40)," +
                "'"+Score+"' INTEGER," +
                "'DateTime' DATETIME NOT NULL DEFAULT (datetime(CURRENT_TIMESTAMP, 'localtime')))";
            

            SQLiteCommand command = new SQLiteCommand(AddDatabase, sqlite2);
            command.ExecuteNonQuery();
        }

        public void Analyse(int indkomstPrLevel, int totalInkomst, int loseLvl, int totalTårnePlaceret)
        {
            string AddDatabase = "CREATE TABLE IF NOT EXISTS Analyse" +
                "('ID' INTEGER PRIMARY KEY AUTOINCREMENT," +
                "'" + indkomstPrLevel + "' INTEGER," +
                "'" + totalInkomst + "' INTEGER," +
                "'" + loseLvl + "' INTEGER," +
                "'" + totalTårnePlaceret + "' INTEGER)";




            SQLiteCommand command = new SQLiteCommand(AddDatabase, sqlite2);
            command.ExecuteNonQuery();
        }

        //public void Items()
        //{
        //    string AddDatabase = "CREATE TABLE IF NOT EXISTS HighScore" +
        //        "('ID' INTEGER PRIMARY KEY AUTOINCREMENT," +
        //        "'" + user + "' VARCHAR(40)," +
        //        "'" + Score + "' INTEGER)," +
        //        "'DateTime' DATETIME NOT NULL DEFAULT (datetime(CURRENT_TIMESTAMP, 'localtime'))";

            

        //    SQLiteCommand command = new SQLiteCommand(AddDatabase, sqlite2);
        //    command.ExecuteNonQuery();
        //}
    }
}
