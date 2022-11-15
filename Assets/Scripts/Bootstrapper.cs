using System;
using System.Collections;
using System.Collections.Generic;
using Cell_container;
using Configs;
using Player_container;
using UI;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private CellCreator _cellCreator;
    [SerializeField] private Player _player;
    [SerializeField] private GameContainer _gameContainer;
    private void Start()
    {
        _player.Initialize();
        _gameContainer.Initialize(_player);
        _cellCreator.CreateCells();
    }
}