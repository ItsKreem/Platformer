using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : GameManager
{
    public delegate void HitEvent(GameObject source);
    public HitEvent OnHit;

    public delegate void ResetEvent();
    public ResetEvent OnHitReset;


    public float MaxHealth = 10f;
    public Cooldown Invulnerability;

    public AudioSource HurtAudio;

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

    private float _currentHealth = 10f;
    private bool _canDamage = true;


    // Start is called before the first frame update
    void Start()
    {
        ResetHealthtoMax();
    }

    // Update is called once per frame
    void Update()
    {
        ResetInvulnerable();
    }

    void ResetInvulnerable()
    {
        if (_canDamage)
            return;

        if (Invulnerability.IsOnCooldown && _canDamage == false)
            return;

        _canDamage = true;
        OnHitReset?.Invoke();
    }

    public void Damage(float damage, GameObject source) 
    {
        if (!_canDamage)
            return;

        _currentHealth -= damage;
        if (HurtAudio != null)
        {
            GameObject.Instantiate(HurtAudio, transform.position, Quaternion.identity);
        }

        if (_currentHealth <= 0f )
        {
            _currentHealth = 0f;
            Die();
        }

        Invulnerability.StartCooldown();
        _canDamage = false;

        OnHit?.Invoke(source);
    }

    public void Die()
    {
        Debug.Log("This has been triggereed.");
        if (this.gameObject.CompareTag("Player") == true)
        {
            Death();
        }
        Destroy(this.gameObject);
    }

    void ResetHealthtoMax() 
    {
        _currentHealth = MaxHealth;
    }
}
