using System;
using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace UI
{
    public class HarvestContainer : MonoBehaviour
    {
        [SerializeField] private List<PlantCellButton> _plantCells;

        public event Action<PlantConfig> PlantedAPlant;
        
        public void Initialize()
        {
            foreach (PlantCellButton plantCell in _plantCells)
            {
                plantCell.PlantedAPlant += PlantedAPlant;
                plantCell.Initalize(this);
            }
        }

        public void Hide() => 
            gameObject.SetActive(false);

        private void OnDisable()
        {
            foreach (PlantCellButton plantCell in _plantCells) 
                plantCell.PlantedAPlant -= PlantedAPlant;
        }
    }
}