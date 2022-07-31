using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Plant plant;
    [SerializeField] private List<GoalZone> goalList;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile exitTileClosed;
    [SerializeField] private Tile exitTileOpen;
    [SerializeField] private Vector2 exitPosition;

    [SerializeField] private float moveCooldown;

    private float timeLastMoved = -1;

    public void Move(InputAction.CallbackContext context) {
        Vector2 moveDirection = Vector2.zero;
        
        if (context.control.displayName == "W" && context.control.IsPressed())
        {
            moveDirection = Vector2.up;
        }
        else if (context.control.displayName == "D" && context.control.IsPressed())
        {
            moveDirection = Vector2.right;
        }
        else if (context.control.displayName == "A" && context.control.IsPressed())
        {
            moveDirection = Vector2.left;
        }
        else if (context.control.displayName == "S" && context.control.IsPressed())
        {
            moveDirection = Vector2.down;
        }

        if ((float) Time.time - timeLastMoved > moveCooldown) {
            timeLastMoved = (float) Time.time;
            player.Move(moveDirection);
        }

        foreach (GoalZone goal in goalList)
        {
            goal.OnMove(plant);
        }

        if (GoalZone.CanLeave())
        {
            Vector3Int location = tileMap.WorldToCell(new Vector3(exitPosition.x, exitPosition.y, 0));
            tileMap.SetTile(location, exitTileOpen);
        }
        else
        {
            Vector3Int location = tileMap.WorldToCell(new Vector3(exitPosition.x, exitPosition.y, 0));
            tileMap.SetTile(location, exitTileClosed);
        }
    }

    public void Reset(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
