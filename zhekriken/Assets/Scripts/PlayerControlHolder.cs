//public enum Direction {
//    FORWARD,
//    BACK, 
//    HOLD
//}
using Assets.Scripts.Controller;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts {
    public class PlayerControlHolder {

        private static PlayerControlHolder instance;
//    public Direction direction = Direction.HOLD;
        public bool IsForwardRun;
        public bool IsBackRun;
        public bool IsJump;


        public static PlayerControlHolder Instance {
            get {
                if (instance == null) {
                    instance = new PlayerControlHolder();
                }
                return instance;
            }
        }

        protected PlayerControlHolder() {
        
        }

        public void SwipeControl(UserAction action){
            if (action == UserAction.SWIPE_RIGHT && IsBackRun){
                IsBackRun = false;
            }
            else if (action == UserAction.SWIPE_RIGHT && !IsBackRun) {
                IsForwardRun = true;
            }
            else if (action == UserAction.SWIPE_LEFT && IsForwardRun) {
                IsForwardRun = false;
            }
            else if (action == UserAction.SWIPE_LEFT && !IsForwardRun) {
                IsBackRun = true;
            }
            else if (action == UserAction.SWIPE_UP) {
                IsJump = true;
            }
        }
    }
}
