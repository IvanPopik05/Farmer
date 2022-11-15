using System;
using System.Collections.Generic;
using Configs;
using UnityEditor;
using UnityEngine;

namespace Cell_container
{
    public class CellCreator : MonoBehaviour
    {
        [SerializeField] private CellConfig _cellConfig;
        [SerializeField] private Cell _cellPrefab;
    
        private List<Cell> _cells = new List<Cell>();

        public void CreateCells()
        {
            for (int x = 0; x < _cellConfig.Width; x++)
            {
                for (int z = 0; z < _cellConfig.Length; z++)
                {
                    CreateCell(x, z);
                }
            }
        }

        private void CreateCell(int x, int z)
        {
            Cell newCell = Instantiate(_cellPrefab,
                new Vector3(x + transform.position.x,transform.position.y,z + transform.position.z),
                Quaternion.identity, transform);
            newCell.Initialize();
            _cells.Add(newCell);
        }
        
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            for (int x = 0; x < _cellConfig.Width; x++)
            {
                for (int z = 0; z < _cellConfig.Length; z++)
                {
                    Gizmos.color = Color.white;
                    Handles.DrawWireCube(new Vector3(x + transform.position.x,transform.position.y,
                        z + transform.position.z), Vector3.one);
                }
            }
        } 
#endif
    }
}