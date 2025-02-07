using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTower : MonoBehaviour
{
    public GameObject construction;
    public GameObject panel_towerInfo;

    public void SpawnTower()
    {
        if (BuildingSystem.spawned_tower == null)
        {
            if (MoneySystem.CheakMoney(construction.GetComponent<Construction>().price))
            {
                var spawned_tower = Instantiate(construction);
                BuildingSystem.spawned_tower = spawned_tower;
            }
            else
                ErrorSystem.SeeError("Ошибка: недостаточно золота!!!");
        }
        else ErrorSystem.SeeError("Ошибка: поставьте башню!!!");
    }

    public void InfoSee()
    {
        panel_towerInfo.transform.position = new Vector3(transform.position.x + 270, transform.position.y + 130, 0);
        panel_towerInfo.SetActive(true);
        panel_towerInfo.transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text = construction.GetComponent<Construction>().name_;
        panel_towerInfo.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = construction.GetComponent<Construction>().description;
    }
    public void InfoClose() =>
        panel_towerInfo.SetActive(false);
}
