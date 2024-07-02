using Common;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WinLoseDisplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI losesText;

        private void OnEnable()
        {
            levelText.text = PlayerStats.Instance.Wins.ToString();
            losesText.text = PlayerStats.Instance.Losses.ToString();
        }
    }
}
