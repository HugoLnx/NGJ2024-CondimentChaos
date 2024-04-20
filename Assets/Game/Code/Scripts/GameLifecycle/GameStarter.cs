using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class GameStarter : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            GameTime.Instance.StartTimer();
            EnemySpawner.Instance.StartSpawning();
        }
    }
}
