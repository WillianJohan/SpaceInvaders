using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Health health = null;
    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarLerpFill;
    [SerializeField] float fillSpeed = 10.0f;

    private void Awake()        => health.OnHealthUpdated += HandleHealthUpdated;
    private void OnDestroy()    => health.OnHealthUpdated -= HandleHealthUpdated;

    private void LateUpdate()
    {
        healthBarLerpFill.fillAmount = 
            Mathf.Lerp(
                healthBarLerpFill.fillAmount, 
                healthBar.fillAmount, 
                fillSpeed * Time.deltaTime
                );
    }

    protected virtual void HandleHealthUpdated(int currentHealth, int maxHealth)
    { 
        healthBar.fillAmount = (float) currentHealth / maxHealth;
    }
}
