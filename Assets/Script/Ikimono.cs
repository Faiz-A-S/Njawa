using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ikimono : MonoBehaviour
{
    public string Name;
    public float MaxHealth;
    public float CurrentHealth;
    public float Damage;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log(Name + " take " + damage + " damage");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("ded");
        //Destroy(gameObject);
    }

    public virtual float GiveDamage()
    {
        //Debug.Log(Name + " give " + Damage + " damage");
        return Damage;
    }
}
