using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEnemys : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount == 0)
        {
            EnemySystem.count_enemys -= 1;
            Destroy(gameObject);
        }
    }
}
