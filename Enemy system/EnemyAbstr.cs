using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAbstr : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] protected float health = 40; // Здоровье
    [SerializeField] protected float speed; // Скорость
    public float damage = 10; // Урон
    [SerializeField] protected Priority priority; // Приоритет атаки
    [SerializeField] protected int gold;

    [Space, Header("Звуки")]
    [SerializeField] protected AudioSource audio_source;
    [SerializeField] protected AudioClip audio_clip_attack; // Звук атаки
    protected Transform town; // нахождение центра города
    protected NavMeshAgent _agent; // ии агент
    protected Animator _animator; // аниматор
    protected Transform _cur_target; // выбраная цель
    protected bool _is_stop = true; // стоит ли на месте
    protected List<GameObject> _towers = new(); // башни
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
