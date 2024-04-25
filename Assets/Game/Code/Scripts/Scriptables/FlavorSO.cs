using UnityEngine;

namespace Jam
{
    [CreateAssetMenu(fileName = "FlavorSO", menuName = "Jam/Flavor")]
    public class FlavorSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Sprite FountainSprite { get; private set; }
    }
}
