using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : EnemyAbstr
{
    [SerializeField] private Transform spawn_bullet;
    [SerializeField] private GameObject bullet;

    private bool is_startCor = false;

    private void Start()
    {
        town = GameObject.Find("Town").transform;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _cur_target = town;
        _agent.destination = town.position;
        _agent.speed = speed;

        _animator.Play("idle");
    }

    protected override void EnemyLogic()
    {
        _mines.RemoveAll(x => x == null);

        if (_mines.Count > 0 && (_cur_target == town || _cur_target == null))
        {
            _cur_target = _mines.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault().transform;
            if (_cur_target.GetComponent<Construction>().is_spawned)
            {
                _agent.destination = _cur_target.position;
            }
            else _cur_target = town;
        }
        else if (_mines.Count == 0)
        {
            _cur_target = town;
            _agent.destination = _cur_target.position;
        }

        if (Vector3.Distance(transform.position, _cur_target.position) <= 90f)
        {
            _agent.velocity = Vector3.zero;
            if (!is_startCor )
                StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        is_startCor = true;
        yield return new WaitForSeconds(3.5f);
        Spawn_Bullet();
        audio_source.PlayOneShot(audio_clip_attack);
        is_startCor = false;

    }
    private void Spawn_Bullet()
    {
        spawn_bullet.localEulerAngles = new Vector3(-45, 0f, 0f);
        Vector3 fromTo = _cur_target.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleRadians = 45 * Mathf.PI / 180;
        float v2 = (10 * x * x) / (2 * (y - Mathf.Tan(angleRadians) * x) * Mathf.Pow(Mathf.Cos(angleRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        if (Vector3.Distance(transform.position, _cur_target.position) <= 90)
        {
            GameObject newBullet = Instantiate(bullet, spawn_bullet.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().velocity = spawn_bullet.forward * v;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mine"))
        {
            if (!_mines.Contains(other.gameObject))
            {
                _mines.Add(other.gameObject);
            }
        }
    }
}
