using MatteoLoPiccolo.Manager;
using UnityEngine;

namespace MatteoLoPiccolo.Feature
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.gameObject.GetComponent<Target>();

            if (target != null)
            {
                Time.timeScale = 0f;
                GameManager.Instance.GameOver();
            }
        }
    }
}