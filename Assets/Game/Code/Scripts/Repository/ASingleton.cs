using UnityEngine;

namespace Jam
{
    public abstract class ASingleton<T> : MonoBehaviour
    where T : ASingleton<T>
    {
        public static T Instance { get; protected set; }
        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
