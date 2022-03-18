using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class AbilityBaseClass : MonoBehaviour
{
    [Header("State indicators")]
    #region State Indicators
    public bool readyToCast;
    public bool casting;
    public bool recharging;
    #endregion

    [Space(10)]
    [Header("Target type")]
    #region Target type
    public bool targetPlayer;
    public bool targetEnemyUnderCursor;
    public bool targetEnemiesInRadius;
    public bool targetNothing;
    public bool targetEveryone;
    #endregion

    [Space(10)]
    [Header("Ability type")]
    #region Ability type
    public bool heal;
    public bool damage;
    #endregion

    [Space(10)]
    [Header("Ability positioning")]
    #region Ability positioning
    public bool onPlayer;
    public bool onCursor;
    public bool onEnemyUnderCursor;
    #endregion

    [Space(10)]
    [Header("Ability values")]
    #region Ability values
    public int healAmount;
    public int damageAmount;
    public float coolDown;
    public float radius;
    public int energyCost;
    #endregion

    [Space(10)]
    [Header("Component references")]
    #region Component references
    public bool shouldReferencePlayer;
    Animator animator;
    #endregion

    
    #region Player References
    GameObject player;
    PlayerStats playerStats;
    Rigidbody2D playerRb2D;
    #endregion
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (shouldReferencePlayer == true)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerStats = player.GetComponent<PlayerStats>();
            playerRb2D = player.GetComponent<Rigidbody2D>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (readyToCast == true && casting == false  && recharging == false)
        {
            //Positioning functions
            MoveOnPlayer();//implemented
            MoveOnCursor();//implemented
            MoveOnEnemyUnderCursor();//implemented

            //Targetting functions
            TargetPlayer();//implemented
            TargetEnemyUnderCursor();//implemented
            TargetNothing();//implemented
            TargetEveryone();
            TargetEnemiesInRadius();
        }
     
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    #region Targeting functions
    void TargetPlayer()
    {
        if (targetPlayer == true)//this ability targets the player
        {
            ApplyEffectsForPlayer();
        }
    }
    void TargetEnemyUnderCursor()
    {
        if (targetEnemyUnderCursor == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))//we hit the enemy
            {
                ApplyEffectsForEnemies(hit.collider);
            }
        }
    }
    void TargetNothing()
    {
        if (targetNothing == true)
        {
            //Do stuff related to the world such as time stop abilities
        }
    }
    void TargetEveryone()
    {
        if (targetEveryone == true)//Make sure that shouldReferencePlayer is true
        {

        }
    }
    void TargetEnemiesInRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                ApplyEffectsForEnemies(collider);
            }
        }
    }
    #endregion

    #region Location functions
    void MoveOnPlayer()
    {
        if (onPlayer == true)
        {
            gameObject.transform.position = player.transform.position;
        }
    }
    void MoveOnCursor()
    {
        if (onCursor == true)
        {
            gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
        }
    }
    void MoveOnEnemyUnderCursor()
    {
        if (onEnemyUnderCursor == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.CompareTag("Enemy"))
            {
                gameObject.transform.position = hit.transform.position;
            }
        }
    }
    #endregion

    #region Apply effects functions
    void ApplyEffectsForPlayer()//Make sure that shouldReferencePlayer is true
    {
        if (heal == true)
        {
            playerStats.Heal(healAmount);
        }
        if (damage == true)
        {
            playerStats.TakeDamage(damageAmount);
        }
        playerStats.ReduceEnergy(energyCost);
    }
    void ApplyEffectsForEnemies(Collider2D enemyCollider)
    {
        EnemyStats enemyStats = enemyCollider.GetComponent<EnemyStats>();
        if (heal == true)
        {
            enemyStats.Heal(healAmount);
        }
        if (damage == true)
        {
            enemyStats.TakeDamage(damageAmount);
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
