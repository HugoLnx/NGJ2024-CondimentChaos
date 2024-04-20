using UnityEngine;

namespace Jam
{
    public class FlavorSORepository : ARepository<FlavorSO>
    {
        public static FlavorSORepository Repo => (FlavorSORepository)Instance;
    }
}
