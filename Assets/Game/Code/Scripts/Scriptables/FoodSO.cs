using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [CreateAssetMenu(fileName = "FoodSO", menuName = "Jam/Food")]
    public class FoodSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Texture { get; private set; }
        [field: SerializeField] public Sprite FlavorTexture { get; private set; }
    }
}
