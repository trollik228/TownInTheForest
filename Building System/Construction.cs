using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Construction : MonoBehaviour
{
    public string name_;
    public string description;
    public float y_ofsset;

    public float health;
    public int price;

    public bool is_spawned = true;

    public void Damage(float _val_) =>  health -= _val_;
    protected virtual void ConstructLogic()
    {

    }

    private void Update()
    {
        if (health <= 0) Destroy(gameObject);
        ConstructLogic();
    }
}
