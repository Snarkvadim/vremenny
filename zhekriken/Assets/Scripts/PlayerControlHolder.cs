//public enum Direction {
//    FORWARD,
//    BACK, 
//    HOLD
//}


namespace Assets.Scripts {
    public class PlayerControlHolder {

        private static PlayerControlHolder instance;
//    public Direction direction = Direction.HOLD;
        public bool IsForwardRun;
        public bool IsBackRun;


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
    }
}
