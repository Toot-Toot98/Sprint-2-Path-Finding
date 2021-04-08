using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 50f;

    public GameManager GameManager;

    void Start()
    {
        GameManager.GetComponent<GameManager>().EnemyHasAppeared();
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }
    
    public void Die()
    {
        GameManager.GetComponent<GameManager>().EnemyHasDied();
        Destroy(gameObject);
    }
}
