using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RpgTowerDefense
{
    public class WaveManager
    {
        int waveNumber;
        int mineWaveNumber;

        int monsterAmmount;
        int monstersLeft;
        int mineMonsterAmmount;
        int mineLeft;

        public float speedMod;
        float lastAddedSpeed = 0.055f;
        public float mineSpeedMod;
        float mineLastAddedSpeed = 0.055f;
        float healthMod;
        public int addedHealth;

        float waveCountdown = 5;
        float waveDelay;
        float mineCountdown = 14;
        float mineDelay;

        bool waveInProgress;
        bool mineWaveInProgress;

        public WaveManager()
        { }

        public void Update()
        {
            waveCountdown -= GameWorld._Instance.deltaTime;
            mineCountdown -= GameWorld._Instance.deltaTime;

            if (waveInProgress)
            {
                if (waveDelay <= 0)
                {
                    waveDelay = 0.75f;
                    monstersLeft--;
                    GameWorld._Instance.SpawnMob();
                    if (monstersLeft == 0)
                    {
                        waveInProgress = false;
                        NewWave();
                    }
                }
                else
                {
                    waveDelay -= GameWorld._Instance.deltaTime;
                }

            }
            if (mineWaveInProgress)
            {
                if (mineDelay <= 0)
                {
                    mineDelay = 0.5f;
                    mineLeft--;
                    GameWorld._Instance.SpawnMobMine();
                    if (mineLeft == 0)
                    {
                        mineWaveInProgress = false;
                        MineWave();
                    }
                }
                else
                {
                    mineDelay -= GameWorld._Instance.deltaTime;
                }
            }

            if (waveCountdown <= 0)
            {
                waveInProgress = true;
            }
            if (mineCountdown <= 0)
            {
                mineWaveInProgress = true;
            }
        }

        public void NewWave()
        {
            waveNumber++;
            monsterAmmount += waveNumber + 1;
            healthMod += 0.75f;
            if (healthMod >= addedHealth + 1) { addedHealth++; }
            waveCountdown = 10;
            if (mineSpeedMod < 1.275f)
            {
                mineSpeedMod += mineLastAddedSpeed - 0.005f;
            }
            monstersLeft = monsterAmmount;
        }
        public void MineWave()
        {
            mineWaveNumber++;
            mineMonsterAmmount += mineWaveNumber - 1;
            mineCountdown = 10;
            if (mineSpeedMod < 1.275f)
            {
                mineSpeedMod += mineLastAddedSpeed - 0.005f;
            }
            mineLeft = mineMonsterAmmount;
        }
    }
}
