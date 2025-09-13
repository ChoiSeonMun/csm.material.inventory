using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_instance;
    private static bool s_isApplicationQuitting = false;

    public static T Instance
    {
        get
        {
            if (s_isApplicationQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again - returning null.");
                return null;
            }

            if (s_instance == null)
            {
                s_instance = FindFirstObjectByType<T>();

                if (s_instance == null)
                {
                    Debug.LogError($"[Singleton] An instance of {typeof(T)} is needed in the scene, but there is none.");
                }
            }

            return s_instance;
        }
    }

    protected virtual void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (s_instance != this)
        {
            Debug.LogWarning($"[Singleton] There is more than one instance of {typeof(T)} in the scene. Destroying the newest one.");
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (s_instance == this)
        {
            s_isApplicationQuitting = true;
        }
    }
}
