using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile wallTile;


    public void Move(Vector2 direction) {
        Vector3Int location = tileMap.WorldToCell(transform.position);
        location = new Vector3Int(location.x + (int) direction.x, location.y + (int) direction.y, location.z);


        if (tileMap.GetTile(location) != wallTile)
        {
            transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        }
    }
}
