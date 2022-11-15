using System;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlantMaturationProcessContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Image _attentionIcon;
        
        private PlantMaturationProcessTimer _plantMaturationProcessTimer;

        public event Action PlantRipened;

        public void Initialize(PlantConfig plantConfig)
        {
            _plantMaturationProcessTimer = new PlantMaturationProcessTimer(plantConfig.RipeningTimerInMinutes);
            _plantMaturationProcessTimer.TextTimerUpdate += UpdateText;
            _plantMaturationProcessTimer.TimerEnded += ProcessTimerEnded;
        }

        private void ProcessTimerEnded()
        {
            _timerText.gameObject.SetActive(false);
            _attentionIcon.gameObject.SetActive(true);
            PlantRipened?.Invoke();
        }

        private void UpdateText(string text) => 
            _timerText.text = text;

        public void StartTimer()
        {
            _timerText.gameObject.SetActive(true);
            _attentionIcon.gameObject.SetActive(false);
            StartCoroutine(_plantMaturationProcessTimer.StartTimerCoroutine());
        }
    }
}