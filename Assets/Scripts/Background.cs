using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Material backgroundMaterial;
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0f, 0.1f);

    private Vector2 startOffset;

    private void Start()
    {
        if (backgroundMaterial != null)
        {
            startOffset = backgroundMaterial.mainTextureOffset;
        }
    }

    private void Update()
    {
        if (backgroundMaterial == null)
        {
            return;
        }

        backgroundMaterial.mainTextureOffset = startOffset + scrollSpeed * Time.time;
    }
}
