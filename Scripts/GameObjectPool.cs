using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenTools.PoolBoy
{
    /// <summary>
    /// Manages a pool of GameObject instances to optimize performance by reusing objects.
    /// This class allows for the pre-instantiation and recycling of GameObjects based on a specified prefab,
    /// minimizing the overhead associated with frequent instantiation and destruction.
    /// </summary>
    public class GameObjectPool 
    {
        // The prefab from which pool objects are instantiated. This serves as the template for all objects in the pool.
        private readonly GameObject _prefab;
        // A queue of inactive GameObjects ready to be reused, helping manage the pool efficiently by reusing objects.
        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        // A list of GameObjects that are currently in use. This helps track which objects are active at any given time.
        private readonly List<GameObject> _activeObjects = new List<GameObject>(); 
        // The parent transform under which all instantiated objects will be organized. This helps keep the scene hierarchy clean.
        private readonly Transform _parentTransform;
        
        /// <summary>
        /// Initializes a new instance of the GameObjectPool class.
        /// </summary>
        /// <param name="prefab">The GameObject prefab to be used for pooling.</param>
        /// <param name="initialCount">The initial number of objects to instantiate and add to the pool.</param>
        /// <param name="parentTransform">The parent transform under which all pooled objects will be instantiated.</param>
        public GameObjectPool(GameObject prefab, int initialCount = 0, Transform parentTransform = null)
        {
            this._prefab = prefab;
            this._parentTransform = parentTransform;

            // Pre-instantiate objects.
            for (int i = 0; i < initialCount; i++)
            {
                GameObject someGameObject = AddObject();
            }
        }
        
        /// <summary>
        /// Instantiates a new GameObject from the prefab, optionally activates it, and adds it to the pool.
        /// </summary>
        /// <returns>The instantiated GameObject.</returns>
        private GameObject AddObject()
        {
            var newObject = Object.Instantiate(_prefab, _parentTransform);
            newObject.SetActive(false);
            _pool.Enqueue(newObject);
            //Debug.Log($"Adding new object {newObject} to pool and returning it to the caller. {_pool.Count} objects in pool.");
            return newObject;
        }

        /// <summary>
        /// Retrieves an active GameObject from the pool. If the pool is empty, a new object is instantiated.
        /// </summary>
        /// <returns>An active GameObject.</returns>
        public GameObject Get()
        {
            if (_pool.Count == 0)
            {
                Debug.Log("Pool is empty. Adding new object.");
                AddObject();
            }

            var obj = _pool.Dequeue();
            obj.SetActive(true);
            _activeObjects.Add(obj);
            return obj;
        }

        /// <summary>
        /// Returns a GameObject to the pool, deactivating it and making it available for future use.
        /// </summary>
        /// <param name="objectToReturn">The GameObject to return to the pool.</param>
        public void ReturnToPool(GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);
            _pool.Enqueue(objectToReturn);
            _activeObjects.Remove(objectToReturn);
        }
        
        /// <summary>
        /// Deactivates all active objects and returns them to the pool. This is useful for bulk operations
        /// such as resetting the scene or clearing objects between levels.
        /// </summary>
        public void ReturnAllToPool()
        {
            foreach (var activeObject in _activeObjects)
            {
                activeObject.SetActive(false);
                _pool.Enqueue(activeObject);
            }
            _activeObjects.Clear();
        }
    }
}
