using UnityEngine;
using UnityEngine.AI;

public class ObstacleGenerator : MonoBehaviour
{
    public NavMeshSurface surface;

    public int width = 10;
    public int height = 10;
    public GameObject ground;
    public GameObject wall;
    public GameObject player;

    private bool playerSpawned = false;

    // Use this for initialization
    void Start()
    {
//        spawnObstacle();
        spawnPlayer();
        //Update Navmesh
        surface.BuildNavMesh();
    }

    // Create an obstacle object and place it on the ground platform
    void spawnObstacle()        //TODO: Accept a prameter for position
    {
        Vector3  pos = new Vector3(width / 2f, 1f, height / 2f);     //Place in center of the "ground" platform and raise 1 unit to sit fluch on the ground surface (assuming a 1 unit thick ground platform).
        Instantiate(wall, pos, Quaternion.identity);        // Spawn a wall block prefab

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

