using UnityEngine;


/*
 * usage:
 * public class MyAudioController : GenericSingletonClass<MyAudioController>; 
 * {
 * 
 * }
 */


namespace my_unity_integration
{
    public class GenericSingletonClass<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

/*docs:
 * 
 * Here is an example of how you might use the MySingleton class in your Unity project:

        public class MySingleton : GenericSingletonClass<MySingleton>
        {
            public int myValue;

            public void MyMethod()
            {
                // Your code here
            }
        }

        public class MyOtherClass : MonoBehaviour
        {
            void Start()
            {
                // Access the singleton instance
                MySingleton mySingleton = MySingleton.Instance;

                // Use the singleton instance
                mySingleton.myValue = 10;
                mySingleton.MyMethod();
            }
        }
*/