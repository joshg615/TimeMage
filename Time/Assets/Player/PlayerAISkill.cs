using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAISkill : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float attackRange;
    public float skillCooldown;
    public float projectileCooldown;
    public GameObject[] enemyList;

    private Transform target;
    private Animator anim;
    private bool isAttacking;
    private bool isUsingSkill;
    private bool isOnCooldown;
    private bool isProjectileOnCooldown;
    Rigidbody2D rb;
    public float dragSpeed = 100f; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        if (!isOnCooldown)
        {
            FindNearestEnemy();
            if (target != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, target.position);
                if (distanceToEnemy <= attackRange)
                {
                    Attack();
                }
                else
                {
                    MoveToEnemy();
                }
            }
        }

        if (!isProjectileOnCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            }
            ShootProjectile();
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude;

        rb.drag = currentSpeed * dragSpeed;
    }

    private void FindNearestEnemy()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyList.Length > 0)
        {
            float distance = Mathf.Infinity;
            foreach (GameObject enemy in enemyList)
            {
                float newDistance = Vector2.Distance(transform.position, enemy.transform.position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    target = enemy.transform;
                }
            }
        }
    }

    private void MoveToEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime);
        //anim.SetBool("isMoving", true);
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        //anim.SetTrigger("attack");
        yield return new WaitForSeconds(3f);//anim.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = target.position - transform.position;
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        isProjectileOnCooldown = true;
        StartCoroutine(ProjectileCooldownCoroutine());
    }

    private IEnumerator ProjectileCooldownCoroutine()
    {
        yield return new WaitForSeconds(projectileCooldown);
        isProjectileOnCooldown = false;
    }

    private IEnumerator SkillCoroutine()
    {
        isOnCooldown = true;
        isUsingSkill = true;
        // Perform skill action here
        yield return new WaitForSeconds(skillCooldown);
        isOnCooldown = false;
        isUsingSkill = false;
    }
}

