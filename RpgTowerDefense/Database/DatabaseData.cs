using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgTowerDefense
{
    class DatabaseData
    {
        private static DatabaseData DBDATA;
        static public DatabaseData _Instance
        {
            get
            {
                if (DBDATA == null)
                {
                    DBDATA = new DatabaseData();
                }
                return DBDATA;
            }
        }
        
        //Analyse data
        int incomeEachWave, totalIncome, loseWave = 1, totalTowersPlaced;






        public int IncomeEachLevel { get => incomeEachWave; set => incomeEachWave = value; }
        public int TotalIncome { get => totalIncome; set => totalIncome = value; }
        public int LoseLvl { get => loseWave; set => loseWave = value; }
        public int TotalTowersPlaced { get => totalTowersPlaced; set => totalTowersPlaced = value; }






    }
}
