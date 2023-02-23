using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tile tilePrefab;
    public uint maxRow;
    public uint maxColumn;
    private Grid gridData;
    public Dictionary<uint[], TileData> mapTiles = new Dictionary<uint[], TileData>();

    private void Awake()
    {
        gridData = GetComponent<Grid>();
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3 startPosition = new Vector3(maxColumn * (gridData.cellSize.x + gridData.cellGap.x) / 2, 0, maxRow * (gridData.cellSize.z + gridData.cellGap.z) / 2);
        float x = startPosition.x;
        float y = startPosition.z;

        for (uint row = maxRow; row > 0; row--)
        {
            for(uint column = 0; column < maxColumn; column++)
            {
                var tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
                tile.transform.localScale = gridData.cellSize;
                x -= 1 * (gridData.cellSize.x + gridData.cellGap.x);
                tile.Initialize(this, row, column);
                tile.name = "Tile - (" + row.ToString() + " - " + column.ToString() + ")";
                uint[] newKeyMap = { row, column };
                mapTiles[newKeyMap] = tile.data;
            }
            x = startPosition.x;
            y -= 1 * (gridData.cellSize.z + gridData.cellGap.z);
        }
    }

    /*protected void CreateGrid()
    {
        Vector3 startPosition = new Vector3(gm.GameState.Columns * (grid.cellSize.x + grid.cellGap.x) / 2, 0, gm.GameState.Rows * (grid.cellSize.z + grid.cellGap.z) / 2);
        float x = startPosition.x;
        float y = startPosition.z;
        for (int row = 0; row < gm.GameState.Rows; row++)
        {
            for (int column = gm.GameState.Columns - 1; column >= 0; column--)
            {
                var tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                tile.transform.SetParent(this.transform);
                tile.transform.localScale = grid.cellSize;
                x -= 1 * (grid.cellSize.x + grid.cellGap.x);
                tile.Setup(gm, row, column, TileType.empty, false);
                mapTiles[new Vector2Int(row, column)] = tile;
            }
            x = startPosition.x;
            y -= 1 * (grid.cellSize.z + grid.cellGap.z);
        }
    }*/
}
