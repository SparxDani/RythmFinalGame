using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public Vector2 endPos1;
    public Vector2 endPos2;
    public Vector2 endPos3;

    public GameObject flashImage;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject particleField;


    void Start()
    {
        Vector2 startPos = new Vector2(-Screen.width, 0);

        text1.rectTransform.anchoredPosition = startPos;
        text2.rectTransform.anchoredPosition = startPos;
        text3.rectTransform.anchoredPosition = startPos;

        button1.gameObject.SetActive(false);
        //button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        particleField.gameObject.SetActive(false);

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(text1.rectTransform.DOAnchorPos(endPos1, 0.4f).SetEase(Ease.OutBack));
        mySequence.Append(text2.rectTransform.DOAnchorPos(endPos2, 0.4f).SetEase(Ease.OutBack));
        mySequence.Append(text3.rectTransform.DOAnchorPos(endPos3, 0.4f).SetEase(Ease.OutBack));

        mySequence.OnComplete(() =>
        {
            FlashOut();
        });
    }

    void FlashOut()
    {
        flashImage.SetActive(true);
        SpriteRenderer spriteRenderer = flashImage.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            spriteRenderer = flashImage.AddComponent<SpriteRenderer>();
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);

        button1.gameObject.SetActive(true);
        //button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        particleField.gameObject.SetActive(true);
            
        spriteRenderer.DOFade(0f, 3f).OnComplete(() =>
        {
            flashImage.SetActive(false);
        });
    }
}
