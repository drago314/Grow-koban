using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Plant plant;
    [SerializeField] private List<GoalZone> goalList;

    [SerializeField] private float moveCooldown;

    private float timeLastMoved = -1;

    // Start is called before the first frame update
    public void Move(InputAction.CallbackContext context) {
        Vector2 inputDirection = context.ReadValue<Vector2>();
        Vector2 moveDirection = Vector2.zero;
        if (Mathf.Abs(inputDirection.x) > 0.5) {
            moveDirection.x = Mathf.Sign(inputDirection.x);
        }
        else if (Mathf.Abs(inputDirection.y) > 0.5)
        {
            moveDirection.y = Mathf.Sign(inputDirection.y);
        }

        if ((float) Time.time - timeLastMoved > moveCooldown) {
            timeLastMoved = (float) Time.time;
            player.Move(moveDirection);
        }

        foreach (GoalZone goal in goalList)
        {
            goal.OnMove(plant);
        }
    }
}
