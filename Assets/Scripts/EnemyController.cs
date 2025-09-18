using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public Animator zombiAnim;
    private Transform target;

    private Vector3 initialPosition; // Початкова позиція зомбі

    [SerializeField]
    private float zombSpeed;

    [SerializeField]
    private float area; // Радіус детекту гравця 
    [SerializeField]
    private float minArea;

    private bool isReturning = false;

    public int currentHealth;
    public int maxHealth;
    //public TemplateRooms room;
    //public PlayerController playerController;

    void Start()
    {
        zombiAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        initialPosition = transform.position; // Зберегти початкову позицію 
    }

    void Update()
    {
        
        if (Vector3.Distance(target.position, transform.position) <= area && Vector3.Distance(target.position, transform.position) >= minArea)
        {
            FollowPlayer();
        }
        
        else if (Vector3.Distance(target.position, transform.position) >= area)
        {
            GoBack();
        }
    }

    public void FollowPlayer()
    {
        zombiAnim.SetBool("Range", true);
        zombiAnim.SetFloat("MoveX", (target.position.x - transform.position.x));
        zombiAnim.SetFloat("MoveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, zombSpeed * Time.deltaTime);
    }

    public void GoBack()
    {
        zombiAnim.SetFloat("MoveX", (initialPosition.x - transform.position.x));
        zombiAnim.SetFloat("MoveY", (initialPosition.y - transform.position.y));

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

        transform.position = Vector3.MoveTowards(transform.position, initialPosition, zombSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) == 0)
        {
            zombiAnim.SetBool("Range", false);
            isReturning = false;
        }
    }
}
