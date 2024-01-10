using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource audio;

    int currentPositionIndex = 0;
    List<Vector3> positions = new List<Vector3> { new Vector3(-3.136f, -0.61f, -2.73f), new Vector3(1.52f, -0.96f, 5.52f), new Vector3(0.33f, -0.4f, -5.29f) };
    private Coroutine cronoCoroutine;
    List<int> positionTime = new List<int> { 3 * 60, 4 * 60, 5 * 60 };

    void Start()
    {
        audio = GetComponent<AudioSource>();

        audio.loop = true;

        audio.Play();

        StartCrono();
    }

    void OnDestroy()
    {
        if (audio != null)
        {
            audio.Stop();
        }
        StopCrono();
    }

    void StartCrono()
    {
        //StopCrono();

        cronoCoroutine = StartCoroutine(Crono(positionTime[currentPositionIndex]));
    }

    void StopCrono()
    {
        if (cronoCoroutine != null)
        {
            StopCoroutine(cronoCoroutine);
            cronoCoroutine = null;
        }
    }

    IEnumerator Crono(int duration)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            duration--;
            if(duration > 0)
            {
                continue;
            }
            ++currentPositionIndex;

            duration = positionTime[currentPositionIndex];
            transform.position = positions[currentPositionIndex];
        }
    }
}
