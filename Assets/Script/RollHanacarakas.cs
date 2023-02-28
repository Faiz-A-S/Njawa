using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollHanacarakas : MonoBehaviour
{
    [SerializeField] private List<Hanacaraka> hanacarakas;
    [SerializeField] private TextMeshProUGUI hanacarakaName;
    [SerializeField] private TextMeshProUGUI pointRound;
    [SerializeField] private TextMeshProUGUI timerCount;
    public string Result;
    public int POINTS = 1;
    public int MaxRounds;

    bool sekai;
    bool rolled;
    [SerializeField] private float timerMax = 60f;
    private float currentTimer = 0f;
    bool waktuHabis = false;

    int family;
    // Start is called before the first frame update
    void Start()
    {
        family = hanacarakas[0].HanacarakaMember.Count;
    }

    // Update is called once per frame
    void Update()
    {
        pointRound.text = POINTS.ToString();

        currentTimer += Time.deltaTime;
        int currINTTimer = (int)timerMax - (int)currentTimer;
        timerCount.text = currINTTimer.ToString();
        if(currentTimer > timerMax)
        {
            waktuHabis = true;
            currentTimer = 0;
        }
        else
        {
            waktuHabis = false;
        }


        Result = FindObjectOfType<Qprogram>().gestureResult;
        if (rolled != true && waktuHabis == false && POINTS < MaxRounds)
        {
            rolled = true;
            var getHana = hanacarakas[0].HanacarakaMember[Random.Range(0, family)];
            hanacarakaName.text = getHana.ToString();
        }

        if (hanacarakaName.text == Result)
        {
            hanacarakaName.text = "";
            POINTS += 1;
            rolled = false;
            FindObjectOfType<Qprogram>().DeleteDrawing();
        }

        if(waktuHabis == true || POINTS == MaxRounds)
        {
            POINTS = 1;
            NewRoll();
        }

    }

    private void NewRoll()
    {
        Debug.Log("New Roll");
    }
}
