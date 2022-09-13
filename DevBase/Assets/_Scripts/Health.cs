using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent onDeath;
    public static Action<GameObject> OnDeathh;
    public event Action OnDamageTaken;
    public event Action OnDeath;
    public event Action<float> onValueUpdate;

    [SerializeField] string Tag;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;

    public float MaxHealth { get => maxHealth; }
    
    public void addLife(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, MaxHealth);
        onValueUpdate?.Invoke(currentHealth);
    }

    public void die()
    {
        OnDeathh?.Invoke(gameObject);
        onDeath?.Invoke();
    }

    private void Loose()
    {
        GameStateManager.SetState(GameState.fail);
    }

    public float getCurrent()
    {
        return currentHealth;
    }

    public void getDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0f, MaxHealth);

        onValueUpdate?.Invoke(currentHealth);
        OnDamageTaken?.Invoke();

        if (currentHealth == 0f)
            die();
    }

    public float getMax()
    {
        return MaxHealth;
    }

    public float getPercent()
    {
        return currentHealth / MaxHealth;
    }

    public bool tagCheck(string tag)
    {
        if (Tag == tag)
            return true;
        else
            return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }
}
