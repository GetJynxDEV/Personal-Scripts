using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //This will determine where the Attack will originate from
    [SerializeField] private Transform attackOrigin;
    //This will determine how far the player can attack
    [SerializeField] private float attackRange;
    //This will be the LayerMask of the enemy
    [SerializeField] private LayerMask enemyMask;

    public bool isAttack;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float timer;

    Animator anim;

    stats dmg;

    #region START METHOD
    private void Start()
    {
        dmg = GetComponent<stats>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isAttack)
        {
            timer += Time.deltaTime;

            if (timer >= 0.5f)
            {
                anim.SetBool("isAttacking", false);
            }

            if (timer >= attackCooldown)
            {
                isAttack = false;
                timer = 0;
            }
        }
    }

    #endregion

    #region ATTACK INPUT

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttack)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRange, enemyMask);

            anim.SetInteger("movement", 5);
            anim.SetBool("isAttacking", true);

            foreach (var enemy in enemiesInRange)
            {
                stats targetStats = enemy.gameObject.GetComponent<stats>();
                DamageCalculation damageCalc = GameObject.FindAnyObjectByType<DamageCalculation>();

                if (targetStats != null)
                {
                    targetStats.takeDamage(damageCalc.physicalDamage(targetStats.currentArmor, targetStats.damage));
                }
            }

            isAttack = true;
        }
    }

    #endregion

    #region RAYCAST

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRange);
    }

    #endregion
}
