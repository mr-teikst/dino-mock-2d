using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public float invincibleTime = 1.2f; 
    public Image healthFill;              
    public Color hitColor = new Color(1f, 0.4f, 0.4f);

    public int Lives { get; private set; }
    public bool IsDead { get; private set; }

    SpriteRenderer sr;
    Animator anim;
    bool invincible;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Lives = maxLives;
        UpdateBar();
    }

    public void TakeDamage(int amount)
    {
        if (invincible || IsDead) return;

        Lives = Mathf.Max(Lives - amount, 0);
        UpdateBar();

        if (Lives == 0) Die();
        else
        {
            anim.SetTrigger("Hit");
            StartCoroutine(HitFlash());
        }
    }

    void UpdateBar()
    {
        if (healthFill != null)
            healthFill.fillAmount = (float)Lives / maxLives;
    }

    IEnumerator HitFlash()
    {
        invincible = true;
        float t = 0f;
        while (t < invincibleTime)
        {
            sr.color = hitColor;
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
        }
        sr.enabled = true;
        sr.color = Color.white;
        invincible = false;
    }

    void Die()
    {
        IsDead = true;
        anim.SetTrigger("Dead");
        GetComponent<PlayerController>().enabled = false;   
        StartCoroutine(StopGame());
    }

    
    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.GameOver();
    }
}
