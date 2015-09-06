using Assets.Scripts;
using UnityEngine;

namespace UnityStandardAssets._2D{
    [RequireComponent(typeof (RabbitController))]
    public class PlatformerUserControl : MonoBehaviour{
        private RabbitController m_Character;
        private bool m_Controlled = true; // Player have control.
        private bool m_Jump;


        private void Awake(){
            m_Character = GetComponent<RabbitController>();
        }

        public void Controlled(bool controlled){
            m_Controlled = controlled;
            m_Character.Move(0, false);
        }


        private void FixedUpdate(){
            if (PlayerControlHolder.Instance.IsJump){
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = true;
            }
            if (m_Controlled){
                float h = 0;
                if (PlayerControlHolder.Instance.IsForwardRun && !PlayerControlHolder.Instance.IsBackRun){
                    h = 1;
                }
                if (!PlayerControlHolder.Instance.IsForwardRun && PlayerControlHolder.Instance.IsBackRun){
                    h = -1;
                }

                // Read the inputs.
                m_Character.Move(h, m_Jump);
                m_Jump = false;
                PlayerControlHolder.Instance.IsJump = false;
            }
        }
    }
}