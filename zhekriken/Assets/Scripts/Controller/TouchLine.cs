using System.Collections.Generic;
using UnityEngine;

public class TouchLine : MonoBehaviour{
    private const int maxLine = 5;

    private float _zPos = 1;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    private int i;

    private GameObject lineGO;
    private List<Vector3> linePos;
    private LineRenderer lineRenderer;
    private bool mouseDown;

    private Dictionary<int, LineRenderer> lineRenderers = new Dictionary<int, LineRenderer>();
    private Dictionary<int, List<Vector3>> linePositions = new Dictionary<int, List<Vector3>>();
    private Dictionary<int, GameObject> lineGOs = new Dictionary<int, GameObject>(); 

    private void Start(){
        InputAggregator.Instance.OnNewTouchEvent += OnNewTouchEvent;
        InputAggregator.Instance.OnTouchMoveEvent += OnTouchMoveEvent;
        InputAggregator.Instance.OnTouchEndedEvent += OnTouchEndedEvent;
    }

    private void OnTouchEndedEvent(int touchId) {
        if (lineRenderers.ContainsKey(touchId))
        lineRenderers[touchId].SetVertexCount(0);
        i = 0;
        if (linePositions.ContainsKey(touchId))
        linePositions[touchId].Clear();
    }


    private void OnTouchMoveEvent(int touchId, Vector3 position){
        if (i < maxLine) {
            lineRenderers[touchId].SetVertexCount(i + 1);
            var mPosition = new Vector3(position.x, position.y, _zPos);
            linePositions[touchId].Add(mPosition);
            lineRenderers[touchId].SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
            i++;
        } else {
            for (int b = 0; b < linePositions[touchId].Count; b++) {
                lineRenderers[touchId].SetPosition((b), Camera.main.ScreenToWorldPoint(linePositions[touchId][b]));
            }
            var mPosition = new Vector3(position.x, position.y, _zPos);
            lineRenderers[touchId].SetPosition(maxLine - 1, Camera.main.ScreenToWorldPoint(mPosition));
            linePositions[touchId].RemoveAt(0);
            linePositions[touchId].Add(mPosition);
        }
    }

    private void OnNewTouchEvent(int touchId){
//        Debug.LogError("NewTouch id - "+touchId);
        if (!lineGOs.ContainsKey(touchId)){
            lineGOs.Add(touchId, new GameObject("Line"+touchId));
        }
        if (!lineRenderers.ContainsKey(touchId)){
            lineRenderers.Add(touchId, lineGOs[touchId].AddComponent<LineRenderer>());
            lineRenderers[touchId].sortingLayerName = "Front";
            lineRenderers[touchId].material = new Material(Shader.Find("Mobile/Particles/Additive"));
            lineRenderers[touchId].SetColors(c1, c2);
            lineRenderers[touchId].SetWidth(0.1F, 0);
            lineRenderers[touchId].SetVertexCount(1);
        }
        if (!linePositions.ContainsKey(touchId)){
            linePositions.Add(touchId, new List<Vector3>());
        }
    }

}