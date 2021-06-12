using UnityEngine.SceneManagement;
using UnityEngine;

namespace StartMenu
{
    public class AnyButtonLoadScene : MonoBehaviour
    {

        [SerializeField] int sceneIndexToLoad = 1;

        void Update()
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(sceneIndexToLoad);
            }
        }

    }

}