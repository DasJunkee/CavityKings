using UnityEngine;

/// <summary>
/// Represents a projectile fired by a building. Handles movement and collision with hex tiles.
/// </summary>
public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public HexTile.TileOwner Owner; // Set this before firing!
    private Vector3 _targetPosition;

    /// <summary>
    /// Initializes the projectile's movement.
    /// </summary>
    /// <param name="target">Where to move towards.</param>
    public void Init(Vector3 target, HexTile.TileOwner owner)
    {
        _targetPosition = target;
        Owner = owner;
    }

    void Update()
    {
        // Move towards target
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

        // Destroy if reached
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HexTile tile = other.GetComponent<HexTile>();
        if (tile != null && tile.Owner != Owner)
        {
            Debug.Log("Projectile hit a tile!");
            tile.Capture(Owner);
            Destroy(gameObject);
        }
    }
}
