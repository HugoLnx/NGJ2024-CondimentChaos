using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [CreateAssetMenu(fileName = "FoodSO", menuName = "Jam/Food")]
    public class FoodSO : ScriptableObject
    {
        [field: SerializeField] public Texture2D Texture { get; private set; }
        [field: SerializeField] public Texture2D FlavorTexture { get; private set; }
    }
}
