using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

using PDollarGestureRecognizer;
using QDollarGestureRecognizer;

public class Qprogram : MonoBehaviour
{
    public Transform brushPrefab;
    private List<Gesture> trainingSet = new List<Gesture>();
    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private Vector3 virtualKeyPosition = Vector2.zero;
    [SerializeField] private RectTransform drawRectUI;
    private Rect drawArea;

    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    private int vertexCount = 0;
    private RuntimePlatform platform;

    public string gestureResult = "";

    [SerializeField] private TextMeshProUGUI gestureName;
    [SerializeField] private string newGestureName;


    // Start is called before the first frame update
    void Start()
    {
        platform = Application.platform;

        TextAsset[] gestureFolders = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gestureFolders)
        {
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get the panel's position and size
        Vector2 panelPosition = drawRectUI.position;
        Vector2 panelSize = drawRectUI.sizeDelta;

        // Create a rectangle using the panel's position and size
        drawArea = new Rect(panelPosition.x - panelSize.x / 2f,
                            panelPosition.y - panelSize.y / 2f,
                            panelSize.x,
                            panelSize.y);

        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
        }

        if (drawArea.Contains(virtualKeyPosition))
        {
            if (Input.GetMouseButtonDown(0))
            {
                ++strokeId;
                Transform tmpGesture = Instantiate(brushPrefab, transform.position, transform.rotation) as Transform;
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                gestureLinesRenderer.Add(currentGestureLineRenderer);

                vertexCount = 0;
            }

            if (Input.GetMouseButton(0))
            {
                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

                currentGestureLineRenderer.positionCount = ++vertexCount;
                currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }
        }
    }

    public void RecognizePattern()
    {
        if (strokeId != -1)
        {
            Gesture candidate = new Gesture(points.ToArray());
            gestureResult = QPointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            gestureName.SetText(gestureResult);
        }
        else
        {
            gestureName.SetText("Kosong");
        }
    }

    public void GetNewName(string s)
    {
        newGestureName = s;
    }
    public void AddNewPattern()
    {
        Debug.Log(points.Count > 0 && newGestureName != "");
        if (points.Count > 0 && newGestureName != "")
        {
            string fileName = String.Format(Application.dataPath + "/Resources/GestureSet/10-stylus-MEDIUM/{1}-{2}.xml", Application.dataPath, newGestureName, DateTime.Now.ToFileTime());

            trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

            GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);

            newGestureName = "";
            Debug.Log(newGestureName);
            Debug.Log(newGestureName.ToString());
        }
    }

    public void DeleteDrawing()
    {
        strokeId = -1;

        points.Clear();

        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
        {
            lineRenderer.positionCount = 0;
            Destroy(lineRenderer.gameObject);
        }
         gestureLinesRenderer.Clear();
    }
}
