using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
