using System;
using Configs;
using Plants;
using UI;
using UnityEngine;

namespace Cell_container
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private HarvestContainer _harvestContainer;
        [SerializeField] private PlantMaturationProcessContainer plantMaturationProcessContainer;
        
        private PlantConfig _plantConfig;
        private BasePlant _activePlant;
        
        public event Action<BasePlant, Cell> ChoseAPlant;
        
        public bool IsFree { get; private set; }
        public bool IsRipened { get; private set; }
        public BasePlant Plant { get; private set; }
        public PlantConfig PlantConfig => _plantConfig;
        
        public void Initialize()
        {
            IsFree = true;
            _harvestContainer.PlantedAPlant += SelectedAPlant;
        }

        private void SelectedAPlant(PlantConfig plantConfig)
        {
            _plantConfig = plantConfig;
            Plant = plantConfig.Plant;
            IsFree = false;
            ChoseAPlant?.Invoke(Plant, this);
        }

        public void AddPlant(BasePlant plant) => 
            _activePlant = plant;

        public void HarvestSelection()
        {
            _harvestContainer.gameObject.SetActive(true);
            plantMaturationProcessContainer.gameObject.SetActive(false);
            _harvestContainer.Initialize();
        }

        public void ShowDisplayPlantProcess()
        {
            plantMaturationProcessContainer.gameObject.SetActive(true);
            plantMaturationProcessContainer.Initialize(_plantConfig);
            plantMaturationProcessContainer.PlantRipened += PlantRipened;
            plantMaturationProcessContainer.StartTimer();
        }

        private void PlantRipened() => 
            IsRipened = true;

        public void DeactivatePanel() => 
            plantMaturationProcessContainer.gameObject.SetActive(false);

        public void MakePlace()
        {
            IsFree = true;
            IsRipened = false;
            _plantConfig = null;
            Plant = null;
            
            if (_activePlant)
            {
                Destroy(_activePlant.gameObject);
                _activePlant = null;
            }
        }
    }
}