using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class UIController : MonoBehaviour
{
  
   
    public GameObject LobyPanel;

    public Vector3[] offPosition;

    public GameObject blinkingObject; // The GameObject you want to blink
    public float blinkInterval = 0.05f; // The interval between each blink
    public float blinkDuration = 5f; // How long the blinking lasts

    public Image roomImage; // The Image
    public float scaleDuration = 1f; // How long the scaling should take
    public float rotateDuration = 1f; // How long the rotation should take
    public float initialScaleFactor = 0.5f; // The initial scale factor of the Image


    public Button[] buttons; // The array of Buttons
    public Vector3[] positions; // The array of target positions

    public float moveDuration = 1f; // How long each move should take
    public float delay = 0.1f; // Delay between each button's start

   
    void OnEnable()
    {

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.localPosition = offPosition[i];
        }
        LobyPanel.SetActive(true);
        MainUI();
        
    }
    public void MainUI()
    {
        BtnMove();
        roomTable();
        BlinkingObject();
    }
    /*public void FriendBtn()
    {
        if (buttons.Length != offPosition.Length)
        {
            Debug.LogError("Mismatched button and position arrays!");
            return;
        }
        Array.Reverse(buttons);
        Array.Reverse(offPosition);
        // Create a new Sequence
        Sequence sequence = DOTween.Sequence();

        // Iterate over each Button in the array
        for (int i = 0; i < buttons.Length; i++)
        {
            // For each Button, add a new tween to the Sequence that moves the Button to the corresponding position
            sequence.Insert(i * delay, buttons[i].transform.DOLocalMove(offPosition[i], moveDuration).SetEase(Ease.InOutSine));
        }
        // Start the Sequence
        sequence.Play();

        StartCoroutine(FriendUI(sequence));
        

    }*/
   /* IEnumerator FriendUI(Sequence sequence)
    {
        yield return new WaitForSeconds(1.1f);

        PopupManager.Instance.DisableAllPopup();
        PopupManager.Instance.GetPopup("Friend");
    }*/
    public void StartBtn()
    {
        if (buttons.Length != offPosition.Length)
        {
            Debug.LogError("Mismatched button and position arrays!");
            return;
        }
        Array.Reverse(buttons);
        Array.Reverse(offPosition);
        // Create a new Sequence
        Sequence sequence = DOTween.Sequence();

        // Iterate over each Button in the array
        for (int i = 0; i < buttons.Length; i++)
        {
            // For each Button, add a new tween to the Sequence that moves the Button to the corresponding position
            sequence.Insert(i * delay, buttons[i].transform.DOLocalMove(offPosition[i], moveDuration).SetEase(Ease.InOutSine));
        }
        // Start the Sequence
        sequence.Play();
        StartCoroutine(PlayGame(sequence));
    }
    
    IEnumerator PlayGame(Sequence sequence)
    {
        if (sequence == null || !sequence.IsActive() || sequence.IsComplete())
        {
            Debug.LogError("Sequence is null, not active, or already complete.");
            yield break;
        }

        float waitTime = 10f;
        while (sequence.IsActive() && !sequence.IsComplete() && waitTime > 0)
        {
            Debug.Log(sequence.IsComplete());
            yield return new WaitForEndOfFrame();
            waitTime -= Time.deltaTime;
        }

        if (waitTime <= 0)
        {
            Debug.LogError("Sequence did not complete within the allowed time.");
            yield break;
        }

        LobbyController.Instance.StartBtn();
    }


    void BtnMove()
    {
        if (buttons.Length != positions.Length)
        {
            Debug.LogError("Mismatched button and position arrays!");
            return;
        }

        // Create a new Sequence
        Sequence sequence = DOTween.Sequence();

        // Iterate over each Button in the array
        for (int i = 0; i < buttons.Length; i++)
        {
            // For each Button, add a new tween to the Sequence that moves the Button to the corresponding position
            sequence.Insert(i * delay, buttons[i].transform.DOLocalMove(positions[i], moveDuration).SetEase(Ease.InOutSine));
        }
        // Start the Sequence
        sequence.Play();
    }
    void roomTable()
    {
        // Get the initial scale of the Image
        Vector3 initialScale = roomImage.transform.localScale;

        // Set the initial scale of the Image
        roomImage.transform.localScale = initialScale * initialScaleFactor;

        // Create a new Sequence
        Sequence sequence2 = DOTween.Sequence();

        // Add a tween to the sequence that scales up the Image
        sequence2.Append(roomImage.transform.DOScale(initialScale * 2, scaleDuration).SetEase(Ease.InOutSine));

        // Simultaneously, rotate the Image 360 degrees
        sequence2.Join(roomImage.transform.DORotate(new Vector3(0, 0, 360), rotateDuration, RotateMode.FastBeyond360).SetEase(Ease.InOutSine));

        // After that, scale the Image back to its initial size
        sequence2.Append(roomImage.transform.DOScale(initialScale, scaleDuration).SetEase(Ease.InOutSine));

        // Start the Sequence
        sequence2.Play();
    }
    void BlinkingObject()
    {
        // Make sure the object is active at start
        blinkingObject.SetActive(true);

        // Calculate how many times to blink
        int blinkTimes = Mathf.RoundToInt(blinkDuration / blinkInterval);

        // Use a Sequence to blink the object on and off
        Sequence sequence3 = DOTween.Sequence();
        for (int i = 0; i < blinkTimes; i++)
        {
            sequence3.AppendCallback(() => blinkingObject.SetActive(false));
            sequence3.AppendInterval(blinkInterval);
            sequence3.AppendCallback(() => blinkingObject.SetActive(true));
            sequence3.AppendInterval(blinkInterval);
        }

        // Start the Sequence
        sequence3.Play();
    }
}
