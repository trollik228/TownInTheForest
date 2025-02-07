using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    public static byte num_day = 10;
    public static bool is_startNewWave = false;

    [SerializeField] private TMPro.TMP_Text day_text;
    [SerializeField] private TMPro.TMP_Text cooldown_text;
    [SerializeField] private TMPro.TMP_Text pause_text;
    [SerializeField] private GameObject panel_newDay;
    [SerializeField] private TMPro.TMP_Text text_newDay;


    private const int COOLDOWN_NEW_WAVE = 10;
    private bool is_startCor_for_spawn = false, is_startCor_for_newDay = false,
                 end_day = false, start_day = false, pause = false;
    private int time = 0;
    private float alpha_buf = 0;


    private void Start() =>
        Time.timeScale = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            Time.timeScale = pause ? 0 : 1;
            pause_text.gameObject.SetActive(pause);
        }
        if (EnemySystem.count_enemys == 0)
        {
            if (!panel_newDay.activeInHierarchy)
            {
                panel_newDay.SetActive(true);
            }
            if (panel_newDay.GetComponent<Image>().color.a < 1 && !end_day)
            {
                alpha_buf += Time.deltaTime / 2;
                panel_newDay.GetComponent<Image>().color = new Color(0, 0, 0, alpha_buf);
            }
            else if (panel_newDay.GetComponent<Image>().color.a > 0)
            {
                end_day = true;

                if (!is_startCor_for_newDay)
                {
                    text_newDay.gameObject.SetActive(true);
                    is_startCor_for_newDay = true;
                    text_newDay.text = $"Δενό: {++num_day}";
                    day_text.text = text_newDay.text;
                    StartCoroutine(Wait_ForChangeAlphaPanelNewDay());
                    MoneySystem.GetMoney(2000);
                }
                if (start_day)
                {
                    alpha_buf -= Time.deltaTime / 2;
                    panel_newDay.GetComponent<Image>().color = new Color(0, 0, 0, alpha_buf);
                }
            }
            else
            {
                GameObject.Find("Background audio").GetComponent<AudioSource>().volume = 1;
                GameObject.Find("Background audio").GetComponent<AudioSource>().Play();
                time = 0;
                alpha_buf = 0;
                is_startCor_for_newDay = false;
                EnemySystem.count_enemys = -1;
                EnemySystem.is_spawn = false;
                is_startNewWave = false;
                end_day = false;
                start_day = false;
                panel_newDay.SetActive(false);
            }
        }
        if (COOLDOWN_NEW_WAVE - time <= 1)
        {
            GameObject.Find("Background audio").GetComponent<AudioSource>().volume -= Time.deltaTime;
        }
        if (!is_startCor_for_spawn && COOLDOWN_NEW_WAVE - time >= 0)
        {
                StartCoroutine(Cooldown());
                cooldown_text.text = (COOLDOWN_NEW_WAVE - time).ToString();
        }
        else if (COOLDOWN_NEW_WAVE - time < 0)
        {
            GameObject.Find("Background audio").GetComponent<AudioSource>().Stop();
            is_startNewWave = true;
        }
    }
    private IEnumerator Wait_ForChangeAlphaPanelNewDay()
    {
        yield return new WaitForSeconds(1.5f);
        text_newDay.gameObject.SetActive(false);
        start_day = true;

    }
    private IEnumerator Cooldown()
    {
        is_startCor_for_spawn=true;
        yield return new WaitForSeconds(1);
        time += 1;
        is_startCor_for_spawn = false;
    }
}
