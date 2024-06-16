using UnityEngine;

public class RotationObj : MonoBehaviour
{
    public float speed = 10f;  // Tốc độ quay

    void Update()
    {
        // Quay GameObject liên tục theo trục Y
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
