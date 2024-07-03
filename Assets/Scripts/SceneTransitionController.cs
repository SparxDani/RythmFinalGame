using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionController : MonoBehaviour
{
    public static SceneTransitionController Instance { get; private set; }

    public Image fadeImage;
    public AudioClip playSoundEffect;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void FadeToScene(string sceneName)
    {
        PlayPlaySound();
        StartCoroutine(FadeOutIn(sceneName));
    }

    private void PlayPlaySound()
    {
        if (audioSource != null && playSoundEffect != null)
        {
            audioSource.PlayOneShot(playSoundEffect);
        }
    }
    private IEnumerator FadeOutIn(string sceneName)
    {
        yield return fadeImage.DOFade(1, 1f).WaitForCompletion();
        SceneManager.LoadScene(sceneName);
        yield return null;
        yield return fadeImage.DOFade(0, 1f).WaitForCompletion();
    }

    
}