using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class RunMoney : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        
        public int Money { get; private set; }

        private void Start()
        {
            SetText();
        }

        public void IncRunMoney()
        {
            Money += 10;
            SetText();
        }
        
        public void DecrRunMoney(int amount)
        {
            if (amount > Money) return;
            Money -= amount;
            SetText();
        }

        private void SetText()
        {
            moneyText.text = $"{Money} $";
        }
    }
}