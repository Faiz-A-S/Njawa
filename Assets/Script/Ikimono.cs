using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ikimono : MonoBehaviour
{
    public string Name;
    public int Health;
    public int Damage;

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual int GiveDamage()
    {
        return Damage;
    }
}
