using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 1;
    int hp = 1;
    // Use this for initialization
    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(int value = 1)
    {
        hp--;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            TakeDamage();
        }
    }
}
