using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AgeBomb : MonoBehaviour
{
    public float agingRadius;
    public float agingDuration = 6f;
    

    private void Start()
    {
        //StartCoroutine(Smoke());
        AgeEnemies(); 
    }
    private void Update()
    {
       
    }

    private void AgeEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, agingRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                //StartCoroutine(Smoke(collider));
                //collider.GetComponent<Animator>().SetTrigger("Smoke");
                GameObject agedPrefab = Instantiate(enemy.agedEnemyPrefab, enemy.transform.position, Quaternion.identity);
                agedPrefab.GetComponent<Enemy>().currentHealth = collider.GetComponent<Enemy>().currentHealth;
                agedPrefab.GetComponent<Enemy>().healthBarFill.GetComponent<Image>().fillAmount = collider.GetComponent<Enemy>().healthBarFill.GetComponent<Image>().fillAmount; 

                Destroy(collider.gameObject);
            }
        }
    }
    
    IEnumerator Smoke(Collider2D enemy)
    {
        enemy.GetComponent<Animator>().SetTrigger("Smoke");
        yield return new WaitForSeconds(agingDuration);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agingRadius);
    }
}
