using Plants;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Plant config",menuName = "Configs/Plant",order = 1)]
    public class PlantConfig : ScriptableObject
    {
        private const float MeasureTime = 60f;
        
        [SerializeField] private BasePlant _plant;
        [SerializeField,Range(0.1f,90f)] private float _ripeningTimerInMinutes;

        public BasePlant Plant => _plant;
        public float RipeningTimerInMinutes => _ripeningTimerInMinutes * MeasureTime;
        public int Experience => (int)RipeningTimerInMinutes * 2;
    }
}