using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile exitTile;

    private ArrayList plantTileList = new ArrayList();

    public bool Move(Vector2 direction, Vector2 pushPosition) 
    {
        ArrayList plantPositionList = GetPlantPositionList();
        bool willMove = true;

        foreach (Vector2 position in plantPositionList)
        {
            Vector3Int location = tileMap.WorldToCell(position);
            location = new Vector3Int(location.x + (int)direction.x, location.y + (int)direction.y, location.z);
            if (tileMap.GetTile(location) == wallTile || tileMap.GetTile(location) == exitTile)
            {
                willMove = false;
            }
        }

        for (int i = 0; i < plantPositionList.Count; i++)
        {
            Vector3Int location = tileMap.WorldToCell(new Vector3(pushPosition.x + direction.x * 2, pushPosition.y + direction.y * 2, 0));
            bool hittingWall = tileMap.GetTile(location) == wallTile || tileMap.GetTile(location) == exitTile;


            location = tileMap.WorldToCell(new Vector3(direction.x + pushPosition.x, direction.y + pushPosition.y, 0));
            bool growingIntoWall = tileMap.GetTile(location) == wallTile || tileMap.GetTile(location) == exitTile;

            if ((Vector2) plantPositionList[i] == pushPosition && !plantPositionList.Contains(direction + pushPosition) && !hittingWall && !growingIntoWall)
            {
                GrowPlant(new Vector3(direction.x + pushPosition.x, direction.y + pushPosition.y, 0), direction);
                break;
            }
            else if ((Vector2)plantPositionList[i] == pushPosition && !plantPositionList.Contains(direction + pushPosition) && !willMove && !growingIntoWall)
            {
                GrowPlant(new Vector3(direction.x + pushPosition.x, direction.y + pushPosition.y, 0), direction);
                break;
            }
            else if ((Vector2) plantPositionList[i] == pushPosition)
            {
                pushPosition = new Vector2(pushPosition.x + direction.x, pushPosition.y + direction.y);
                i = -1;
            }
        }

        if (willMove)
        {
            transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        }

        return willMove;
    }

    public void GrowPlant(Vector3 position, Vector2 direction)
    {
        GameObject plantTile = Instantiate(plantPrefab, position, Quaternion.identity, transform);
        plantTile.GetComponent<PlantTile>().SetParent(this);
        if (direction == Vector2.right)
        {
            plantTile.transform.Rotate(0, 0, 270);
        }
        else if (direction == Vector2.down)
        {
            plantTile.transform.Rotate(0, 0, 180);
        }
        else if (direction == Vector2.left)
        {
            plantTile.transform.Rotate(0, 0, 90);
        }
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
