using MatteoLoPiccolo.Feature;
using MatteoLoPiccolo.Player;
using TMPro;
using UnityEngine;

namespace MatteoLoPiccolo.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private TextMeshProUGUI _timerText;

        [SerializeField] private GunController _gunController;
        [SerializeField] private BoxCollider2D _reloadButtonRandomSpawnWindow;
        [SerializeField] private ReloadButtonUI[] _reloadButtons;
        private int _reloadCounter;

        [SerializeField] private GameObject _gameOver;

        private static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);

            _gameOver.SetActive(false);

            _reloadButtons = GetComponentsInChildren<ReloadButtonUI>(true);
        }

        public void OpenReloadButtonUI()
        {
            foreach (var button in _reloadButtons)
            {
                button.gameObject.SetActive(true);

                float randomX, randomY;
                SpawnTargetsInRandomSpaceWindow(out randomX, out randomY);

                button.transform.position = new Vector3(randomX, randomY);
            }

            Time.timeScale = 0.4f;
            _reloadCounter = _reloadButtons.Length;
        }

        private void SpawnTargetsInRandomSpaceWindow(out float randomX, out float randomY)
        {
            randomX = Random.Range(_reloadButtonRandomSpawnWindow.bounds.min.x, _reloadButtonRandomSpawnWindow.bounds.max.x);
            randomY = Random.Range(_reloadButtonRandomSpawnWindow.bounds.min.y, _reloadButtonRandomSpawnWindow.bounds.max.y);
        }

        public void ReloadUI()
        {
            if (_reloadCounter > 0)
                _reloadCounter--;

            if (_reloadCounter <= 0)
                _gunController.ReloadGun();
        }

        public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
        {
            _ammoText.text = "Ammo : " + currentAmmo.ToString() + "/" + maxAmmo.ToString();
        }

        public void UpdateScore(int amout)
        {
            _scoreText.text = "Score : " + amout.ToString();
        }

        public void UpdateTimer()
        {
            _timerText.text = "Timer : " + Time.time.ToString("#,#");
        }

        public void GameOver()
        {
            _gameOver.SetActive(true);
        }
    }
}