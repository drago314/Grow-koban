using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTile : MonoBehaviour
{
    [SerializeField] private PlantParent parent;
    
    private void Start()
    {
        parent.AddPlantTile(this);
    }
}
