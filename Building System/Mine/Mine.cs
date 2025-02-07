using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Construction
{

    private bool is_startCor_for_mine = false;

    protected override void ConstructLogic()
    {
        if (!is_startCor_for_mine)
        {
            StartCoroutine(StartMine());
        }
    }

    private IEnumerator StartMine()
    {
        is_startCor_for_mine = true;
        yield return new WaitForSeconds(10);
        MoneySystem.GetMoney(20);
        is_startCor_for_mine = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet enemy"))
        {
            Damage(other.GetComponent<Bullet_Ghost>().damage);
            Destroy(other.gameObject);
        }
    }
}
