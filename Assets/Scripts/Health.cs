using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private float healthAmount;

    private float healthRemaining;
    private bool isDead = false;

    private void Start()
    {
        SetHealthToMax();
    }

    public void TakeDamage(float damage)
    {
        healthRemaining -= damage * Time.deltaTime;
        float dissolveHealth = healthAmount - healthRemaining;
        skinnedMeshRenderer.material.SetFloat("_DissolveAmount", (dissolveHealth / healthAmount) * 0.7f);
        if(healthRemaining <= 0f)
        {
            healthRemaining = 0f;
            isDead = true;
            //play death sound
        }
    }

    public bool GetIsDead() => isDead;

    public float GetHealthNormalized() => healthRemaining / healthAmount;

    public void SetHealthToMax()
    {
        healthRemaining = healthAmount;
    }

    public void ResetDissolve()
    {
        skinnedMeshRenderer.material.SetFloat("_DissolveAmount", 0.7f);
    }
}
