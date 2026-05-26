using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //to data class
    [Header("Grid Dimensions (In Pixels)")]
    [SerializeField] private int _tileWidth = 32; 
    [SerializeField] private int _tileHeight = 32; 

    //to data class
    [Header("Grid Size (In Tiles)")]
    [SerializeField] private int _tileWidthCount = 10;  
    [SerializeField] private int _tileHeightCount = 10; 
    
    [Header("References")]
    [SerializeField] private GameObject tilePrefab;      
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
                GameObject newTile = Instantiate(tilePrefab, gridParent);
                newTile.gameObject.SetActive(true);
                newTile.name = $"Tile_{col}_{row}";

                RectTransform tileRect = newTile.GetComponent<RectTransform>();
                
                tileRect.sizeDelta = new Vector2(_tileWidth, _tileHeight);

                tileRect.anchorMin = new Vector2(0.2f, 1);
                tileRect.anchorMax = new Vector2(0.2f, 1);
                tileRect.pivot = new Vector2(1, 1);
                
                float posX = row * _tileWidth;
                float posY = -col * _tileHeight; 
                
                TileController tileController = newTile.GetComponent<TileController>();

                TileData tileData = TileDataProvider.CreateTileData(col * row, TileType.Empty, col, row);

                tileController.TileImage.color = tileData.Color;

                tileRect.anchoredPosition = new Vector2(posX, posY);
            }
        }
    }
}