using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorSystem : MonoBehaviour
{
    [SerializeField] private GameObject error_text;
    public static string text_error;
    public static bool is_see = false;
    private static float alpha = 0.9f;

    public static void SeeError(string str)
    {
        alpha = 0.9f;
        text_error = str;
        is_see = true;
    }
    private void Update()
    {
        if (is_see)
        {
            if (error_text.GetComponent<TMPro.TMP_Text>().text != text_error)
            {
                error_text.GetComponent<TMPro.TMP_Text>().text = text_error;
            }
            if (alpha <= 0)
            {
                is_see = false;
                error_text.SetActive(false);
                alpha = 0.9f;
                return;
            }
            alpha -= Time.deltaTime;
            error_text.SetActive(true);
            error_text.GetComponent<TMPro.TMP_Text>().color = new Color(255, 0, 0, alpha);
        }
    }
}
