using UnityEngine;

/// <summary>
/// Manages the hex grid. Instantiates all tiles at start.
/// </summary>
public class GridManager : MonoBehaviour
{
    public GameObject hexTilePrefab;
    public int gridWidth = 100;
    public int gridHeight = 100;

    private float hexWidth;
    private float hexHeight;

    void Start()
    {
        // Get sprite size in world units from the prefab's SpriteRenderer
        SpriteRenderer sr = hexTilePrefab.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("HexTile prefab missing SpriteRenderer!");
            return;
        }
        hexWidth = sr.bounds.size.x;
        hexHeight = sr.bounds.size.y;

        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // Offset every other row for pointy-topped hexes
                float xOffset = (y % 2 == 0) ? 0 : hexWidth / 2f;
                Vector3 pos = new Vector3(
                    x * hexWidth + xOffset,
                    y * (hexHeight * 0.75f),
                    0
                );
                Instantiate(hexTilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
