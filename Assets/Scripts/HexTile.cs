using UnityEngine;

/// <summary>
/// Represents a single hex tile in the grid. Handles ownership, capturing, and visuals.
/// </summary>
public class HexTile : MonoBehaviour
{
    public enum TileOwner { Neutral, Player, Enemy }
    public TileOwner Owner = TileOwner.Neutral;

    private SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    /// <summary>
    /// Captures this tile for the specified owner.
    /// </summary>
    /// <param name="newOwner">The new owner of the tile.</param>
    public void Capture(TileOwner newOwner)
    {
        if (Owner != newOwner)
        {
            Debug.Log($"Tile captured! Old owner: {Owner}, New owner: {newOwner}");
            Owner = newOwner;
            UpdateColor();
            // TODO: Add event call or effect if needed
        }
    }

    /// <summary>
    /// Updates the color of the tile based on ownership.
    /// </summary>
    void UpdateColor()
    {
        if (_renderer == null)
        {
            Debug.LogWarning("No SpriteRenderer found on HexTile!");
            return;
        }

        switch (Owner)
        {
            case TileOwner.Neutral:
                _renderer.color = Color.gray;
                break;
            case TileOwner.Player:
                _renderer.color = Color.cyan;
                break;
            case TileOwner.Enemy:
                _renderer.color = Color.red;
                break;
        }
    }
}
