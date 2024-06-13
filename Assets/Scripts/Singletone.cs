using UnityEngine;

public abstract class Singletone<T> : MonoBehaviour where T : class
{
    public static T Instance;
    protected virtual void Start()
    {
        if (Instance == null)
        {
            Instance = GameObject.FindObjectOfType(typeof(T)) as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != gameObject)
        {
            Destroy(gameObject);
        }
    }
}