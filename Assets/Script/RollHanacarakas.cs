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
    PLAYERACTION,
    ENEMYACTION
}

public class RollHanacarakas : MonoBehaviour
{
    public GAMESTATE CURRENTGAMESTATE;
    public GAMESTATE TEMPGAMESTATE;

    [SerializeField] private GameObject Hero;
    [SerializeField] private GameObject Enemy;

    [SerializeField] private List<Hanacaraka> hanacarakas;
    [SerializeField] private TextMeshProUGUI hanacarakaNameText;
    [SerializeField] private TextMeshProUGUI pointRoundText;
    [SerializeField] private TextMeshProUGUI timerCountText;
    [SerializeField] private TMP_InputField getNameInputField;
    [SerializeField] private Image hanacarakaImage;
    [SerializeField] private int maxRounds;

    [SerializeField] private float timerMax = 60f;
    private bool rolled;
    private float currentTimer;
    private bool waktuHabis;

    private int familySize;
    private string tempName;
    private HanacarakaSolo currentHanacaraka;
    private bool hit;

    private int points;
    public int tempPoints;

    // Start is called before the first frame update
    void Start()
    {
        //CURRENTGAMESTATE = (GAMESTATE)Random.Range(1, 3);
        CURRENTGAMESTATE = GAMESTATE.ENEMYTURN;
        familySize = hanacarakas[0].HanacarakaMember.Count;
    }

    // Update is called once per frame
    void Update()
    {
        TimerCount();
        switch (CURRENTGAMESTATE)
        {
            case GAMESTATE.PLAYERTURN:
                PlayerTurn();
                break;
            case GAMESTATE.ENEMYTURN:
                EnemyTurn();
                break;
            case GAMESTATE.PLAYERACTION:
                ActionProgress(Hero, Enemy);
                break;
            case GAMESTATE.ENEMYACTION:
                ActionProgress(Enemy, Hero);
                break;
        }
    }

    private void ActionProgress(GameObject attacker, GameObject defender)
    {
        if (!hit)
        {
            int dmg = attacker.GetComponent<Ikimono>().GiveDamage();
            defender.GetComponent<Ikimono>().TakeDamage(dmg);
            hit = true;
        }

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
        currentTimer += Time.deltaTime;
        int currINTTimer = (int)timerMax - (int)currentTimer;
        timerCountText.text = currINTTimer.ToString();
        waktuHabis = currentTimer > timerMax;
    }

    private void RollHana()
    {
        if (rolled != true && waktuHabis == false && points < maxRounds)
        {
            rolled = true;
            currentHanacaraka = hanacarakas[0].HanacarakaMember[Random.Range(0, familySize)];
        }
    }

    private void PlayerTurn()
    {
        CURRENTGAMESTATE = GAMESTATE.PLAYERTURN;

        pointRoundText.text = points.ToString() + "/" + maxRounds;
        string Result = FindObjectOfType<Qprogram>().gestureResult;

        RollHana();
        hanacarakaNameText.text = currentHanacaraka.HanacarakaName;

        if (hanacarakaNameText.text == Result)
        {
            hanacarakaNameText.text = "";
            points += 1;
            rolled = false;
            FindObjectOfType<Qprogram>().DeleteDrawing();
        }

        if (waktuHabis || points == maxRounds)
        {
            tempPoints = points;
            TEMPGAMESTATE = CURRENTGAMESTATE;
            CURRENTGAMESTATE = GAMESTATE.PLAYERACTION;
            points = 1;
            currentTimer = 0;
        }
    }

    private void EnemyTurn()
    {
        CURRENTGAMESTATE = GAMESTATE.ENEMYTURN;
        getNameInputField.gameObject.SetActive(true);
        //hanacarakaImage.gameObject.SetActive(true);
        pointRoundText.text = points.ToString() + "/" + maxRounds;

        RollHana();
        hanacarakaImage.sprite = currentHanacaraka.HanacarakaImg;
        tempName = currentHanacaraka.HanacarakaName;

        //check is on button
        if (waktuHabis == true || points == maxRounds)
        {
            tempPoints = points;
            //hanacarakaImage.gameObject.SetActive(false);
            getNameInputField.gameObject.SetActive(false);
            TEMPGAMESTATE = CURRENTGAMESTATE;
            CURRENTGAMESTATE = GAMESTATE.ENEMYACTION;
            points = 1;
            currentTimer = 0;
        }
    }

    public void EnemyTurnRecognized()
    {
        if (tempName == getNameInputField.text)
        {
            getNameInputField.text = "";
            points += 1;
            rolled = false;
        } 
        else
        {
            hanacarakaNameText.SetText("Kosong");
        }
    }
}
