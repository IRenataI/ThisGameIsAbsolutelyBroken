using System.Collections;
using UnityEngine;

public class FloatingObjectsSpawner : MonoBehaviour
{
    [Header("Настройки объектов")]
    public GameObject[] prefabs;
    public float[] spawnChances;

    [Header("Настройки времени")]
    public Vector2 spawnIntervalRange = new Vector2(1f, 3f);
    public Vector2 objectLifetimeRange = new Vector2(3f, 6f);

    [Header("Настройки полета")]
    public Collider floatingArea;
    public float floatSpeed = 1f;
    public float rotationSpeed = 10f;
    public float directionChangeInterval = 1f;

    private bool isObjectActive = false;
    private GameObject lastSpawnedPrefab;

    private void Start()
    {
        if (floatingArea == null)
        {
            Debug.LogError("Не назначен коллайдер области полета!");
            return;
        }

        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (!isObjectActive)
            {
                GameObject prefabToSpawn = GetRandomPrefab();
                if (prefabToSpawn != null)
                {
                    Vector3 spawnPosition = GetRandomPointInCollider();
                    GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

                    isObjectActive = true;
                    lastSpawnedPrefab = prefabToSpawn;

                    float objectLifetime = Random.Range(objectLifetimeRange.x, objectLifetimeRange.y);
                    StartCoroutine(FloatAndDestroyObject(spawnedObject, objectLifetime));
                }
            }

            float spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetRandomPrefab()
    {
        float totalChance = 0;
        foreach (float chance in spawnChances) totalChance += chance;

        float randomPoint = Random.value * totalChance;
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (randomPoint < spawnChances[i] && prefabs[i] != lastSpawnedPrefab)
            {
                return prefabs[i];
            }
            randomPoint -= spawnChances[i];
        }
        return null;
    }

    private Vector3 GetRandomPointInCollider()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(-0.5f, 0.5f),
            Random.Range(-0.5f, 0.5f),
            Random.Range(-0.5f, 0.5f)
        );

        Vector3 localPoint = floatingArea.bounds.center + Vector3.Scale(randomPoint, floatingArea.bounds.size);
        Vector3 worldPoint = floatingArea.transform.TransformPoint(localPoint);
        return floatingArea.ClosestPoint(worldPoint);
    }

    private IEnumerator FloatAndDestroyObject(GameObject obj, float objectLifetime)
    {
        float time = 0f;
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        Vector3 randomRotationAxis = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        float timeToChangeDirection = directionChangeInterval;

        while (time < objectLifetime)
        {
            Vector3 nextPosition = obj.transform.position + randomDirection * floatSpeed * Time.deltaTime;
            if (!floatingArea.bounds.Contains(nextPosition))
            {
                if (nextPosition.x < floatingArea.bounds.min.x || nextPosition.x > floatingArea.bounds.max.x)
                    randomDirection.x = -randomDirection.x;

                if (nextPosition.y < floatingArea.bounds.min.y || nextPosition.y > floatingArea.bounds.max.y)
                    randomDirection.y = -randomDirection.y;

                if (nextPosition.z < floatingArea.bounds.min.z || nextPosition.z > floatingArea.bounds.max.z)
                    randomDirection.z = -randomDirection.z;
            }
            else
            {
                obj.transform.Translate(randomDirection * floatSpeed * Time.deltaTime, Space.World);
            }

            obj.transform.Rotate(randomRotationAxis * rotationSpeed * Time.deltaTime, Space.World);

            timeToChangeDirection -= Time.deltaTime;
            if (timeToChangeDirection <= 0f)
            {
                randomDirection = new Vector3(
                    randomDirection.x + Random.Range(-0.5f, 0.5f),
                    randomDirection.y + Random.Range(-0.5f, 0.5f),
                    randomDirection.z + Random.Range(-0.5f, 0.5f)
                ).normalized;
                timeToChangeDirection = directionChangeInterval;
            }

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
        isObjectActive = false;
    }
}
