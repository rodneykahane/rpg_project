﻿using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        public void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            yield return FadeIn(3f);
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)  // while alpha NOT 1
            {
                canvasGroup.alpha += Time.deltaTime / time;

                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)  // while alpha NOT 0
            {
                canvasGroup.alpha -= Time.deltaTime / time;

                yield return null;
            }
        }
    }
}