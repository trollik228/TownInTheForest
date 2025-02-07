using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenceTower : Construction
{

    [SerializeField] private GameObject projectile_prefab;
    [SerializeField] private GameObject projectile_spwn_pos;
    [SerializeField] private List<GameObject> _enemy_list = new();
    [SerializeField] private AudioSource audio_cource;
    [SerializeField] private AudioClip attack;


    private GameObject _cur_enemy;
    private bool is_startCor = false;

    protected override void ConstructLogic()
    {
        if (_enemy_list.Count > 0)
        {
            _enemy_list.RemoveAll(x => x == null);
            if (_cur_enemy == null)
            {
                if (_enemy_list.Count == 1)
                    _cur_enemy = _enemy_list[0];
                else if (_enemy_list.Count > 1)
                    _cur_enemy = _enemy_list.OrderBy(x =>
                                                      Vector3.Distance(transform.position,
                                                      x.transform.position)).FirstOrDefault();
            }
        }
        else
            _cur_enemy = null;


        if (_cur_enemy != null)
        {
            if (!is_startCor)
                StartCoroutine(StartAttack());
        }
    }
    private IEnumerator StartAttack()
    {
        is_startCor = true;
        yield return new WaitForSeconds(2);
        Attack();
        is_startCor = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && is_spawned && !_enemy_list.Contains(other.gameObject))
            _enemy_list.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        _enemy_list.Remove(other.gameObject);
        _cur_enemy = null;
    }
    private void Attack()
    {
        if (_cur_enemy != null)
        {
            audio_cource.PlayOneShot(attack);
            var proj = Instantiate(projectile_prefab, projectile_spwn_pos.transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().target = _cur_enemy.transform.parent.gameObject;
            _cur_enemy = null;
        }
    }
}
