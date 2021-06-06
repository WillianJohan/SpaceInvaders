using UnityEngine;

public class ChangeMaterialColorOverTime : MonoBehaviour
{
    [SerializeField] Gradient colorGradient;
    [SerializeField] float velocity = 0.5f;
    
    float elapsedTime = 0;
    Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime * velocity;
        Color newColor = colorGradient.Evaluate(elapsedTime % 1);
        material.SetColor("_EmissionColor", newColor);
    }
}
