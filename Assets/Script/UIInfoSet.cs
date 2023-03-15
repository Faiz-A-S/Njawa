using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInfoSet : MonoBehaviour
{
    [SerializeField] private bool _heroUI;
    [SerializeField] private bool _enemyUI;

    [SerializeField] private TextMeshProUGUI _nameCharaText;
    [SerializeField] private Image _healthBarImage;

    private string _names;
    private float _health;
    private float _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (_heroUI)
        {
            _names = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().Name;
            _currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().CurrentHealth;
            _health = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().MaxHealth;
        }
        else
        {
            _names = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().Name;
            _currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().CurrentHealth;
            _health = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().MaxHealth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_heroUI)
        {
            _currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Hero.GetComponent<Ikimono>().CurrentHealth;
        }
        else
        {
            _currentHealth = GameObject.Find("GetsTheResult").GetComponent<RollHanacarakas>().Enemy.GetComponent<Ikimono>().CurrentHealth;
        }

        _nameCharaText.text = _names;
        float fillLevel = _currentHealth / _health;
        _healthBarImage.fillAmount = fillLevel;
    }
}
