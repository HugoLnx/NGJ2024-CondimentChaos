using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Jam
{

    public class FoodSORepository : ARepository<FoodSO>
    {

        public static FoodSORepository Repo => (FoodSORepository)Instance;
        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log($"Food: {GetRandom().name}");
            }
        }
    }
}
