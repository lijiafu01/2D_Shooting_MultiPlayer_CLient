using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ObjectPool effectPool;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject effect = effectPool.Get();

            // Đặt vị trí của hiệu ứng tại vị trí chuột
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0; // Đảm bảo hiệu ứng không bị di chuyển ra khỏi mặt phẳng
            effect.transform.position = mousePos;

            // Tắt hiệu ứng sau 0.2s
            StartCoroutine(ReturnEffectToPool(effect, 0.3f));
        }
    }

    IEnumerator ReturnEffectToPool(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effectPool.ReturnToPool(effect);
    }
}
