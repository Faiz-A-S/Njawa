using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum GAMESTATE
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
    WAIT
}

public class RollHanacarakas : MonoBehaviour
{
    private GAMESTATE CURRENTGAMESTATE;
    private GAMESTATE TEMPGAMESTATE;

    [field:SerializeField] public GameObject Hero { get; private set; }
    [field:SerializeField] public GameObject Enemy { get; private set; }

    [Header("Hanacaraka List")]
    [SerializeField] private List<Hanacaraka> _hanacarakaList;
    private HanacarakaSolo _currentHanacaraka;
    private int _familySize;
    private string _tempName;
    

    [Header("Rounds")]
    [SerializeField] private int _maxRounds;
    [SerializeField] private float _timerMax = 60f;
    private float _currentTimer;
    private bool _waktuHabis;
    private bool _rolled;
    private bool _hit;
    private int _points;
    private int _skipPoints;
    public float BonusPoint { get; private set; }

    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI _hanacarakaNameText;
    [SerializeField] private TextMeshProUGUI _pointRoundText;
    [SerializeField] private TextMeshProUGUI _timerCountText;
    [SerializeField] private TMP_InputField _getNameInputField;
    [SerializeField] private Image _hanacarakaImage;

    // Start is called before the first frame update
    void Start()
    {
        ResetAllAttribut();
        //CURRENTGAMESTATE = (GAMESTATE)Random.Range(1, 3);
        CURRENTGAMESTATE = GAMESTATE.ENEMYTURN;
        _familySize = _hanacarakaList[0].HanacarakaMember.Count;
        _getNameInputField.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BonusPoint = _points - _skipPoints;

        TimerCount();
        switch (CURRENTGAMESTATE)
        {
            case GAMESTATE.PLAYERTURN:
                PlayerTurn();
                break;
            case GAMESTATE.ENEMYTURN:
                EnemyTurn();
                break;
        }
    }

    private IEnumerator ActionProgress(GameObject attacker, GameObject defender)
    {
        _hit = false;
        if (!_hit)
        {
            float dmg = attacker.GetComponent<Ikimono>().GiveDamage();
            defender.GetComponent<Ikimono>().TakeDamage(dmg);
            _hit = true;
        }

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        ResetAllAttribut();
        if (TEMPGAMESTATE == GAMESTATE.PLAYERTURN)
        {
            CURRENTGAMESTATE = GAMESTATE.ENEMYTURN;
        }
        else
        {
            CURRENTGAMESTATE = GAMESTATE.PLAYERTURN;
        }
    }

    private void TimerCount()
    {
        _currentTimer += Time.deltaTime;
        int currINTTimer = (int)_timerMax - (int)_currentTimer;
        _timerCountText.text = currINTTimer.ToString();
        _waktuHabis = _currentTimer > _timerMax;
    }

    private void RollHana()
    {
        if (_rolled != true && _waktuHabis == false && _points <= _maxRounds)
        {
            _rolled = true;
            _currentHanacaraka = _hanacarakaList[0].HanacarakaMember[Random.Range(0, _familySize)];
        }
        _hanacarakaNameText.text = _currentHanacaraka.HanacarakaName;
        _tempName = _currentHanacaraka.HanacarakaName;
    }

    private void PlayerTurn()
    {
        CURRENTGAMESTATE = GAMESTATE.PLAYERTURN;

        _pointRoundText.text = _points.ToString() + "/" + _maxRounds;
        string Result = FindObjectOfType<Qprogram>().gestureResult;
        _hanacarakaImage.sprite = default;

        RollHana();

        if (_hanacarakaNameText.text == Result)
        {
            _hanacarakaNameText.text = "";
            _points += 1;
            _rolled = false;
            FindObjectOfType<Qprogram>().DeleteDrawing();
        }

        if (_waktuHabis || _points > _maxRounds)
        {
            FindObjectOfType<Qprogram>().DeleteDrawing();
            
            StartCoroutine(ActionProgress(Hero, Enemy));
            TEMPGAMESTATE = CURRENTGAMESTATE;
            CURRENTGAMESTATE = GAMESTATE.WAIT;
        }
    }

    private void EnemyTurn()
    {
        CURRENTGAMESTATE = GAMESTATE.ENEMYTURN;
        _getNameInputField.gameObject.SetActive(true);
        _pointRoundText.text = _points.ToString() + "/" + _maxRounds;

        RollHana();
        _hanacarakaImage.sprite = _currentHanacaraka.HanacarakaImg;

        //check is on button
        if (_waktuHabis == true || _points > _maxRounds)
        {
            _getNameInputField.gameObject.SetActive(false);
            StartCoroutine(ActionProgress(Enemy, Hero));
            TEMPGAMESTATE = CURRENTGAMESTATE;
            CURRENTGAMESTATE = GAMESTATE.WAIT;
        }
    }

    public void Skip()
    {
        _rolled = false;
        _points += 1;
        RollHana();
        _skipPoints += 1;
    }

    private void ResetAllAttribut()
    {
        _rolled = false;
        BonusPoint = 1;
        _points = 1;
        _currentTimer = 0;
        _skipPoints = 0;
    }

    public void EnemyTurnRecognized()
    {
        if (_tempName == _getNameInputField.text)
        {
            _getNameInputField.text = "";
            _points += 1;
            _rolled = false;
        } 
        else
        {
            _hanacarakaNameText.SetText("Kosong");
        }
    }
}
