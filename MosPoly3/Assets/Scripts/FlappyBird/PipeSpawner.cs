using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 0.45f;
    [SerializeField] private GameObject _pipe;

    private float _timer;
    private bool _isSpawning = false;

    private void Start()
    {
        StartSpawning();
    }

    private void Update()
    {
        if (_isSpawning && _timer > _maxTime)
        {
            SpawnPipe();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    public void StartSpawning()
    {
        _isSpawning = true;
        _timer = 0;
    }

    public void ResetSpawner()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-_heightRange, _heightRange));
        GameObject pipe = Instantiate(_pipe, spawnPos, Quaternion.identity, transform);

        Destroy(pipe, 10f);
    }
}
