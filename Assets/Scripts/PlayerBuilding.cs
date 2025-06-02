using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the player's building: shooting projectiles at the closest neutral/enemy tile.
/// </summary>
public class PlayerBuilding : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireInterval = 1.0f; // Seconds between shots
    public float projectileSpeed = 10f;

    void Start()
    {
        StartCoroutine(AutoShootRoutine());
    }

    /// <summary>
    /// Finds the closest neutral or enemy tile and fires at it.
    /// </summary>
    IEnumerator AutoShootRoutine()
    {
        while (true)
        {
            HexTile target = FindClosestTarget();
            if (target != null)
            {
                ShootAt(target);
            }
            yield return new WaitForSeconds(fireInterval);
        }
    }

    HexTile FindClosestTarget()
    {
        HexTile[] tiles = FindObjectsOfType<HexTile>();
        HexTile closest = null;
        float minDist = float.MaxValue;
        foreach (var tile in tiles)
        {
            if (tile.Owner != HexTile.TileOwner.Player)
            {
                float dist = Vector3.Distance(transform.position, tile.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = tile;
                }
            }
        }
        return closest;
    }

    void ShootAt(HexTile target)
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.Init(target.transform.position, HexTile.TileOwner.Player);
        projectile.Speed = projectileSpeed;
    }
}
