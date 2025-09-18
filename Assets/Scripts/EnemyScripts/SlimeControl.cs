using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeControl : MonoBehaviour
{
    public Animator slimeAnim;
    private Transform target;

    private Vector3 initialPosition; // Початкова позиція HitEnemy

    [SerializeField]
    private float slimeSpeed;

    [SerializeField]
    public float area; // Радіус детекту гравця 
    [SerializeField]
    private float minArea;

    private bool isReturning = false;

    public int currentHealth;
    public int maxHealth;

    //public RangeAttack rangeAttack;
    //public TemplateRooms room;
    //public PlayerController playerController;

    void Start()
    {
        slimeAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        initialPosition = transform.position; // Зберегти початкову позицію
    }

    void Update()
    {
        
        if (Vector3.Distance(target.position, transform.position) <= area && Vector3.Distance(target.position, transform.position) >= minArea)
        {
            
            FollowPlayer();
            //rangeAttack.StartCoroutine(ShootCoroutine());
        }
        
        else if (Vector3.Distance(target.position, transform.position) >= area)
        {
            GoBack();
        }
    }

    public void FollowPlayer()
    {
        slimeAnim.SetBool("Range", true);
        slimeAnim.SetFloat("MoveX", (target.position.x - transform.position.x));
        slimeAnim.SetFloat("MoveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, slimeSpeed * Time.deltaTime);
        
    }

    public void GoBack()
    {
        slimeAnim.SetFloat("MoveX", (initialPosition.x - transform.position.x));
        slimeAnim.SetFloat("MoveY", (initialPosition.y - transform.position.y));

        if (!isReturning)
        {
            // Перевірка наявності перешкод перед рухом назад
            Vector3 direction = (initialPosition - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector3.Distance(transform.position, initialPosition));
            if (hit.collider != null)
            {
                // Якщо є перешкода, активуйте режим повернення
                isReturning = true;
                return;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, initialPosition, slimeSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) == 0)
        {
            slimeAnim.SetBool("Range", false);
            isReturning = false;
        }
    }
}
