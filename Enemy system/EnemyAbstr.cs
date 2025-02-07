using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAbstr : MonoBehaviour
{
    [Header("�������� ���������")]
    [SerializeField] protected float health = 40; // ��������
    [SerializeField] protected float speed; // ��������
    public float damage = 10; // ����
    [SerializeField] protected Priority priority; // ��������� �����
    [SerializeField] protected int gold;

    [Space, Header("�����")]
    [SerializeField] protected AudioSource audio_source;
    [SerializeField] protected AudioClip audio_clip_attack; // ���� �����
    protected Transform town; // ���������� ������ ������
    protected NavMeshAgent _agent; // �� �����
    protected Animator _animator; // ��������
    protected Transform _cur_target; // �������� ����
    protected bool _is_stop = true; // ����� �� �� �����
    protected List<GameObject> _towers = new(); // �����
    protected List<GameObject> _mines = new();
    protected enum Priority
    {
        defense, town
    }


    protected virtual void EnemyLogic()
    {
        Debug.LogError("ERROR! ENEMY_LOGIC IS EMPTHY!");
    }

    private void Update()
    {
        EnemyLogic();
    }
    public void GetDamage(float _val_)
    {
        health -= _val_;
        if (health < 0)
        {
            MoneySystem.GetMoney(gold);
            StatisticSystem.kill_enemys += 1;
            Destroy(gameObject);
        }
    }
}
