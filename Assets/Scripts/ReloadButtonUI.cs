using UnityEngine;
using UnityEngine.EventSystems;

public class ReloadButtonUI : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.Instance.ReloadUI();

        gameObject.SetActive(false);
    }
}