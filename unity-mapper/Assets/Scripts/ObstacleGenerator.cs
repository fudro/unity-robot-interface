using UnityEngine;
using UnityEngine.AI;

public class ObstacleGenerator : MonoBehaviour
{
    public NavMeshSurface surface;

    public int width = 10;
    public int height = 10;

    public GameObject wall;
    public GameObject player;

    private bool playerSpawned = false;

    // Use this for initialization
    void Start()
    {
//        setObstacle();
        spawnPlayer();
        //Update Navmesh
        surface.BuildNavMesh();
    }

    // Create a grid based level
    void setObstacle()
    {
        // Spawn a wall
        Vector3 pos = new Vector3(width / 2f, 1f, height / 2f);
        Instantiate(wall, pos, Quaternion.identity);
    }

    void spawnPlayer()
    {
        if (!playerSpawned) // Should we spawn a player?
        {
            // Spawn the player
            Vector3 pos = new Vector3(width / 2f, 1.25f, height / 2f);
            Instantiate(player, pos, Quaternion.identity);
            playerSpawned = true;
        }
    }
}

