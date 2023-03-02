using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Ikimono
{
    public int MultDmg;

    private void Update()
    {
        MultDmg = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().tempPoints;
    }

    public override int GiveDamage()
    {
        int total = Damage * MultDmg;
        return total;
    }
}
