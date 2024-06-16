using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ChoosePlayer : MonoBehaviour
{
    public Color color1 = Color.black;
    public Color color2 = Color.white;
    public float blinkInterval = 5f; // Thời gian giữa các lần nhấp nháy
    public int blinkCount = 5; // Số lần nhấp nháy mỗi lần
    public float blinkDuration = 0.5f; // Thời gian của từng lần nhấp nháy

    [SerializeField] private Button right;
    [SerializeField] private Button left;
    [SerializeField] private GameObject[] player;

    private int[] categories = { 0, 2, 1 };

    private int currentIndex = 0;
    private void Start()
    {
        ScaleButton(right);
        ScaleButton(left);
        right.onClick.AddListener(ShiftRight);
        left.onClick.AddListener(ShiftLeft);
        UpdateCharacterVisibility();
        
    }
   
    void OnEnable()
    {
        StartCoroutine(BlinkColor());
    }
    IEnumerator BlinkColor()
    {
        while (true) // Vòng lặp vô tận để nhấp nháy liên tục
        {
            yield return new WaitForSeconds(blinkInterval);
            for (int i = 0; i < blinkCount; i++)
            {
                player[currentIndex].GetComponent<Image>().color = color1;
                yield return new WaitForSeconds(blinkDuration);
                player[currentIndex].GetComponent<Image>().color = color2;
                yield return new WaitForSeconds(blinkDuration);
            }
        }
    }
    private void ScaleButton(Button button)
    {
        // Scale up over 0.5 seconds, then scale down over 0.5 seconds
        button.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    private void ShiftRight()
    {
        currentIndex = (currentIndex + 1) % player.Length;
        UpdateCharacterVisibility();
    }
    private void ShiftLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = player.Length - 1;
        }
        UpdateCharacterVisibility();
    }
    private void UpdateCharacterVisibility()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i].SetActive(i == currentIndex);
        }
        LobbyController.Instance.roleCategory = categories[currentIndex];
    }
}
