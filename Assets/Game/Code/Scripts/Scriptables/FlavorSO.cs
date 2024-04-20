using UnityEngine;

namespace Jam
{
    [CreateAssetMenu(fileName = "FlavorSO", menuName = "Jam/Flavor")]
    public class FlavorSO : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
    }
}
