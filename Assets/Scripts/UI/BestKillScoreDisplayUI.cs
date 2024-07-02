using TMPro;
using UnityEngine;

namespace UI
{
    public class BestKillScoreDisplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyKilledText;

        private void Start()
        {
            SetBestKillScore();
        }

        private void SetBestKillScore()
        {
            enemyKilledText.text = PlayerPrefs.GetInt("RecordKills", 0).ToString();
        }
    }
}
