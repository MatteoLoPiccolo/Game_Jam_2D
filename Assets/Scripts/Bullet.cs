using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D;

    private void Update()
    {
        transform.right = _rb2D.velocity;
    }
}