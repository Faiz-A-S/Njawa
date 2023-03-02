using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Ikimono
{
    private int MultDmg;
    private int DefendMult;

    public float equipment;

    private void Update()
    {
        MultDmg = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().attackBonus;
        DefendMult = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().defendBonus;
    }

    public override int GiveDamage()
    {
        int total = Damage * MultDmg;
        return total;
    }

    public override void TakeDamage(int damage)
    {
        //base.TakeDamage(damage);
        float meth = (damage * DefendMult) / equipment;
        CurrentHealth -= damage - meth;
    }
}
