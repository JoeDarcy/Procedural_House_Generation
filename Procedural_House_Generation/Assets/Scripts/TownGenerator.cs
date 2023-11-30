using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TownGenerator : MonoBehaviour
{
    public static int instanceNumber;

    [SerializeField] private bool regenerateTown;

    [SerializeField] private int numberOfRows;
    [SerializeField] private int numberOfColums;
    [SerializeField] private float spacing;
    [SerializeField] private GameObject baseHouse;

    private int numberOfHouses;
    private Vector3 spawnPoint = Vector3.zero;
    private GameObject houseInstance;
    private GameObject houseHolder;
    private int rotationChance;
    private Vector3 rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        houseHolder = new()
        {
            name = "House_Holder"
        };
        numberOfHouses = numberOfColums * numberOfRows;

        spawnPoint.y = baseHouse.transform.position.y;

        GenerateTown();
    }

    private void Update()
    {
        if (regenerateTown)
        {
            GenerateTown();
            regenerateTown = false;
        }
    }

    private void GenerateTown()
    {
        

        for (int x = 0; x < numberOfRows; x++)
        {
            for (int z = 0; z < numberOfColums; z++)
            {
                // Choose rotation
                rotationChance = Random.Range(1, 5);

                switch (rotationChance)
                {
                    case 1:
                        rotationDirection = new Vector3(0, 0, 0);
                        break;
                    case 2:
                        rotationDirection = new Vector3(0, 90, 0);
                        break;
                    case 3:
                        rotationDirection = new Vector3(0, 180, 0);
                        break;
                    case 4:
                        rotationDirection = new Vector3(0, 270, 0);
                        break;
                    default:
                        rotationDirection = new Vector3(0, 0, 0);
                        break;
                }

                // Instantiate house
                houseInstance = Instantiate(baseHouse, new Vector3(transform.position.x + x * spacing, baseHouse.transform.position.y, transform.position.z + z * spacing), Quaternion.Euler(rotationDirection));
                houseInstance.transform.parent = houseHolder.transform;
                instanceNumber++;

                // Instantiate roads
                //roadInstance = Instantiate(roadSection, new Vector3(), rotation);
                //roadInstance.transform.parent = roadHolder.transform;
                // Save house as a prefab for later use by artists (creates perminant copy)
                //PrefabUtility.SaveAsPrefabAsset(houseInstance, "Assets/Prefabs/Houses/houseInstance_ " + instanceNumber + ".prefab");
            }
        }
    }
}
