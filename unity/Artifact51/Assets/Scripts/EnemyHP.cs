using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 1;
    int hp = 1;
    // Use this for initialization
    protected virtual void Start()
    {
        hp = maxHP;
    }

    public virtual void TakeDamage(int value = 1)
    {
        hp -= value;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(this.transform.root.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            TakeDamage();
        }
        AlienBullet b = collision.transform.GetComponent<AlienBullet>();
        if (b != null)
        {
            b.Death();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            TakeDamage();
        }
        AlienBullet b = other.transform.GetComponent<AlienBullet>();
        if (b != null)
        {
            b.Death();
        }
    }
}
