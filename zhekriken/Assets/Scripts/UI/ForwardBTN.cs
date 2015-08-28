using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI {
    public class ForwardBTN : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public void OnPointerDown(PointerEventData eventData) {
            PlayerControlHolder.Instance.IsForwardRun = true;
        }

        public void OnPointerUp(PointerEventData eventData) {
            PlayerControlHolder.Instance.IsForwardRun = false;
        }
    }
}
