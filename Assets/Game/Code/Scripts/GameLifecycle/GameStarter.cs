using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            _spawner.StartSpawning();
        }
    }
}
