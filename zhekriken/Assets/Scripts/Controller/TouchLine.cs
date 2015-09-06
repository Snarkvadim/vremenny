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

    private void Start(){
        lineGO = new GameObject("Line");
        lineRenderer = lineGO.AddComponent<LineRenderer>();
//        lineRenderer = lineGO.GetComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Front";
        lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.1F, 0);
        lineRenderer.SetVertexCount(1);
        mouseDown = false;
        linePos = new List<Vector3>();
        Debug.Log(Camera.main);
    }

    private void Update(){
        if (!mouseDown && Input.GetMouseButtonDown(0)){
            mouseDown = true;
        }
        if (mouseDown){
            if (i < maxLine){
                lineRenderer.SetVertexCount(i + 1);
                var mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zPos);
                linePos.Add(mPosition);
                lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
                i++;
            }
            else{
                for (int b = 0; b < linePos.Count; b++){
                    lineRenderer.SetPosition((b), Camera.main.ScreenToWorldPoint(linePos[b]));
                }
                var mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zPos);
                lineRenderer.SetPosition(maxLine - 1, Camera.main.ScreenToWorldPoint(mPosition));
                linePos.RemoveAt(0);
                linePos.Add(mPosition);
            }
        }
        if (mouseDown && Input.GetMouseButtonUp(0)){
            lineRenderer.SetVertexCount(0);
            i = 0;
            linePos.Clear();
            mouseDown = false;
        }
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved){
                lineRenderer.SetVertexCount(i + 1);
                var mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zPos);
                lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
                i++;

                /* Add Collider */

                var bc = lineGO.AddComponent<BoxCollider>();
                bc.transform.position = lineRenderer.transform.position;
                bc.size = new Vector3(0.1f, 0.1f, 0.1f);
            }

            if (touch.phase == TouchPhase.Ended){
                /* Remove Line */

                lineRenderer.SetVertexCount(0);
                i = 0;

                /* Remove Line Colliders */

                BoxCollider[] lineColliders = lineGO.GetComponents<BoxCollider>();

                foreach (BoxCollider b in lineColliders){
                    Destroy(b);
                }
            }
        }
    }
}