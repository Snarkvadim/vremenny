using UnityEngine;

namespace Assets.Scripts.Controller{
    public class UserActionListner : MonoBehaviour{
        private bool mouseDown;
        private UserActionRecord rec;
        // Use this for initialization
        private void Start(){
        }

        // Update is called once per frame
        private void Update(){
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                foreach (var touch in Input.touches)
                {
                    startRecord(touch);
                }
            }
#endif
#if UNITY_EDITOR
            startRecord();
#endif
        }

        private void startRecord(){
            if (!mouseDown && Input.GetMouseButtonDown(0)){
                mouseDown = true;
                rec = new UserActionRecord(Input.mousePosition);
            }
            if (mouseDown){
                rec.checkNewPosition(Input.mousePosition);
            }
            if (mouseDown && Input.GetMouseButtonUp(0)){
                rec.checkNewPosition(Input.mousePosition);
                mouseDown = false;
                PlayerControlHolder.Instance.SwipeControl(rec.getResultAction());
            }
        }

        private void startRecord(Touch touch){
            if (touch.phase == TouchPhase.Began){
                rec = new UserActionRecord(touch.position);
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary){
                rec.checkNewPosition(touch.position);
            }
            if (touch.phase == TouchPhase.Ended){
                rec.checkNewPosition(touch.position);
                PlayerControlHolder.Instance.SwipeControl(rec.getResultAction());
            }
        }
    }
}