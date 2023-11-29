using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralHouseGenerator : MonoBehaviour
{
    [SerializeField] private float totalHouse;
    [SerializeField] private GameObject groundPlane;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject[] roofs;
    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject[] windows;
    [SerializeField] private GameObject[] details;

    private Vector3 startPosition;

    // Current room instance and renderer for reference by other components during build process.
    // Can be updated as each room is complete and the next room becomes the main room
    private int roomChoice;
    private float roomScale;
    private GameObject currentRoomInstance;
    private Renderer currentRoomInstanceRenderer;
    private float currentRoomSpawnPointOffsetX;
    private Vector3 currentRoomPositionInWorldSpace;

    // Current component instance and renderer for placement within / on current room instance
    private int roofChoice;
    private GameObject componentInstance;
    private Renderer componentInstanceRenderer;
    private Vector3 spawnPoint;
    private float spawnPointOffsetX;
    private float spawnPointOffsetY;
    private float spawnPointOffsetZ;


    // Start is called before the first frame update
    void Start()
    {
        // Get the start position of the generator
        startPosition = transform.position;


        for (int i = 0; i < totalHouse; i++)
        {
            // Spawn main room
            roomChoice = Random.Range(0, rooms.Length);
            currentRoomInstance = Instantiate(rooms[roomChoice]);

            // Rescale main room
            if (roomChoice == 0)
            {
                roomScale = Random.Range(1, 5);
                currentRoomInstance.transform.localScale = new Vector3(roomScale, roomScale, roomScale);
            }
            else if (roomChoice == 1)
            {
                roomScale = Random.Range(1, 5);
                currentRoomInstance.transform.localScale = new Vector3(roomScale, roomScale * 2, roomScale);
            }

            // Get the renderer for the house component, to get bounds, for setting spawn point
            currentRoomInstanceRenderer = currentRoomInstance.GetComponent<Renderer>();
            spawnPointOffsetY = currentRoomInstanceRenderer.bounds.center.y + currentRoomInstanceRenderer.bounds.extents.y;
            currentRoomSpawnPointOffsetX += currentRoomInstance.transform.position.x + currentRoomInstanceRenderer.bounds.extents.x * 3.5f;
            spawnPoint = new Vector3(startPosition.x + rooms[roomChoice].transform.position.x + currentRoomSpawnPointOffsetX, startPosition.y + rooms[roomChoice].transform.position.y + spawnPointOffsetY, startPosition.z + rooms[roomChoice].transform.position.z);
            currentRoomInstance.transform.position = spawnPoint;

            // Spawn components

            // Roof
            componentInstance = Instantiate(roofs[0]);
            componentInstance.transform.localScale = new Vector3(roomScale, roomScale, roomScale);
            // Get the renderer for the house component, to get bounds, for setting spawn point
            componentInstanceRenderer = componentInstance.GetComponent<Renderer>();
            // Add the current room's height to the spawn point offset
            spawnPointOffsetY = currentRoomInstanceRenderer.bounds.center.y + currentRoomInstanceRenderer.bounds.extents.y + componentInstanceRenderer.bounds.extents.y;
            spawnPoint = new Vector3(currentRoomInstance.transform.position.x, startPosition.y + roofs[0].transform.position.y + spawnPointOffsetY, startPosition.z + roofs[0].transform.position.z);
            componentInstance.transform.position = spawnPoint;

            // Door
            componentInstance = Instantiate(doors[0]);
            componentInstance.transform.localScale = new Vector3(roomScale, roomScale, roomScale);
            // Get the renderer for the house component, to get bounds, for setting spawn point
            componentInstanceRenderer = componentInstance.GetComponent<Renderer>();
            // Add the current room's height to the spawn point offset
            spawnPointOffsetY = componentInstanceRenderer.bounds.extents.y;
            spawnPointOffsetZ = currentRoomInstanceRenderer.bounds.extents.z;
            spawnPoint = new Vector3(currentRoomInstance.transform.position.x, doors[0].transform.position.y + spawnPointOffsetY, startPosition.z + doors[0].transform.position.z + spawnPointOffsetZ);
            componentInstance.transform.position = spawnPoint;

            // Windows
            componentInstance = Instantiate(windows[0]);
            componentInstance.transform.localScale = new Vector3(roomScale, roomScale, roomScale);
            // Get the renderer for the house component, to get bounds, for setting spawn point
            componentInstanceRenderer = componentInstance.transform.Find("Window").GetComponentInChildren<Renderer>();
            // Add the current room's height to the spawn point offset
            spawnPointOffsetX = currentRoomInstanceRenderer.bounds.center.x;
            spawnPointOffsetY = currentRoomInstanceRenderer.bounds.center.y + currentRoomInstanceRenderer.bounds.extents.y * 0.8f;
            spawnPointOffsetZ = currentRoomInstanceRenderer.bounds.extents.z * 0.8f;
            spawnPoint = new Vector3(spawnPointOffsetX, windows[0].transform.position.y + spawnPointOffsetY, startPosition.z + windows[0].transform.position.z + spawnPointOffsetZ);
            componentInstance.transform.position = spawnPoint;

            Debug.Log(currentRoomInstanceRenderer.bounds.center
                        );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
