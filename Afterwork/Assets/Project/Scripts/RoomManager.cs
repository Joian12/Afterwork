using System;
using UnityEngine;

//USE MVVM PLEASE
public class RoomManager : MonoBehaviour
{
    [SerializeField] private GridTileInteraction _gridTileInteraction;
    
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gridTileInteraction.GetLatestTilePosition();
        }
    }

    private void GetSelectedTileData()
    {
        
    }
}

