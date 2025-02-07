using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    public float damage;
    public GameObject target;
    public GameObject k;
    public float speed = 5;

    [SerializeField] private AudioSource audio_cource;
    [SerializeField] private AudioClip attack;


    private List<GameObject> targets = new List<GameObject>();
    private Vector3 point = new Vector3(0,-1000000,0);
    private void Update()
    {
        if (point == new Vector3(0, -1000000, 0)) 
            Attack();

        transform.LookAt(point);
      //  transform.eulerAngles *=-1 ;
        transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            audio_cource.PlayOneShot(attack);

            foreach (var t in targets)
            {
                if (t != null)
                t.transform.parent.GetComponent<EnemyAbstr>().GetDamage(damage);
            }
            var g = Instantiate(k, transform.position, Quaternion.identity);
            transform.position = new Vector3(0,-1000,0);
            StartCoroutine(dd(g));
        }
        if (other.CompareTag("Enemy")) targets.Add(other.gameObject);
    }

    IEnumerator dd(GameObject g)
    {
       // gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(g);
        Destroy(gameObject);
    }
    public void Attack()
    {
        Vector3 targetPosition;
        if (target != null)
        {
            targetPosition = target.transform.position;

            // –ассчитываем врем€ полета снар€да
            float distance = Vector3.Distance(transform.position, targetPosition);
            float flightTime = distance / speed;

            // –ассчитываем предсказанную позицию цели
            point = targetPosition + target.GetComponent<NavMeshAgent>().velocity * flightTime;
        }
        else Destroy(gameObject);
    }
}
