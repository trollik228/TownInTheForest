using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForSpawn : MonoBehaviour
{
    public static List<GameObject> towers = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TowerDefense") || other.CompareTag("Mine"))
        {
            BuildingSystem.request_spawn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TowerDefense") || other.CompareTag("Mine"))
        {
            BuildingSystem.request_spawn = false;
        }
    }
}
