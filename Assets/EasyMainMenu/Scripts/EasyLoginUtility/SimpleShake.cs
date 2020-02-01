using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShake : MonoBehaviour {

    [Range(0.1f,2f)]
    public float shakeDuration;
    [Range(0.1f,5f)]
    public float shakeAmount;
    [Range(0.1f,5f)]
    public float decreaseFactor;

	// Use this for Shaking
	public void StartShaking () {
        StartCoroutine(Shake());
	}

    IEnumerator Shake() {

        Vector3 originalPos = transform.position;
        float currentShakeDuration = shakeDuration;

        while (currentShakeDuration > 0) {

            transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            currentShakeDuration -= Time.deltaTime * decreaseFactor;
            yield return null;

        }

        transform.position = originalPos;

    }
}
