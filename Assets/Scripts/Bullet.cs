using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
