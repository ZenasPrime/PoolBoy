using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenTools.PoolBoy
{
    /// <summary>
    /// Provides an abstract base for object pool managers within Unity projects.
    /// Object pools are used to manage a collection of reusable GameObjects,
    /// minimizing the performance costs associated with the frequent creation and destruction of objects.
    /// </summary>
    public abstract class ObjectPoolManager : MonoBehaviour
    {
        /// <summary>
        /// Initializes the object pool.
        /// This method should be overridden by subclasses to implement specific initialization logic,
        /// such as pre-instantiating a certain number of objects.
        /// </summary>
        protected virtual void InitializePool() { }

        /// <summary>
        /// Retrieves a GameObject from the pool.
        /// This method should be overridden by subclasses to return an appropriate GameObject instance.
        /// If the pool is empty or if specific conditions are not met, this method may return null.
        /// </summary>
        /// <returns>A GameObject from the pool or null if no object is available.</returns>
        public virtual GameObject GetObject() { return null; }
        
        /// <summary>
        /// Retrieves a GameObject from the pool based on a name.
        /// This method should be overridden by subclasses to return an instance of the GameObject
        /// that matches the provided name.
        /// If the pool does not have an available object of the requested type, this method may return null.
        /// </summary>
        /// <param name="poolName">The name used to identify the type of object to retrieve from the pool.</param>
        /// <returns> A GameObject from the pool that matches the name or null if no object is available.</returns>
        public virtual GameObject GetObjectFromPool(string poolName) { return null; }
        
        /// <summary>
        /// Returns a GameObject to the pool.
        /// Subclasses should override this method to implement the logic for returning objects to the pool,
        /// such as deactivating them and placing them back into a collection for future use.
        /// </summary>
        /// <param name="gameObjectToReturn">The GameObject to return to the pool.</param>
        public virtual void ReturnObject(GameObject gameObjectToReturn) { }
        
        /// <summary>
        /// Returns a GameObject to the pool based on the name of the specific pool.
        /// </summary>
        /// <param name="poolName">The name of the pool name to return the object to.</param>
        /// <param name="gameObjectToReturn">The GameObject to return to the pool.</param>
		public virtual void ReturnObjectToPool(string poolName, GameObject gameObjectToReturn) { }

        /// <summary>
        /// Returns all active objects to the pool.
        /// This method can be used to reset the pool and reclaim all objects.
        /// Subclasses should override it to implement specific logic for returning all managed objects to the pool.
        /// </summary>
        public virtual void ReturnAllObjects() { }
    }
}
