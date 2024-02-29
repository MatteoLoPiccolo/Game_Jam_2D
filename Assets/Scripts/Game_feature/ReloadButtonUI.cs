using MatteoLoPiccolo.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MatteoLoPiccolo.Feature
{
    public class ReloadButtonUI : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            UIManager.Instance.ReloadUI();

            gameObject.SetActive(false);
        }
    }
}