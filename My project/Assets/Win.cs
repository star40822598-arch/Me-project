using UnityEngine;
using TMPro;
using System.Collections;

public class WinManager : MonoBehaviour
{
    public LightTarget[] targets;

    public GameObject winText;

    bool won = false;

    void Update()
    {
        if (won) return;

        bool allActive = true;

        foreach (var target in targets)
        {
            if (!target.activated)
            {
                allActive = false;
            }
        }

        if (allActive)
        {
            won = true;

            StartCoroutine(ShowWin());
        }
    }

    IEnumerator ShowWin()
    {
        winText.SetActive(true);

        yield return new WaitForSeconds(3);

        winText.SetActive(false);
    }
}