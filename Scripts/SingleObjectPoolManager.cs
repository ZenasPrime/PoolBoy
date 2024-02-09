using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenTools.PoolBoy
{
    /// <summary>
    /// Manages a pool of identical GameObject instances to minimize the performance overhead
    /// associated with frequently creating and destroying objects. This class extends
    /// ObjectPoolManager to implement a single-object type pool using a predefined prefab.
    /// </summary>
    public class SingleObjectPoolManager : ObjectPoolManager
    {
        [Tooltip("The prefab of the GameObject that this pool will manage.")]
        [SerializeField] private GameObject itemPrefab;
        [Tooltip("Initial number of objects to instantiate in the pool.")]
        [SerializeField] private int initialCount = 10;
        
        // The pool of GameObject instances managed by this class.
        private GameObjectPool _objectPool;

        void Start()
        {
            InitializePool();
        }

        /// <summary>
        /// Initializes the object pool with the specified prefab and count.
        /// </summary>
        private void InitializePool()
        {
            _objectPool = new GameObjectPool(itemPrefab, initialCount, transform);
        }

        /// <summary>
        /// Retrieves an available GameObject from the pool. If no objects are available,
        /// a new one may be instantiated depending on the implementation of the GameObjectPool.
        /// </summary>
        /// <returns>An instance of the pooled GameObject, ready for use.</returns>
        public new GameObject GetObject()
        {
            return _objectPool.Get();
        }

        /// <summary>
        // /// Returns a GameObject to the pool, making it available for future use. This method
        // /// should be called instead of destroying the object when it is no longer needed.
        // /// </summary>
        // /// <param name="item">The GameObject to return to the pool.</param>
        public new void ReturnObject(GameObject item)
        {
            _objectPool.ReturnToPool(item);
        }
        
        /// <summary>
        /// Returns all active objects to the pool. This is useful for resetting the scene
        /// or cleaning up when the game state changes.
        /// </summary>
        public new void ReturnAllObjects()
        {
            _objectPool.ReturnAllToPool();
        }
    }
}
