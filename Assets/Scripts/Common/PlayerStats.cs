using UnityEngine;

namespace Common
{
    public class PlayerStats : MonoBehaviour
    {
        private static PlayerStats _instance;

        private const string WinsKey = "Wins";
        private const string LossesKey = "Losses";
        private const string RecordKillsKey = "RecordKills";

        public static PlayerStats Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("PlayerStats");
                    _instance = go.AddComponent<PlayerStats>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        public int Wins
        {
            get => PlayerPrefs.GetInt(WinsKey, 0);
            private set => PlayerPrefs.SetInt(WinsKey, value);
        }

        public int Losses
        {
            get => PlayerPrefs.GetInt(LossesKey, 0);
            private set => PlayerPrefs.SetInt(LossesKey, value);
        }
    
        public int RecordKills
        {
            get => PlayerPrefs.GetInt(RecordKillsKey, 0);
            private set => PlayerPrefs.SetInt(RecordKillsKey, value);
        }

        public void IncrementWins()
        {
            Wins++;
            PlayerPrefs.Save();
        }

        public void IncrementLosses()
        {
            Losses++;
            PlayerPrefs.Save();
        }
        public void SetRecordKills(int newRecord)
        {
            RecordKills = newRecord;
            PlayerPrefs.Save();
        }

        public void ResetStats()
        {
            PlayerPrefs.DeleteKey(WinsKey);
            PlayerPrefs.DeleteKey(LossesKey);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }
}