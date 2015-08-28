using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI {
    public class BackBTN : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public void OnPointerDown(PointerEventData eventData)
        {
            PlayerControlHolder.Instance.IsBackRun = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PlayerControlHolder.Instance.IsBackRun = false;
        }
    }
}
