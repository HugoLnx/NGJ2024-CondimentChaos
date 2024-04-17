using UnityEngine;

namespace Sensen.Components
{
    public abstract class PrefabSimplePool<T> : PrefabPoolBase<T, T>
    where T : Component
    {
        protected override T InstantiateNew()
        {
            T instance = Instantiate(_prefab, this.transform);
            instance.name = $"[{Creations.Count+1}] {_prefab.name}";
            return instance;
        }
    }
}
