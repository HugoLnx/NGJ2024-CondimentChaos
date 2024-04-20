using UnityEngine;

namespace Jam
{
    public class FlavorSORepository : ARepository<FlavorSO>
    {

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log($"Flavor: {GetRandom().name}");
            }
        }
    }
}
