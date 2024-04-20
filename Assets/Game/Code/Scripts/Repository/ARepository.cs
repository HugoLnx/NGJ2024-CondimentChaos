using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class ARepository<T> : ASingleton<ARepository<T>>
    where T : ScriptableObject
    {

        [SerializeField] private List<T> _scriptableInstances;
        public List<T> All => _scriptableInstances;

        public T GetRandom()
        {
            return _scriptableInstances[Random.Range(0, _scriptableInstances.Count)];
        }
    }
}
