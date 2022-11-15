using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Cell config",menuName = "Configs/Cell",order = 1)]
    public class CellConfig : ScriptableObject
    {
        [SerializeField,Range(1,15)] private int _length;
        [SerializeField,Range(1,15)] private int _width;

        public int Length => _length;
        public int Width => _width;
    }
}