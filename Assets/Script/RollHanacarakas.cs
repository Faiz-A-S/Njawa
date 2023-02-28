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
    public GAMESTATE CURRENTGAMESTATE;

    [SerializeField] private List<Hanacaraka> hanacarakas;
    [SerializeField] private TextMeshProUGUI hanacarakaName;
    [SerializeField] private TextMeshProUGUI pointRound;
    [SerializeField] private TextMeshProUGUI timerCount;
    [SerializeField] private TMP_InputField getName;
    [SerializeField] private Image hanacarakaImg;
    public string Result;
    public int POINTS = 1;
    public int MaxRounds;

    bool rolled;
    [SerializeField] private float timerMax = 60f;
    private float currentTimer = 0f;
    bool waktuHabis = false;

    int family;
    string tempName;
    // Start is called before the first frame update
    void Start()
    {
        CURRENTGAMESTATE = (GAMESTATE)Random.Range(1, 3);
        family = hanacarakas[0].HanacarakaMember.Count;
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
        }
    }

    private void TimerCount()
    {
        currentTimer += Time.deltaTime;
        int currINTTimer = (int)timerMax - (int)currentTimer;
        timerCount.text = currINTTimer.ToString();
        if (currentTimer > timerMax)
        {
            waktuHabis = true;
            currentTimer = 0;
        }
        else
        {
            waktuHabis = false;
        }
    }

    private void PlayerTurn()
    {
        CURRENTGAMESTATE = GAMESTATE.PLAYERTURN;

        pointRound.text = POINTS.ToString() + "/"+ MaxRounds;

        Result = FindObjectOfType<Qprogram>().gestureResult;
        if (rolled != true && waktuHabis == false && POINTS < MaxRounds)
        {
            rolled = true;
            var getHana = hanacarakas[0].HanacarakaMember[Random.Range(0, family)];
            hanacarakaName.text = getHana.HanacarakaName;
        }

        if (hanacarakaName.text == Result)
        {
            hanacarakaName.text = "";
            POINTS += 1;
            rolled = false;
            FindObjectOfType<Qprogram>().DeleteDrawing();
        }

        if (waktuHabis == true || POINTS == MaxRounds)
        {
            POINTS = 1;
            currentTimer = 0;
            CURRENTGAMESTATE = GAMESTATE.ENEMYTURN;
        }
    }

    private void EnemyTurn()
    {
        CURRENTGAMESTATE = GAMESTATE.ENEMYTURN;
        hanacarakaImg.gameObject.SetActive(true);
        pointRound.text = POINTS.ToString() + "/" + MaxRounds;

        if (rolled != true && waktuHabis == false && POINTS < MaxRounds)
        {
            rolled = true;
            var getHana = hanacarakas[0].HanacarakaMember[Random.Range(0, family)];
            hanacarakaImg.sprite = getHana.HanacarakaImg;
            tempName = getHana.HanacarakaName;
        }

        if (Input.GetKey(KeyCode.Mouse0) && tempName == getName.text)
        {
            getName.text = "";
            POINTS += 1;
            rolled = false;
        }

        if (waktuHabis == true || POINTS == MaxRounds)
        {
            POINTS = 1;
            currentTimer = 0;
            hanacarakaImg.gameObject.SetActive(false);
            CURRENTGAMESTATE = GAMESTATE.PLAYERTURN;
        }
    }
}
