using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;

namespace StartMenu
{
    public class AnyButtonLoadScene : MonoBehaviour
    {

        [SerializeField] int sceneIndexToLoad = 1;

        public static event Action OnAnyButtonPressed;

        void Update()
        {
            if (Input.anyKey)
            {
                OnAnyButtonPressed?.Invoke();
                StartCoroutine(LoadScene());
            }
        }

        IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(sceneIndexToLoad);
        }

    }

}