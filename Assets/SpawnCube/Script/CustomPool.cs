using System.Collections.Generic;
using UnityEngine;

public class CustomPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool;
    private T _prefab;

    public CustomPool(T prefab, int poolCount)
    {
        _pool = new Queue<T>();
        _prefab = prefab;
        
        for (int i = 0; i < poolCount; i++)
        {
            var obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T TakeObject()
    {
        if (_pool.TryDequeue(out T obj))
        {
            obj.gameObject.SetActive(true);
            return obj;
        }

        return Object.Instantiate(_prefab);;
    }

    public void PutPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}