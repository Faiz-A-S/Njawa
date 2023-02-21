using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTheResult : MonoBehaviour
{
    public string result;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       result = FindObjectOfType<Qprogram>().gestureResult;

        if(result == "faiz")
        {
            Debug.Log("SEKAI!!!");
        }
    }
}
