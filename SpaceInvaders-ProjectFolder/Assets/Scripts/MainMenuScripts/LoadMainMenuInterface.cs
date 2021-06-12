using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class LoadMainMenuInterface : MonoBehaviour
    {
        [Header("Load Objectes Reference")]
        [SerializeField] float totaltimeForLoadInterface = 2.0f;
        [SerializeField] GameObject[] objectsToActivate;
        
        [Header("Load Info Reference")]
        [SerializeField] TextMesh HiScoreText;
        [SerializeField] LoadInfo []AlienScorePoints;

        #region LoadInfo Struct

        [Serializable]
        struct LoadInfo
        {
            [SerializeField] AlienType type;
            [SerializeField] TextMesh ScoreText;

            public void LoadScoreText()
            {
                ScoreText.text =
                    (type == AlienType.Boss) ?
                    "??????????" :
                    "=   " + ((int)type).ToString() + "  Points";
            }
        }

        #endregion

        void Start()
        {
            HiScoreText.text = "Hi-Score = " + Data.Load().ToString();
            
            foreach (GameObject item in objectsToActivate)
                item.SetActive(false);

            foreach (LoadInfo info in AlienScorePoints)
                info.LoadScoreText();

            StartCoroutine(ActivateObjects());
        }

        IEnumerator ActivateObjects()
        {
            float delay = totaltimeForLoadInterface / objectsToActivate.Length;

            foreach (GameObject item in objectsToActivate)
            {
                item.SetActive(true);
                yield return new WaitForSeconds(delay);
            }
            
            yield return null;

        }

    }
}