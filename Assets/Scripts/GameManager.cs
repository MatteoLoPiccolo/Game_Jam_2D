using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _targets;

    [SerializeField] private GameObject _player;
    [SerializeField] private BoxCollider2D _screenBoundsCollider;
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _timer;

    [Space]
    [Header("Target properties")]
    [SerializeField] private int _targetMilestone;
    [SerializeField] private int _targetCreated;

    [SerializeField] private int _score;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        UIManager.Instance.UpdateScore(_score);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _timer = _cooldown;
            _targetCreated++;

            CheckMilestoneAndIncreaseSpawnRate();

            GameObject newTarget = SpawnTargetAtRandomLocation();
            SelectRandomTarget(newTarget);
        }

        UIManager.Instance.UpdateTimer();
        UIManager.Instance.UpdateScore(_score);
    }

    private void CheckMilestoneAndIncreaseSpawnRate()
    {
        if (_targetCreated > _targetMilestone)
        {
            _targetMilestone += 10;
            _cooldown -= 0.5f;
        }
    }

    private void SelectRandomTarget(GameObject newTarget)
    {
        int randomIndex = Random.Range(0, _targets.Length);
        newTarget.GetComponent<SpriteRenderer>().sprite = _targets[randomIndex];
    }

    private GameObject SpawnTargetAtRandomLocation()
    {
        GameObject newTarget = Instantiate(_targetPrefab);
        float randomX = Random.Range(_screenBoundsCollider.bounds.min.x, _screenBoundsCollider.bounds.max.x);

        newTarget.transform.position = new Vector3(randomX, _screenBoundsCollider.gameObject.transform.position.y);
        return newTarget;
    }

    public void UpdateScore()
    {
        _score++;
        UIManager.Instance.UpdateScore(_score);
    }

    public void GameOver()
    {
        _player.SetActive(false);
        UIManager.Instance.GameOver();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}