using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Ghost : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    public float damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
