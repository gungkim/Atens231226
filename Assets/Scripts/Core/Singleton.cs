using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    bool isInitialized = false;

    private static bool isShutdown = false;

    private static T instance = null;

    public static T Instance
    {
        get
        {
            if(isShutdown) 
            {   
                Debug.LogWarning("싱글톤은 이미 삭제중이다.");
                return null;                                  
            }

            if(instance == null)  
            {
                T singleton = FindAnyObjectByType<T>(); 
                if(singleton == null)                   
                {
                    GameObject obj = new GameObject();  
                    obj.name = "Singleton";             
                    singleton = obj.AddComponent<T>();  
                }
                instance = singleton;
                DontDestroyOnLoad(instance.gameObject); 
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)       
        {
            instance = this as T;  
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            if(instance != this) 
            {
                Destroy(this.gameObject); 
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!isInitialized)
        {
            OnPreInitialize();
        }
        if(mode != LoadSceneMode.Additive)
        {
            OnInitialize();
        }
    }

    protected virtual void OnPreInitialize()
    {
        isInitialized = true;
    }

    protected virtual void OnInitialize()
    {
    }


    private void OnApplicationQuit()
    {
        isShutdown = true;
    }
}

public class TestSingleton
{
    private static TestSingleton instance = null;

    public static TestSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TestSingleton();
            }
            return instance;
        }
    }

    private TestSingleton()
    {
    }
}