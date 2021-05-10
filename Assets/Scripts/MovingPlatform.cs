using UnityEngine;

/// <summary>
/// Controls an individual moving platform
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    public float speed;
    private const float platformGapSize = 3f;
    public Transform leftAlign, rightAlign;
    public MeshRenderer platformRendererLeft, platformRendererRight;
    private const float totalPlatformSpace = 12f;

    void OnEnable()
    {
        RandomizePlatformSpace();
    }

    /// <summary>
    /// Randomize the x scale of the two parts of the platform
    /// </summary>
    void RandomizePlatformSpace()
    {
        float allowedPlatformSpace = totalPlatformSpace - platformGapSize;

        float leftPlatformLength = Random.Range(1, allowedPlatformSpace - platformGapSize);
        float rightPlatformLength = allowedPlatformSpace - leftPlatformLength;
        leftAlign.localScale = new Vector3(leftPlatformLength, 1, 1);
        rightAlign.localScale = new Vector3(rightPlatformLength, 1, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bounds"))
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * speed;
    }
}