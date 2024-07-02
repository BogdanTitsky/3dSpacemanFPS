using System;
using Common;
using Enemy;
using TMPro;
using UI;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private Health[] enemiesHealth;
        [SerializeField] private WinLosePopUpDisplay winLosePopup;
        [SerializeField] private RunMoney runMoney;
        [SerializeField] private TextMeshProUGUI timerText;
        
        public int EnemiesKilled { get; private set; }
        private float survivalTime = 100f;
        
        public event Action OnEnemyKilled;

        private void OnEnable()
        {
            playerHealth.OnDeath += GameLose;
            foreach (var enemyHealth in enemiesHealth)
            {
                enemyHealth.OnDeath += OnEnemyDeath;
            }
        }
        
        private void OnDisable()
        {
            playerHealth.OnDeath -= GameLose;
            foreach (var enemyHealth in enemiesHealth)
            {
                enemyHealth.OnDeath -= OnEnemyDeath;
            }
        }
        
        private void Update()
        {
            // Decrease the remaining time
            survivalTime -= Time.deltaTime;

            // Check if time has run out
            if (survivalTime <= 0)
            {
                survivalTime = 50;
                GameWin();
            }

            // Update the timer UI
            UpdateTimerUI();
        }
        
        private void UpdateTimerUI()
        {
            int minutes = Mathf.FloorToInt(survivalTime / 60);
            int seconds = Mathf.FloorToInt(survivalTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
        private void OnEnemyDeath(Vector3 position)
        {
            EnemiesKilled++;
            runMoney.IncRunMoney();
            OnEnemyKilled?.Invoke();
            // if (enemiesKilled >= enemiesHealth.Length)
            // {
            //     GameWin();
            // }
        }
        
        private void GameLose(Vector3 position)
        {
            winLosePopup.ShowPopup("You lose");
            PlayerStats.Instance.IncrementLosses();
            RecordKills();
        }
        
        
        private void GameWin()
        {
            winLosePopup.ShowPopup("You win");
            PlayerStats.Instance.IncrementWins(); 
            RecordKills();
        }

        private void RecordKills()
        {
            int currentRecord = PlayerStats.Instance.RecordKills;
            if (EnemiesKilled > currentRecord)
                PlayerStats.Instance.SetRecordKills(EnemiesKilled);
        }
    }
}