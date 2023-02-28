using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushWidth : MonoBehaviour
{
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(lineRenderer.GetPosition(1).y - lineRenderer.GetPosition(0).y, lineRenderer.GetPosition(1).x - lineRenderer.GetPosition(0).x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) < 45 || Mathf.Abs(angle) > 135)
        {
            lineRenderer.widthMultiplier = 0.1f;
        }
        else
        {
            lineRenderer.widthMultiplier = 0.3f;
        }
    }
}
