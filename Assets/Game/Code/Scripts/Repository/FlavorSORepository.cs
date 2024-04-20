using UnityEngine;

namespace Jam
{
    public class FlavorSORepository : ARepository<FlavorSO>
    {
        public static new FlavorSORepository Instance => Instance;

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log($"Flavor: {GetRandom().name}");
            }
        }
    }
}
