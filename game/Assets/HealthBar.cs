using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class HealthBar : NetworkBehaviour
{
    
    public Fighter owner;

   
    float maxHealth;
    [SyncVar(hook = "OnChangeHealth")]
    float currentHealth;
    public RectTransform healthBar;

    void Awake()
    {
        maxHealth = owner.health;
        currentHealth = maxHealth;
    }

   void Update()
    {
        if (!isServer)
        {
            return;
        }
        if (currentHealth <= 0 && owner.currentState != FighterStates.DIED)
        {
            owner.animator.SetTrigger("DIED");
        }
        currentHealth = owner.health;
        Debug.Log(currentHealth);
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);

        //nanti pindahin script sendiri
        transform.LookAt(Camera.main.transform);
    }
    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }
}
