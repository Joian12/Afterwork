using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [Header("Grid Dimensions (In Pixels)")]
    [SerializeField] private int _tileWidth = 32; 
    [SerializeField] private int _tileHeight = 32; 

    [Header("Grid Size (In Tiles)")]
    [SerializeField] private int _tileWidthCount = 10;  
    [SerializeField] private int _tileHeightCount = 10; 
    
    [Header("References")]
    [SerializeField] private RawImage tilePrefab;      
    [SerializeField] private RectTransform gridParent; 

    private void Awake()
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        for (int col = 0; col < this._tileHeightCount; col++)
        {
            for (int row = 0; row < this._tileWidthCount; row++)
            {
                RawImage newTile = Instantiate(tilePrefab, gridParent);
                newTile.gameObject.SetActive(true);
                newTile.name = $"Tile_{col}_{row}";

                RectTransform tileRect = newTile.rectTransform;
                tileRect.sizeDelta = new Vector2(_tileWidth, _tileHeight);

                tileRect.anchorMin = new Vector2(0, 1);
                tileRect.anchorMax = new Vector2(0, 1);
                tileRect.pivot = new Vector2(0, 1);
                
                float posX = col * _tileWidth;
                float posY = -row * _tileHeight; 

                tileRect.anchoredPosition = new Vector2(posX, posY);
            }
        }
    }
}