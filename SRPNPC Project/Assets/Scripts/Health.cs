using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;

    private float currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        currentHealth -= amount;

        if (CurrentHpPct <= 0)
            Die();
    }

    private void Update()
    {
        if (currentHealth < startingHealth) 
        {

            currentHealth += 0.01f;
            OnHPPctChanged(CurrentHpPct);
            Debug.Log(currentHealth);
        
        }
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }
}