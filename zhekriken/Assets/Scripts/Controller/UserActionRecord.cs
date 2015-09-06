using UnityEngine;

namespace Assets.Scripts.Controller{
    internal class UserActionRecord{
        private int _count;
        private Vector3 bottomPos;
        private Vector3 leftPos;
        private Vector3 rightPos;
        private Vector3 topPos;

        public UserActionRecord(Vector3 pos){
//            Debug.Log(pos.x +" - "+pos.y);
            leftPos = pos;
            topPos = pos;
            rightPos = pos;
            bottomPos = pos;
            _count = 1;
        }

        public void checkNewPosition(Vector3 pos){
            if (leftPos.x > pos.x){
                leftPos = pos;
                leftPos.z = _count;
            }
            if (topPos.y < pos.y){
                topPos = pos;
                topPos.z = _count;
            }
            if (rightPos.x < pos.x){
                rightPos = pos;
                rightPos.z = _count;
            }
            if (bottomPos.y > pos.y){
                bottomPos = pos;
                bottomPos.z = _count;
            }
            _count++;
        }

        public UserAction getResultAction(){
            if (checkSwipeUp()) return UserAction.SWIPE_UP;
            if (checkSwipeDown()) return UserAction.SWIPE_DOWN;
            if (checkSwipeRight()) return UserAction.SWIPE_RIGHT;
            if (checkSwipeLeft()) return UserAction.SWIPE_LEFT;
            return UserAction.NO_SWIPE;
        }

        private bool checkSwipeUp(){
            if (bottomPos.z < topPos.z){
                if (Mathf.Abs(leftPos.x - rightPos.x) < (Mathf.Abs(topPos.y - bottomPos.y))/3){
                    return true;
                }
            }
            return false;
        }

        private bool checkSwipeDown(){
            if (bottomPos.z > topPos.z){
                if (Mathf.Abs(leftPos.x - rightPos.x) < (Mathf.Abs(topPos.y - bottomPos.y))/3){
                    return true;
                }
            }
            return false;
        }

        private bool checkSwipeRight(){
            if (leftPos.z < rightPos.z){
                if (Mathf.Abs(bottomPos.y - topPos.y) < (Mathf.Abs(leftPos.x - rightPos.x))/3){
                    return true;
                }
            }
            return false;
        }

        private bool checkSwipeLeft(){
            if (leftPos.z > rightPos.z){
                if (Mathf.Abs(bottomPos.y - topPos.y) < (Mathf.Abs(leftPos.x - rightPos.x))/3){
                    return true;
                }
            }
            return false;
        }
    }
}