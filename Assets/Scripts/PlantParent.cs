using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantParent : MonoBehaviour
{
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile wallTile;

    private ArrayList plantTileList = new ArrayList();

    public bool Move(Vector2 direction) 
    {
        foreach (Vector2 position in GetPlantPositionList())
        {
            Vector3Int location = tileMap.WorldToCell(position);
            location = new Vector3Int(location.x + (int)direction.x, location.y + (int)direction.y, location.z);
            if (tileMap.GetTile(location) == wallTile)
            {
                return false;
            }
        }

        transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        return true;
    }

    public void AddPlantTile(PlantTile plant)
    {
        plantTileList.Add(plant);
    }

    public ArrayList GetPlantPositionList()
    {
        ArrayList positionList = new ArrayList();
        foreach (PlantTile plant in plantTileList)
        {
            positionList.Add(new Vector2(plant.transform.position.x, plant.transform.position.y));
        }
        return positionList;
    }
}
