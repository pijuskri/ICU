using UnityEngine;

namespace Spawner
{
    public class RandomObjectSpawner : MonoBehaviour
    {
        // bottle
        [SerializeField] private GameObject bottle;

        // pass positions here
        private readonly SpawnerLocationHelper _bottleLocationHelper =
            new SpawnerLocationHelper(new[]
            {
                // basically spawns in front of player spawn for testing
                new Vector3(0.287f, 0.118f, 4.9f),
                // table
                new Vector3(6.47f, 0.914f, -2.36f)
            });

        // now copy paste this method for all entities
        public void SpawnBottles(int amount = 1)
        {
            for (var i = 0; i < amount; i++)
            {
                var pos = _bottleLocationHelper.GetPosition();

                if (!pos.HasValue) break;

                Instantiate(
                    bottle,
                    pos.Value,
                    Quaternion.identity
                );
            }
        }
    }
}