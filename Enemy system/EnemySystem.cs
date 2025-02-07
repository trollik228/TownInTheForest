using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyDay
{
    public int count_squads;
    public GameObject enemy_type;
}

[Serializable]
public class DayWaves
{
    public List<EnemyDay> enemys;
}
public class EnemySystem : MonoBehaviour
{
    public static Transform spawn_point;
    public static bool is_spawn = false;
    public static int count_enemys = -1;


    public Transform town;
    public List<DayWaves> enemys_days;


    [SerializeField] private List<Transform> positions = new();
    [SerializeField] private TMPro.TMP_Text text;


    private void Update()
    {
        if (count_enemys != -1)
            text.text = "Отряды врагов: " + count_enemys.ToString();
        if (TimeSystem.is_startNewWave && !is_spawn)
        {
            StartCoroutine(Spawn());
            TimeSystem.is_startNewWave = false;
        }
    }
    public IEnumerator Spawn()
    {
        is_spawn = true;
        SystemGame.is_start_timer_for_arrow = false;
        SystemGame.see_arrow = true;
        spawn_point = positions[UnityEngine.Random.Range(0, positions.Count)];
        
        for (int j = 0; j < enemys_days[TimeSystem.num_day - 1].enemys.Count; j++)
        {
            yield return new WaitForSeconds(7);

            for (int i = 0; i < spawn_point.childCount; i++)
            {
                if (i < enemys_days[TimeSystem.num_day - 1].enemys[j].count_squads)
                {
                    var tt = Instantiate(enemys_days[TimeSystem.num_day - 1].enemys[j].enemy_type,
                                         spawn_point.GetChild(i).position, Quaternion.identity);

                    tt.transform.LookAt(town.position);
                    tt.transform.SetParent(GameObject.Find("Enemys").transform);

                    if (count_enemys == -1)
                        count_enemys = 1;
                    else
                        count_enemys++;

                }
            }
                
        }
    }
}
