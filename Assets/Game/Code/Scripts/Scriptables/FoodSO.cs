using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using UnityEngine;

[Serializable]
public struct FlavorTexture
{
    public FlavorSO Flavor;
    public Sprite Sprite;
}
namespace Jam
{
    [CreateAssetMenu(fileName = "FoodSO", menuName = "Jam/Food")]
    public class FoodSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Texture { get; private set; }
        [field: SerializeField] public List<FlavorTexture> FlavorTextures { get; private set; }
    }
}
