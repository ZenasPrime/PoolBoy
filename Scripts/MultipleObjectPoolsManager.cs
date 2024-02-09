using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenTools.PoolBoy
{
    /// <summary>
    /// Manages multiple pools of GameObject instances, allowing for efficient reuse of objects
    /// across different types. This class is designed to minimize the performance impact
    /// associated with frequently creating and destroying objects in Unity games,
    /// by maintaining separate pools for each type of prefab.
    /// </summary>
    public class MultipleObjectPoolsManager : ObjectPoolManager
    {
        [Tooltip("Array of different GameObject prefabs to manage, each with its own pool.")]
        [SerializeField] private GameObject[] itemPrefabs;
        [Tooltip("Initial number of objects to instantiate for each type of prefab in their respective pools.")]
        [SerializeField] private int initialCount = 10; // Initial count for each pool
        private Dictionary<GameObject, GameObjectPool> _pools;

        void Start()
        {
            InitializePools();
        }

        /// <summary>
        /// Initializes a pool for each prefab in the itemPrefabs array, with each pool containing
        /// a specified initial number of instances. Each pool is managed separately to facilitate
        /// efficient object reuse.
        /// </summary>
        private void InitializePools()
        {
            _pools = new Dictionary<GameObject, GameObjectPool>();

            foreach (var itemPrefab in itemPrefabs)
            {
                var pool = new GameObjectPool(itemPrefab, initialCount, transform);
                _pools.Add(itemPrefab, pool);
            }
        }

        /// <summary>
        /// Retrieves an available GameObject from the specified pool. If the pool for the requested
        /// prefab exists, an object is returned; otherwise, null is returned and an error is logged.
        /// </summary>
        /// <param name="itemPrefab">The prefab of the GameObject to retrieve from the pool.</param>
        /// <returns>An instance of the requested GameObject, or null if the pool does not exist.</returns>
        public GameObject GetObject(GameObject itemPrefab)
        {
            if (_pools.TryGetValue(itemPrefab, out var pool))
            {
                return pool.Get();
            }
            else
            {
                Debug.LogError("Pool for the requested prefab does not exist.");
                return null;
            }
        }

        /// <summary>
        /// Returns a GameObject to its corresponding pool based on the prefab. If the pool exists,
        /// the object is successfully returned; otherwise, an error is logged.
        /// </summary>
        /// <param name="itemPrefab">The prefab of the GameObject being returned.</param>
        /// <param name="item">The GameObject to return to the pool.</param>
        public void ReturnObject(GameObject itemPrefab, GameObject item)
        {
            if (_pools.TryGetValue(itemPrefab, out var pool))
            {
                pool.ReturnToPool(item);
            }
            else
            {
                Debug.LogError("Pool for the returned item does not exist.");
            }
        }
        
        /// <summary>
        /// Returns all active objects to their respective pools. Useful for bulk operations like
        /// scene resets or game state changes, ensuring all managed objects are properly reclaimed.
        /// </summary>
        public new void ReturnAllObjects()
        {
            foreach (var pool in _pools.Values)
            {
                pool.ReturnAllToPool();
            }
        }
    }
}

