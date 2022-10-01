using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://levelup.gitconnected.com/tip-of-the-day-manager-classes-singleton-pattern-in-unity-1bf3aafe9430
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<T>(true);
                if (instance == null)
                {
                    //search for a prefab of a singleton (might contain extra data)
                    GameObject go = GetPrefab(Resources.LoadAll("Prefabs/Singletons", typeof(GameObject)));
                    if (go != null)
                    {
                        GameObject go2 = Instantiate(go);
                        instance = go2.GetComponent<T>();
                    }
                    else
                    {
                        GameObject newGO = new GameObject();
                        instance = newGO.AddComponent<T>();
                    }

                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance)
        {
            Debug.Log("Destroying instance..");
            Destroy(this.gameObject);
        }
        else
            instance = this as T;
    }

    public static GameObject GetPrefab(UnityEngine.Object[] prefabs)
    {
        if (prefabs == null)
            return null;

        foreach (GameObject prefab in prefabs)
        {
            var comp = prefab.GetComponent<T>();
            if (comp != null) return prefab;
        }
        return null;
    }
}
