using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    //Use this Enemy Follow if no NavMesh is required or if game is 2D
    
    [Header("TARGET")]
    //Attach the Target that the enemy will follow
    public Transform target;

    [Header("MOVEMENTS")]
    public float attackRange;

    [Header("AUDIO SETTINGS")]
    private float shoutTimer;
    private float shoutInterval;

    public float rotationSpeed;

    [Header("Scripts")]
    stats status;
    stats targetStatus;
    Animator anim;
    EnemyAttack attack;


    private void Start()
    {
        attack = GetComponent<EnemyAttack>();
        status = GetComponent<stats>();
        anim = GetComponentInChildren<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetStatus = target.GetComponent<stats>();

    }

    private void Update()
    {
        if (target == null || targetStatus.currentHealth <= 0) return;

        //Move the enemy towards the target
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            //Move toward the Player
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * status.speed * Time.deltaTime;

            //Rotate smoothly towards the Player
            Vector3 lookDirection = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //Play Animation
            anim.SetBool("isAttacking", false);
        }

        else
        {
            //Stop moving and Attack
            anim.SetBool("isAttacking", true);
        }
    }
}
