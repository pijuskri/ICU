using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Spawner
{
    public class SpawnerLocationHelper
    {
        private readonly Vector3[] _positions;
        private readonly Random _random = new Random();
        private readonly List<int> _unusedPositionIndices;

        public SpawnerLocationHelper(Vector3[] positions)
        {
            _positions = positions;
            _unusedPositionIndices = Enumerable.Range(0, _positions.Length).ToList();
        }

        public Vector3? GetPosition()
        {
            if (_unusedPositionIndices.Count == 0)
            {
                Debug.Log("Spawned all da bottol already mpro");
                return null;
            }

            var randIndex = _unusedPositionIndices
                .OrderBy(x => _random.Next())
                .First();

            _unusedPositionIndices.Remove(randIndex);

            return _positions[randIndex];
        }
    }
}