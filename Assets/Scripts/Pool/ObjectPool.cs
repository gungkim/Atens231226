using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : RecycleObject
{
    public GameObject originalPrefab;

    public int poolSize = 64;

    T[] pool;

    Queue<T> readyQueue;

    public void Initialize()
    {
        if( pool == null )
        {
            pool = new T[poolSize];             
            readyQueue = new Queue<T>(poolSize);

            GenerateObjects(0, poolSize, pool);
        }
        else
        {
            foreach( T obj in pool ) 
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    public T GetObject(Vector3? position = null, Vector3? eulerAngle = null)
    {
        if (readyQueue.Count > 0)
        {
            T comp = readyQueue.Dequeue();  
            comp.gameObject.SetActive(true);
            comp.transform.position = position.GetValueOrDefault();
            comp.transform.Rotate(eulerAngle.GetValueOrDefault()); 
            OnGetObject(comp);           
            return comp;                 
        }
        else
        {
            ExpandPool();
            return GetObject(position, eulerAngle);
        }
    }

    protected virtual void OnGetObject(T component)
    {
    }

    void ExpandPool()
    {
        Debug.LogWarning($"{gameObject.name} 풀 사이즈 증가. {poolSize} -> {poolSize * 2}");

        int newSize = poolSize * 2;    
        T[] newPool = new T[newSize];  
        for(int i = 0; i<poolSize; i++)
        {
            newPool[i] = pool[i];
        }

        GenerateObjects(poolSize, newSize, newPool); 

        pool = newPool;    
        poolSize = newSize;
    }


    void GenerateObjects(int start, int end, T[] results)
    {
        for (int i = start; i < end; i++)
        {
            GameObject obj = Instantiate(originalPrefab, transform);  
            obj.name = $"{originalPrefab.name}_{i}";    

            T comp = obj.GetComponent<T>();
            comp.onDisable += () => readyQueue.Enqueue(comp);

            results[i] = comp;   
            obj.SetActive(false);
        }
    }
}
