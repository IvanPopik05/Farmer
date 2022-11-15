using System;
using System.Collections;
using Cell_container;
using UnityEngine;

namespace Plants
{
    public class BasePlant : MonoBehaviour
    {
        [SerializeField] private GameObject[] _stagesGrass;

        private bool _isInitialized;
        private int _index = 0;

        public virtual void Initialize(Cell cell)
        {
            _isInitialized = true;
            StartCoroutine(PlantGrowthCoroutine(cell.PlantConfig.RipeningTimerInMinutes));
        }

        private IEnumerator PlantGrowthCoroutine(float ripeningTimer)
        {
            float partTime = ripeningTimer / _stagesGrass.Length;
            while (_index < _stagesGrass.Length - 1)
            {
                yield return new WaitForSeconds(partTime);
                if (_stagesGrass[_index].activeSelf)
                {
                    _stagesGrass[_index].SetActive(false);
                    _stagesGrass[++_index].SetActive(true);
                }
            }
        }
    }
}