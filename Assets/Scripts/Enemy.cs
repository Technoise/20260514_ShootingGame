using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float playerTargetChance = 0.3f;

    private Vector3 moveDirection = Vector3.down;

    private void Start()
    {
        SetMoveDirection();
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void SetMoveDirection()
    {
        bool moveToPlayer = Random.value < playerTargetChance;

        if (moveToPlayer)
        {
            GameObject player = GameObject.Find("Player");

            if (player != null)
            {
                moveDirection = player.transform.position - transform.position;
                moveDirection.z = 0f;

                if (moveDirection.sqrMagnitude > 0f)
                {
                    moveDirection.Normalize();
                    return;
                }
            }
        }

        moveDirection = Vector3.down;
    }
}
