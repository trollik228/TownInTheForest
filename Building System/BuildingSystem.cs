using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static GameObject spawned_tower;
    public static bool request_spawn = false;


    [SerializeField] private GameObject panel_building;
    [SerializeField] private GameObject building_place;


    private void Update()
    {
        if (spawned_tower != null)
        {
            if (!building_place.activeInHierarchy)
            {
                building_place.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0) && request_spawn)
            {
                PlaceForSpawn.towers.RemoveAll(x => x == null);
                if (PlaceForSpawn.towers.Count == 0 ||  PlaceForSpawn.towers.All(x => Vector3.Distance(x.transform.position, spawned_tower.transform.position) > 10))
                {
                    PlaceForSpawn.towers.Add(spawned_tower);
                    spawned_tower.GetComponent<Construction>().is_spawned = true;
                    MoneySystem.GetMoney(-spawned_tower.GetComponent<Construction>().price);
                    spawned_tower = null;
                    request_spawn = false;
                    building_place.SetActive(false);
                    StatisticSystem.towers += 1;
                    return;
                }
                else 
                    ErrorSystem.SeeError("Ошибка: слишком близко к другой постройке!!!");
            }
            else if (Input.GetMouseButtonDown(0) && !request_spawn)
                ErrorSystem.SeeError("Ошибка: нельзя строить за пределами зеленой области!!!");

            if (Input.GetMouseButtonDown(1) && spawned_tower != null)
            {
                Destroy(spawned_tower);
                request_spawn = false;
                building_place.SetActive(false);
            }

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000, ~(1 << 9)))
            {
                spawned_tower.transform.position = new Vector3(hit.point.x,
                                                             hit.point.y + spawned_tower.GetComponent<Construction>().y_ofsset,
                                                             hit.point.z);
            }
        }
    }
}
