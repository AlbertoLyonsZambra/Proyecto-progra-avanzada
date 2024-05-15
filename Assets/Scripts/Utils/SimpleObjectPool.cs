using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple Object Pool.
/// </summary>
public class SimpleObjectPool : MonoBehaviour
{
    #region CLASS_VARIABLES

    /// the game object we'll instantiate, it has to be a prefab
    public GameObject gameObjectToPool;
    /// the number of objects we'll add to the pool
    public int poolSize;
    /// if true, the pool will automatically add objects to the itself if needed
    public bool poolCanExpand = true;

    /// this object is just used to group the pooled objects
    protected GameObject _waitingPool;
    /// the actual object pool
    protected List<GameObject> _pooledGameObjects;


    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        Initialization();
    }

    #endregion

    #region CLASS_METHODS 

    /// <summary>
    /// Fills the object pool with the gameobject type you've specified in the inspector.
    /// </summary>
    protected virtual void Initialization()
    {
        // Initialize the list and the container
        _pooledGameObjects = new List<GameObject>();
        _waitingPool = new GameObject("[SimpleObjectPool] - " + this.gameObject.name);

        // Fill the object pool
        for (int i = 0; i < poolSize; i++)
        {
            AddOneObjectToThePool();
        }
    }

    /// <summary>
    /// This method returns one inactive object from the pool.
    /// </summary>
    /// <returns></returns>
    public virtual GameObject GetPooledGameObject()
    {
        // we go through the pool looking for an inactive object
        for (int i = 0; i < _pooledGameObjects.Count; i++)
        {

            // if we find one, we return it
            if (_pooledGameObjects[i].activeInHierarchy == false)
            {
                return _pooledGameObjects[i];
            }
        }

        // if we haven't found an inactive object (the pool is empty), and if we can extend it, we add one new object to the pool, and return it	
        if (poolCanExpand == true)
        {
            return AddOneObjectToThePool();
        }

        // if the pool is empty and can't grow, we return nothing.
        return null;
    }

    /// <summary>
    /// Adds one object of the specified type (in the inspector) to the pool.
    /// </summary>
    /// <returns></returns>
    protected virtual GameObject AddOneObjectToThePool()
    {
        // Instanciamos el prafab
        GameObject newGameObjectToPool = Instantiate(gameObjectToPool, _waitingPool.transform.position,
            gameObjectToPool.transform.rotation, _waitingPool.transform);

        // Por defecto lo desactivamos
        newGameObjectToPool.SetActive(false);

        // Le damos un nombre
        newGameObjectToPool.name = gameObjectToPool.name + " (" + _pooledGameObjects.Count + ")";

        // Guardamos el GameObject en nuestra lista
        _pooledGameObjects.Add(newGameObjectToPool);

        return newGameObjectToPool;
    }

    #endregion
}
