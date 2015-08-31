using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class JumpBTN : MonoBehaviour, IPointerDownHandler
    {

        public void OnPointerDown(PointerEventData eventData)
        {
            PlayerControlHolder.Instance.IsJump = true;
        }

    }
}
