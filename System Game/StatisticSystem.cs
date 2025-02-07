using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticSystem : MonoBehaviour
{
    public static int towers = 0;
    public static int kill_enemys = 0;
    public static float geted_damage = 0;
    public static float time_play = 0;

    [SerializeField] private GameObject panel_stats;
    [SerializeField] private TMPro.TMP_Text towers_text;
    [SerializeField] private TMPro.TMP_Text kill_enemys_text;
    [SerializeField] private TMPro.TMP_Text geted_damage_text;
    [SerializeField] private TMPro.TMP_Text time_play_text;


    public void Open_Panel()
    {
        panel_stats.SetActive(true);

        towers_text.text = $"Построено башен: {towers}.";
        kill_enemys_text.text = $"Уничтожено врагов: {kill_enemys}.";
        geted_damage_text.text = $"Получено урона: {geted_damage}.";
        time_play_text.text = $"Наиграно: {(int)(Time.unscaledTime/60)} минут.";
    }
    public void Close()
    { panel_stats.SetActive(false);}
}
