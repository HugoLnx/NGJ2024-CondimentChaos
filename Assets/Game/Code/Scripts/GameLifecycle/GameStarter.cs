using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jam
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _startSfx;
        private bool _inputedSomething;

        private IEnumerator Start()
        {
            UI.Instance.ShowTitleCover();
            yield return new WaitForSeconds(0.5f);
            _inputedSomething = false;
            yield return new WaitUntil(() => _inputedSomething);
            AudioPlayer.Instance.PlaySFX(_startSfx);
            yield return new WaitForSeconds(0.15f);
            UI.Instance.HideTitleCover();

            GameTime.Instance.StartTimer();
            EnemySpawner.Instance.StartSpawning();
            GameTime.Instance.OnEnded += () => EnemySpawner.Instance.StopSpawning();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            {
                _inputedSomething = true;
            }
        }
    }
}
