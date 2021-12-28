using System.Collections;
using UnityEngine;

public class PlayerOnHitEffect : MonoBehaviour
{
    [Header("Components that will have the collor changed")]
    [SerializeField] TextMesh[] textMeshes;
    [SerializeField] Renderer[] materials;

    [Header("configuration")]
    [SerializeField] float time = 0.1f;
    [SerializeField] Color hitColor = Color.red;

    void Awake() => PlayerHealthHandler.OnPlayerHit += OnHitHandler;
    void OnDestroy() => PlayerHealthHandler.OnPlayerHit -= OnHitHandler;

    void OnHitHandler() => StartCoroutine(ChangeColor());
    IEnumerator ChangeColor()
    {

        //Text Meshes
        Color[] oldColor_TextMeshes = new Color[textMeshes.Length];
        for(int i = 0; i < textMeshes.Length; i++)
        {
            oldColor_TextMeshes[i] = textMeshes[i].color;
            textMeshes[i].color = hitColor;
        }

        //Materials
        Color[] oldColor_Materials = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            oldColor_Materials[i] = materials[i].material.GetColor("_EmissionColor");
            materials[i].material.SetColor("_EmissionColor", hitColor);
        }

        yield return new WaitForSeconds(time);

        for (int i = 0; i < textMeshes.Length; i++)
            textMeshes[i].color = oldColor_TextMeshes[i];

        for (int i = 0; i < materials.Length; i++)
            materials[i].material.SetColor("_EmissionColor", oldColor_Materials[i]);

    }
}
