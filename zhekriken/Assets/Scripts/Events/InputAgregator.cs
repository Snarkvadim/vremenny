using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputAggregator : MonoBehaviour {
    private static InputAggregator instance;
    public Action OnJumpEvent; //deltaPosition Vector3
    public Action<int> OnMoveEvent; //deltaPosition Vector3
    public Action<int> OnNewTouchEvent; //touch number
    public Action<Vector3> OnScrollEvent; //deltaPosition Vector3
    public Action<int> OnTouchEndedEvent; //touch number
    public Action<int, Vector3> OnTouchMoveEvent; //touch number
    public Action<float> OnZoomEvent; //deltaPosition Vector3


    private EventSystem eventSystem;

    private bool is_mouse_button_down;
    private Vector3 previous_position;

    public static InputAggregator Instance {
        get {
            if (instance == null) {
                var go = new GameObject("InputAggregator", typeof (InputAggregator));
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
//        onTouchEvent();
        onTouchEventUI();
//        onMouseEvent();
    }

    private void onTouchEventUI() {
        if (Input.touchCount > 0) {
//            Debug.LogError("Input.touchCount - " + Input.touchCount);
            foreach (Touch touch in Input.touches) {
                var pointer = new PointerEventData(EventSystem.current);
                pointer.position = touch.position;

                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                foreach (RaycastResult raycastResult in raycastResults) {
                    if (raycastResult.gameObject.name.Equals("ForwardBTN")) {
                        OnMoveEvent(1);
                    }
                    if (raycastResult.gameObject.name.Equals("BackBTN"))
                    {
                        OnMoveEvent(-1);
                    }
                    if (raycastResult.gameObject.name.Equals("JumpBTN")&&touch.phase==TouchPhase.Began)
                    {
                        OnJumpEvent();
                    }
                }
            }
        }
        else {
            onMouseEventUI();
        }
    }

    private void onMouseEventUI() {
        if (Input.GetMouseButton(0)) {
//            Debug.LogError("MouseClick");
            var pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0) {
                if (raycastResults[0].gameObject.name.Equals("ForwardBTN")) {
                    OnMoveEvent(1);
                }
                else if (raycastResults[0].gameObject.name.Equals("BackBTN")) {
                    OnMoveEvent(-1);
                }
                else if (raycastResults[0].gameObject.name.Equals("JumpBTN") && Input.GetMouseButtonDown(0)) {
                    OnJumpEvent();
                }
//                Debug.LogError(raycastResults[0].gameObject.name);
            }
        }
        else {
            OnMoveEvent(0); 
        }
    }


    private void onTouchEvent() {
//        if (Input.touchCount > 0) {
//            foreach (Touch touch in Input.touches) {
//                if (touch.phase == TouchPhase.Began) {
//                    OnNewTouchEvent(touch.fingerId);
//                }
//                if (touch.phase == TouchPhase.Moved||touch.phase == TouchPhase.Stationary) {
//                    OnTouchMoveEvent(touch.fingerId, touch.position);
//                }
//                if (touch.phase == TouchPhase.Ended) {
//                    OnTouchEndedEvent(touch.fingerId);
//                }
//            }
//        }
//        Debug.LogError("Input.touchCount - " + Input.touchCount);
        if (Input.touchCount > 0) {
            Debug.LogError("Input.touchCount - " + Input.touchCount);
            foreach (Touch touch in Input.touches) {
//                if (touch.phase == TouchPhase.Began)
//                Debug.LogError("touch.phase - " + touch.phase);
                Vector3 touchScreenPosition = touch.position;
                if (touchScreenPosition.x <= Screen.width/2 && touchScreenPosition.y <= Screen.height/2) {
                    if (OnMoveEvent != null) {
                        Debug.LogError("MoveBack");
                        OnMoveEvent(-1);
                    }
                }
                else if (touchScreenPosition.x > Screen.width/2 && touchScreenPosition.y <= Screen.height/2) {
                    if (OnMoveEvent != null) {
                        Debug.LogError("MoveForward");
                        OnMoveEvent(1);
                    }
                }
                else if (touch.phase == TouchPhase.Began &&
                         (touchScreenPosition.x <= Screen.width/2 && touchScreenPosition.y > Screen.height/2)) {
                    if (OnJumpEvent != null) {
                        Debug.LogError("Jump");
                        OnJumpEvent();
                    }
                }
            }
        }
        else {
            onMouseEvent();
        }
    }

    private void onMouseEvent() {
        if (Input.GetMouseButtonDown(0)) {
            if (eventSystem.currentSelectedGameObject != null)
                return;
            is_mouse_button_down = true;
            if (Input.mousePosition.x <= Screen.width/2 && Input.mousePosition.y > Screen.height/2) {
                if (OnJumpEvent != null) {
                    OnJumpEvent();
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            is_mouse_button_down = false;
//            OnTouchEndedEvent(0);
        }
//        if (is_mouse_button_down) {
//            OnTouchMoveEvent(0, Input.mousePosition);
//        }

        if (Input.GetMouseButton(0)) {
            Vector3 touchScreenPosition = Input.mousePosition;
            if (touchScreenPosition.x <= Screen.width/2 && touchScreenPosition.y <= Screen.height/2) {
                if (OnMoveEvent != null) {
                    OnMoveEvent(-1);
                }
            }
            if (touchScreenPosition.x > Screen.width/2 && touchScreenPosition.y <= Screen.height/2) {
                if (OnMoveEvent != null) {
                    OnMoveEvent(1);
                }
            }
        }
        else {
            OnMoveEvent(0);
        }
    }
}