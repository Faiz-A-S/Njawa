using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInfoSet : MonoBehaviour
{
    [SerializeField] private bool heroUI;
    [SerializeField] private bool enemyUI;

    [SerializeField] private TextMeshProUGUI nameChara;
    [SerializeField] private Image healthBar;

    private string names;
    private float health;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (heroUI)
        {
            names = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().Name;
            currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().CurrentHealth;
            health = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().MaxHealth;
        }
        else
        {
            names = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().Name;
            currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().CurrentHealth;
            health = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().MaxHealth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heroUI)
        {
            currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().CurrentHealth;
        }
        else
        {
            currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().CurrentHealth;
        }

        nameChara.text = names;
        float fillLevel = currentHealth / health;
        healthBar.fillAmount = fillLevel;
    }
}
