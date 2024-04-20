using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Jam
{

    public class FoodSORepository : ARepository<FoodSO>
    {

        public static FoodSORepository Repo => (FoodSORepository)Instance;
    }
}
