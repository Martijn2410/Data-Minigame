using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
        GameOver();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void GameOver()
    {
        if(currentHealth <= 0)
        {
           gameManager.Explode();
        }
    }

}
