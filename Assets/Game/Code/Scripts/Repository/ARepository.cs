using System.Collections.Generic;
using SensenToolkit.Rand;
using UnityEngine;

namespace Jam
{
    public class ARepository<T> : ASingleton<ARepository<T>>
    where T : ScriptableObject
    {

        [SerializeField] private List<T> _scriptableInstances;
        public List<T> All => _scriptableInstances;
        private RandomEnumerableWithVariability<T> _randomizer;
        private RandomEnumerableWithVariability<T> Randomizer => _randomizer ??= new RandomEnumerableWithVariability<T>(_scriptableInstances);

        public T GetRandom()
        {
            return Randomizer.Select();
        }
    }
}
