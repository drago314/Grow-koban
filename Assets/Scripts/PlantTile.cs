using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTile : MonoBehaviour
{
    [SerializeField] private Plant plant;
    
    private void Start()
    {
        if (plant != null)
        {
            plant.AddPlantTile(this);
        }
    }
    
    public void SetParent(Plant _plant)
    {
        plant = _plant;
        plant.AddPlantTile(this);
    }
}


