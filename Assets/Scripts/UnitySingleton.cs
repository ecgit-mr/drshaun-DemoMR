using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_oInstance = null;
    private const string SINGLETON = " (Singleton)";

    public virtual void Awake()
    {
        SetInstance();
    }

    public static T Instance()
    {
        if (m_oInstance == null)
        {
            m_oInstance = (T)FindObjectOfType(typeof(T));
            if (m_oInstance == null)
            {
                // Need to create a new GameObject to attach the singleton to.
                var singletonObject = new GameObject();
                m_oInstance = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T).ToString() + SINGLETON;
            }
        }

        return m_oInstance;
    }

    private static void SetInstance()
    {
        m_oInstance = (T)FindObjectOfType(typeof(T));
    }
}
