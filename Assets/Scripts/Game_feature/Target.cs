using MatteoLoPiccolo.Manager;
using UnityEngine;

namespace MatteoLoPiccolo.Feature
{
    public class Target : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var bullet = collision.gameObject.transform.GetComponent<Bullet>();

            if (bullet != null)
            {
                GameManager.Instance.UpdateScore();
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}