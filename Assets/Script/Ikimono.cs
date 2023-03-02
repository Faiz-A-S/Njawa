using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ikimono : MonoBehaviour
{
    public string Name;
    public float MaxHealth;
    public float CurrentHealth;
    public int Damage;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual int GiveDamage()
    {
        return Damage;
    }
}
