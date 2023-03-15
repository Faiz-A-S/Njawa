using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Ikimono
{
    private float _bonusMult;
    public float equipment;
    

    private void Update()
    {
        _bonusMult = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().BonusPoint;
    }

    public override float GiveDamage()
    {
        float total = base.GiveDamage() * _bonusMult;
        Debug.Log(_bonusMult);
        return total;
    }

    public override void TakeDamage(float damage)
    {
        //Damage Negation = (Equipment Defense / (Equipment Defense + Attacker's Attack Power)) * 100%
        float dmgNegationPersen = (equipment * _bonusMult / (equipment + damage)) * 100f;
        float dmgNegation = ((damage * dmgNegationPersen) / 100f);
        Debug.Log(_bonusMult);
        base.TakeDamage(damage - dmgNegation);
    }
}
