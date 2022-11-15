using System;
using Player_container;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountOfExperienceText;
        [SerializeField] private TextMeshProUGUI _amountOfCarrotText;

        private Player _player;
        public void Initialize(Player player)
        {
            _player = player;
            
            _player.GotAmountOfCarrot += GotAmountOfCarrot;
            _player.GotAmountOfExperience += GotAmountOfExperience;
        }

        private void GotAmountOfExperience(int amountOfExperience) => 
            _amountOfExperienceText.text = $"{amountOfExperience}";

        private void GotAmountOfCarrot(int amountOfCarrot) => 
            _amountOfCarrotText.text = $"{amountOfCarrot}";

        private void OnDisable()
        {
            _player.GotAmountOfCarrot -= GotAmountOfCarrot;
            _player.GotAmountOfExperience -= GotAmountOfExperience;
        }
    }
}