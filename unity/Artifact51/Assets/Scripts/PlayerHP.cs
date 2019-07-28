using UnityEngine;
using System.Collections;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 1;
    int hp = 1;
    // Use this for initialization
    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage()
    {
        hp--;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        LevelManager.GameOver();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ebullet")
        {
            TakeDamage();
        }
    }
}
