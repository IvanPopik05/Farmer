using System;
using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlantCellButton : MonoBehaviour
    {
        [SerializeField] private PlantConfig _plantConfig;
        [SerializeField] private Button _plantButton;
        
        private HarvestContainer _harvestContainer;

        public event Action<PlantConfig> PlantedAPlant;

        public void Initalize(HarvestContainer harvestContainer)
        {
            _harvestContainer = harvestContainer;
            _plantButton.onClick.AddListener(ToPlantAPlant);
        }

        private void ToPlantAPlant()
        {
            PlantedAPlant?.Invoke(_plantConfig);
            _harvestContainer.Hide();
        }

        private void OnDisable() => 
            _plantButton.onClick.RemoveListener(ToPlantAPlant);
    }
}