using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile exitTileClosed;
    [SerializeField] private Tile exitTileOpen;
    [SerializeField] private Plant plant;

    private bool canMove = true;

    public void Move(Vector2 direction) {
        Vector3Int location = tileMap.WorldToCell(transform.position);
        location = new Vector3Int(location.x + (int) direction.x, location.y + (int) direction.y, location.z);


        if (tileMap.GetTile(location) != wallTile && tileMap.GetTile(location) != exitTileClosed && canMove)
        {
            Vector2 pushPosition = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
            bool hitPlant = plant.GetPlantPositionList().Contains(pushPosition);
            if (hitPlant && plant.Move(direction, pushPosition))
            {
                transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
            }
            else if (!hitPlant)
            {
                transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
            }

            if (tileMap.GetTile(location) == exitTileOpen)
            {
                string sceneName = SceneManager.GetActiveScene().name;
                int level = int.Parse(sceneName.Substring(6)) + 1;
                LevelTransitioner levelTransitioner = FindObjectOfType<LevelTransitioner>();
                if (level < 11)
                {
                    levelTransitioner.LoadLevel("Level " + level.ToString());
                    canMove = false;
                }
                else
                {
                    levelTransitioner.LoadLevel("Winning Screen");
                    canMove = false;
                }
            }
        }
    }
}
