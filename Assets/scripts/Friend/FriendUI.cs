using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class FriendUI : MonoBehaviour
{
    public GameObject[] panel;
    public Vector3 startPos;
    public Dictionary<string, GameObject> panelList = new Dictionary<string, GameObject>();
    public Button[] BtnMove;
    public Vector3[] endPos;
    public Vector3 firstPos;
    public float moveTime = 1f;  // Thời gian di chuyển
    public float rotateTime = 1f;  // Thời gian xoay

   
    private void OnEnable()
    {
        for (int i = 0; i < BtnMove.Length; i++)
        {
            BtnMove[i].transform.localScale = new Vector3(1f, 1f, 1f);
        }
        OffAllPanel();
        foreach(Button button in BtnMove)
        {
            button.transform.localPosition = startPos;
        }
        StartCoroutine(MoveAndRotateButtons());
    }
    void OffAllPanel()
    {
        foreach (GameObject p in panel)
        {
            p.SetActive(false);
        }
    }
    private IEnumerator MoveAndRotateButtons()
    {
        for (int i = 0; i < BtnMove.Length; i++)
        {
            // Di chuyển nút đến firstPos
            BtnMove[i].transform.DOLocalMove(firstPos, 0.2f);  // Sử dụng DOLocalMove

            // Đợi mỗi nút 0.1s
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.3f);
        // Sau khi tất cả các nút đã đến firstPos, chúng sẽ di chuyển và xoay đến endPos
        for (int i = 0; i < BtnMove.Length; i++)
        {
            // Tạo một Sequence để kết hợp di chuyển và xoay
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(BtnMove[i].transform.DOLocalMove(endPos[i], moveTime));  // Di chuyển
            mySequence.Join(BtnMove[i].transform.DORotate(new Vector3(0f, 0f, 360f), rotateTime, RotateMode.FastBeyond360));  // Xoay đồng thời với di chuyển
            mySequence.AppendCallback(() => BtnMove[i].transform.rotation = Quaternion.identity);  // Đặt xoay về 0 khi di chuyển hoàn tất
        }
        // Đợi 2 giây sau khi hàm chạy xong
        yield return new WaitForSeconds(1.2f);
        OffAllPanel();
        // Bật panel[0] sau 2 giây
        panel[0].SetActive(true);
        BtnEffective();
    }
   
    public void BtnEffective()
    {
        int j = -1;
        for (int i = 0; i < panel.Length; i++)
        {
            if (panel[i].activeSelf)  // sử dụng activeSelf thay vì active
            {
                j = i;
                break;
            }
        }
        for (int i = 0; i < BtnMove.Length; i++)
        {
            if (i == j)
            {
                BtnMove[i].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                BtnMove[i].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
        }
    }

}
