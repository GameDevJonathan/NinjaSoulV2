using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timetoExplode = .5f;
    public GameObject Explosion;

    public float blastRadius;
    public LayerMask isdestructable ;
    public int damageAmount;
    public LayerMask enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timetoExplode -= Time.deltaTime;

        if (timetoExplode <= 0)
        {
            if (Explosion)
            {
                Instantiate(Explosion, transform.position, transform.rotation);

            }

            Destroy(gameObject);

            Collider2D[] objectToDamage = Physics2D.OverlapCircleAll(transform.position, blastRadius, isdestructable);

            if (objectToDamage.Length > 0)
            {
                foreach (Collider2D col in objectToDamage)
                {
                    Destroy(col.gameObject);
                }
            }

            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, blastRadius, enemies);

            foreach(Collider2D col in objectsToDamage)
            {
                EnemyHealthController enemyHealth = col.GetComponent<EnemyHealthController>();
                if(enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);
                }

            }
        }

        AudioManager.instance.PlaySFXAdjusted(4);

    }
}
