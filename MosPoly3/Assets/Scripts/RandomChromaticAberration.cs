using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class RandomChromaticAberration : MonoBehaviour
{
    public Volume volume;
    public float maxIntensity = 1f;
    public float minDelay = 0.5f;
    public float maxDelay = 2f;
    public float transitionSpeed = 1f;

    private ChromaticAberration chromaticAberration;
    private bool effectEnabled;
    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        if (volume.profile.TryGet<ChromaticAberration>(out chromaticAberration))
        {
            currentIntensity = chromaticAberration.intensity.value;
            targetIntensity = currentIntensity;
            StartCoroutine(RandomizeEffect());
        }
        else
        {
            Debug.LogError("Chromatic Aberration не найден в профиле Volume.");
        }
    }

    void Update()
    {
        if (Mathf.Abs(currentIntensity - targetIntensity) > 0.01f)
        {
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * transitionSpeed);
            chromaticAberration.intensity.value = currentIntensity;
        }
    }

    private IEnumerator RandomizeEffect()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            effectEnabled = !effectEnabled;
            targetIntensity = effectEnabled ? maxIntensity : 0f;
        }
    }
}
