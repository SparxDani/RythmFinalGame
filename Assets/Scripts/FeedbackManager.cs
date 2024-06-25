using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager Instance;
    public TextMeshProUGUI feedbackText;
    public float displayDuration = 1f;

    private Coroutine feedbackCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowFeedback(string message, int comboScore)
    {
        feedbackText.text = $"{comboScore} Combo\n{message}";
        feedbackText.enabled = true;
        StartCoroutine(HideFeedback());
    }

    private IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(1f);
        feedbackText.enabled = false;
    }
}
