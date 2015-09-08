using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputAggregator : MonoBehaviour {
    public Action<Vector3> OnScrollEvent; //deltaPosition Vector3
    public Action<float> OnZoomEvent; //deltaPosition Vector3
    public Action<int> OnNewTouchEvent; //touch number
    public Action<int, Vector3> OnTouchMoveEvent; //touch number
    public Action<int> OnTouchEndedEvent; //touch number

    private EventSystem eventSystem;

    private bool is_mouse_button_down;
    private Vector3 previous_position;

    private static InputAggregator instance;

    public static InputAggregator Instance {
        get {
            if (instance == null) {
                var go = new GameObject("InputAggregator", typeof(InputAggregator));
                instance = go.GetComponent<InputAggregator>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Start() {
        eventSystem = EventSystem.current;
    }

    private void Update() {
        onTouchEvent();
        onMouseEvent();
    }

    private void onTouchEvent() {
        if (Input.touchCount > 0){
            foreach (var touch in Input.touches){
                if (touch.phase == TouchPhase.Began){
                    OnNewTouchEvent(touch.fingerId);
                }
                if (touch.phase == TouchPhase.Moved){
                    OnTouchMoveEvent(touch.fingerId, touch.position);
                }
                if (touch.phase == TouchPhase.Ended) {
                    OnTouchEndedEvent(touch.fingerId);
                }
            }
        }
    }

    private void onMouseEvent() {
        if (Input.GetMouseButtonDown(0)){
            if (eventSystem.currentSelectedGameObject != null)
                return;
            is_mouse_button_down = true;
            OnNewTouchEvent(0);
        }
        if (Input.GetMouseButtonUp(0)){
            is_mouse_button_down = false;
            OnTouchEndedEvent(0);
        }
        if (is_mouse_button_down) {
            OnTouchMoveEvent(0, Input.mousePosition);
        }

    }
}