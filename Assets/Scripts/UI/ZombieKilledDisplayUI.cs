using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ZombieKilledDisplayUI : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private TextMeshProUGUI enemyKilledText;
    
        private void OnEnable()
        {
            SetNewEnemyKilledScore();
            gameController.OnEnemyKilled += SetNewEnemyKilledScore;
        }
    
        private void OnDisable()
        {
            gameController.OnEnemyKilled -= SetNewEnemyKilledScore;
        }

        private void SetNewEnemyKilledScore()
        {
            enemyKilledText.text = gameController.EnemiesKilled.ToString();
        }
    }
}
