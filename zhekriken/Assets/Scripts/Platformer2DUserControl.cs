using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using Assets.Scripts;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Controlled = true;            // Player have control.


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        public void Controlled(bool controlled) {
            m_Controlled = controlled;
            m_Character.Move(0, false, false);
        }

        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            if (m_Controlled) {
                float h = 0;
                if (PlayerControlHolder.Instance.IsForwardRun && !PlayerControlHolder.Instance.IsBackRun) {
                    h = 1;
                }
                if (!PlayerControlHolder.Instance.IsForwardRun && PlayerControlHolder.Instance.IsBackRun)
                {
                    h = -1;
                }

                // Read the inputs.
                bool crouch = true; // временная хуйня
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
        }
    }
}
