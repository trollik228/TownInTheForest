using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class Skeleton : EnemyAbstr
{
    private NavMeshObstacle obstacle;

    private void Start()
    {
        town = GameObject.Find("Town").transform;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        _cur_target = town;
        _agent.destination = town.position;
        _agent.speed = speed;

        _animator.Play("idle");
    }
    protected override void EnemyLogic()
    {
        _towers.RemoveAll(x => x == null);

        if (_cur_target == null)
        {
            _agent.enabled = true;
            obstacle.enabled = false;

            _cur_target = town;
            _agent.destination = _cur_target.position;
        }

        if (_towers.Count == 0 && _cur_target != town && _agent.enabled)
        {
            _cur_target = town;
            _agent.destination = _cur_target.position;
        }

        else if (_towers.Count > 0 && (_cur_target == town || _cur_target == null))
        {
            _cur_target = _towers.OrderBy(g_obj => Vector3.Distance(transform.position, g_obj.transform.position)).FirstOrDefault().transform;
            if (_cur_target.GetComponentInParent<DefenceTower>().is_spawned)
            {
                _agent.destination = _cur_target.position;
            }
            else _cur_target = null;
        }

            if (_cur_target != town && _cur_target != null && (_cur_target.transform.position - transform.position).sqrMagnitude < 36)
            {
            _agent.velocity = Vector3.zero;
                _agent.enabled = false;
                obstacle.enabled = true;
                _animator.Play("attack");
                _is_stop = true;
            }

        if (_agent.velocity != Vector3.zero)
        {
            // Ќо мы еще не "отметили", что враг должен идти.
            if (_is_stop && (_animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || _animator.GetCurrentAnimatorStateInfo(0).IsName("attack")))
            {
                _agent.enabled = true;
                obstacle.enabled = false;
                _animator.Play("walk");
                _is_stop = false;
            }
        }


    }   
    public void Attack() // метод атаки, запускаетс€ где-то посередине анимации атаки
    {
        audio_source.PlayOneShot(audio_clip_attack);
        if (_cur_target != null && _cur_target.transform.parent != null)
        _cur_target.transform.parent.GetComponent<Construction>().Damage(damage);
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TowerDefense"))
        {
            if (priority == Priority.defense)
            {
                if (!_towers.Contains(other.gameObject))
                    _towers.Add(other.gameObject);
                _cur_target = null;
            }
        }
    }
}

