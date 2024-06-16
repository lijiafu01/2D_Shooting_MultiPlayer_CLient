using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject effectPrefab; // Prefab của hiệu ứng
    public int poolSize = 20; // Kích thước pool

    private Queue<GameObject> effectPool; // Queue để lưu trữ các hiệu ứng không hoạt động

    void Start()
    {
        effectPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject effectObj = Instantiate(effectPrefab);
            effectObj.SetActive(false);
            effectPool.Enqueue(effectObj);
        }
    }

    public GameObject Get()
    {
        if (effectPool.Count > 0)
        {
            GameObject effectObj = effectPool.Dequeue();
            effectObj.SetActive(true);
            return effectObj;
        }
        else
        {
            // Tạo thêm hiệu ứng nếu hết
            GameObject effectObj = Instantiate(effectPrefab);
            return effectObj;
        }
    }

    public void ReturnToPool(GameObject effectObj)
    {
        effectObj.SetActive(false);
        effectPool.Enqueue(effectObj);
    }
}
