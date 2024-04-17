using System.Collections.Generic;
using UnityEngine;
using LnxArch;
using SensenToolkit.Pools;

namespace Sensen.Components
{
    public abstract class PrefabPoolBase<TPooled, TPrefab> : MonoBehaviour, ISimplePool<TPooled>
    where TPrefab : Component
    {
        [SerializeField] protected TPrefab _prefab;
        [SerializeField] protected int _minSize = 20;
        [SerializeField] protected int _maxCreations = 50;
        private ISimplePool<TPooled> _pool;
        public HashSet<TPooled> Creations => _pool?.Creations;

        [LnxInit]
        protected void BaseInit()
        {
            SimpleExpandablePool<TPooled> pool = new(
                factory: InstantiateNew,
                minSize: _minSize,
                maxCreations: _maxCreations,
                prefill: false
            );
            _pool = pool;
            pool.Prefill();
        }

        public TPooled Get()
        {
            return _pool.Get();
        }

        public virtual void Release(TPooled resource)
        {
            _pool.Release(resource);
        }

        protected abstract TPooled InstantiateNew();
    }
}
