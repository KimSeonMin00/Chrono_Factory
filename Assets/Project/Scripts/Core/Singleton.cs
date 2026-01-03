using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool m_bIsQuitting = false;
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (m_bIsQuitting)
            {
                return null;
            }
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if(_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        m_bIsQuitting = true;
    }
}
