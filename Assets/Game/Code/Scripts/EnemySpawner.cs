using System.Collections;
using System.Collections.Generic;
using SensenToolkit.Gizmosx;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private List<Customer> _customerPrefabs;
    public List<Vector2> spawnpoints;
    private List<Vector2> usedSpawns = new();
    public float spawnCooldown = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => spawnpoints.Count > 0);
            yield return new WaitForSeconds(spawnCooldown);
            Customer customer = Instantiate(GetRandomCustomer());
            Vector2 chosenSpawn = GetRandomSpawnpoint();
            customer.OnFinish += () => FreeSpawnpoint(chosenSpawn);
            customer.transform.position = chosenSpawn;
        }
    }

    private Vector2 GetRandomSpawnpoint()
    {
        Vector2 randomPoint = spawnpoints[Random.Range(0, spawnpoints.Count)];
        spawnpoints.Remove(randomPoint);
        usedSpawns.Add(randomPoint);
        return randomPoint;
    }

    private void FreeSpawnpoint(Vector2 spawnpointToFree)
    {
        usedSpawns.Remove(spawnpointToFree);
        spawnpoints.Add(spawnpointToFree);
    }

    private Customer GetRandomCustomer()
    {
        return _customerPrefabs[Random.Range(0, _customerPrefabs.Count)];
    }

    private void OnDrawGizmosSelected()
    {
        using (Gizmosx.Color(Color.yellow))
        {
            foreach (Vector2 spawn in spawnpoints)
            {
                Gizmos.DrawWireSphere(spawn, 0.3f);
            }
        }
    }

}
