using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _gun;
    [SerializeField] private float _gunDistance;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _currentAmmo;

    private void Start()
    {
        ReloadGun();
    }

    private void Update()
    {
        Vector3 direction = GunPositionAndRotation();

        if (Input.GetMouseButtonDown(0) && HaveBullets())
            Shoot(direction);

        if (Input.GetKeyDown(KeyCode.R))
            UIManager.Instance.OpenReloadButtonUI();
    }

    private Vector3 GunPositionAndRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        _gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(_gunDistance, 0, 0);
        return direction;
    }

    private void Shoot(Vector3 direction)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        GameObject newBullet = Instantiate(_bulletPrefab, _gun.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * _bulletSpeed;

        UIManager.Instance.UpdateAmmoUI(_currentAmmo, _maxAmmo);

        Destroy(newBullet, 5f);
    }

    public void ReloadGun()
    {
        _currentAmmo = _maxAmmo;
        UIManager.Instance.UpdateAmmoUI(_currentAmmo, _maxAmmo);
        Time.timeScale = 1.0f;
    }

    private bool HaveBullets()
    {
        if (_currentAmmo <= 0)
            return false;

        _currentAmmo--;
        return true;
    }
}