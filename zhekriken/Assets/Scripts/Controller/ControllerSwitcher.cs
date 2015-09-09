using UnityEngine;
using System.Collections;

public class ControllerSwitcher : MonoBehaviour {
    public GameObject buttonController;
    public GameObject SwipeController;


//    void Awake() {
//        buttonController = GameObject.Find("ButtonControllerHolder");
//        SwipeController = GameObject.Find("SwipeController");
//        buttonController.SetActive(false);
//        gameObject.SetActive(false);
//    }

    public void SwipeControllOn() {
        buttonController.SetActive(false);
        SwipeController.SetActive(true);
        gameObject.SetActive(false);
    }

    public void buttonControlOn() {
        buttonController.SetActive(true);
        SwipeController.SetActive(false);
        gameObject.SetActive(false);
    }
}
