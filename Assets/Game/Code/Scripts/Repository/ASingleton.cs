using UnityEngine;

namespace Jam
{
    public abstract class ASingleton<T> : MonoBehaviour
    where T : Component
    {
        public static ASingleton<T> Instance { get; protected set; }
        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
