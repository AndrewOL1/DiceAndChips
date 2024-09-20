using UnityEngine;

#region Credit
/* This is a public gereric script form mstevenson i have modified
 * https://gist.github.com/mstevenson/4325117
 */
#endregion
public class MonoBehaviourSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{

    public bool keepAlive = false;
    private static T _instance=null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    static public bool isInstanceAlive
    {
        get { return _instance != null; }
    }

    public virtual void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = GetComponent<T>();
         if (keepAlive)
        {
            DontDestroyOnLoad(gameObject);
        }
        if (_instance == null)
        {
            return;
        }
        
    }
}

