using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonControl : MonoBehaviour
{
    public Animator demonAnim;
    private Transform target;
    private Vector3 initialPosition; // Початкова позиція 
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float demonSpeed;

    [SerializeField]
    public float area; // Радіус детекту гравця 
    [SerializeField]
    private float minArea;

    public BoxCollider2D bossZoneCollider;

    private bool isReturning = false;

    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        bossZoneCollider = GameObject.Find("BossZone").GetComponent<BoxCollider2D>();
        demonAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position; // Зберегти початкову позицію
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if (bossZoneCollider.bounds.Contains(target.position) && distanceToTarget >= minArea)
        {
            FollowPlayer();
            //rangeAttack.StartCoroutine(ShootCoroutine());
            
        }
        else if (distanceToTarget >= area)
        {
            GoBack();
        }


    }

    public void FollowPlayer()
    {
        demonAnim.SetBool("Range", true);
        Vector3 moveDirection = (target.position - transform.position).normalized;
        demonAnim.SetFloat("MoveX", moveDirection.x);
        //demonAnim.SetFloat("MoveY", moveDirection.y);
        transform.position = Vector3.MoveTowards(transform.position, target.position, demonSpeed * Time.deltaTime);

        if (moveDirection.x < 0)
        {
            // Ворог повернений вправо
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.x > 0)
        {
            // Ворог повернений вліво
            spriteRenderer.flipX = true;
        }
    }

    public void GoBack()
    {
        demonAnim.SetFloat("MoveX", (initialPosition.x - transform.position.x));
        //demonAnim.SetFloat("MoveY", (initialPosition.y - transform.position.y));

        if (!isReturning)
        {
            // Перевірка наявності перешкод перед рухом назад
            Vector3 direction = (initialPosition - transform.position).normalized;
            float distanceToInitial = Vector3.Distance(transform.position, initialPosition);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceToInitial);
            if (hit.collider != null)
            {
                // Якщо є перешкода, активуйте режим повернення
                isReturning = true;
                return;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, initialPosition, demonSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) == 0)
        {
            demonAnim.SetBool("Range", false);
            isReturning = false;
        }
    }
}
