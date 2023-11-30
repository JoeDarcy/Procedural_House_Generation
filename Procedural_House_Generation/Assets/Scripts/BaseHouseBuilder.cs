using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class BaseHouseBuilder : MonoBehaviour
{
    [Range(0,7)]
    [SerializeField] private int numberOfRooms;
    [SerializeField] private bool randomiseRoomNumber;
    [SerializeField] private GameObject roomBase;
    [SerializeField] private Transform[] roomSpawnPoints;

    private int[] roomsBuilt;
    private int roomSpawnPointChoice;
    private GameObject rooms;


    // Start is called before the first frame update
    void Start()
    {
        rooms = new()
        {
            name = "Rooms"
        };
        rooms.transform.parent = transform;
        rooms.AddComponent<UpdatePrefab>();

        if (randomiseRoomNumber)
            numberOfRooms = Random.Range(0, 7);

        for (int i = 0; i < numberOfRooms; i++)
        {
            roomSpawnPointChoice = Random.Range(0, 7);
            Instantiate(roomBase, roomSpawnPoints[roomSpawnPointChoice].position, Quaternion.identity, rooms.transform);
        }
    }
}
