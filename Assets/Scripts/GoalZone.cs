using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public static int totalGoals = 0;
    public static List<GoalZone> coveredGoals = new List<GoalZone>();

    public static bool CanLeave()
    {
        return totalGoals == coveredGoals.Count;
    }

    private void Awake()
    {
        totalGoals = 0;
    }

    private void Start()
    {
        totalGoals += 1;   
    }

    public void OnMove(Plant plant)
    {
        if (plant.GetPlantPositionList().Contains(new Vector2(transform.position.x, transform.position.y)))
        {
            if(!coveredGoals.Contains(this))
            {
                coveredGoals.Add(this);
            }
        }
        else
        {
            if(coveredGoals.Contains(this))
            {
                coveredGoals.Remove(this);
            }
        }
    }
}
