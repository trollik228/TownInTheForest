using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SystemGame : MonoBehaviour
{
    public static bool see_arrow = true, is_start_timer_for_arrow = false;


    public float health = 500;
    public bool game_over = false;


    [SerializeField] private GameObject panel_gameOver;
    [SerializeField] private GameObject panel_gameStarted;
    [SerializeField] private Image health_i;

    private GameObject arrow;
    private void Start() =>
        arrow = GameObject.Find("ArrowToSapwnPoint");
    private bool InFOVCamera(Vector3 vec3)
    {
        var bounds = new Bounds(vec3, Vector3.one);
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

    private void Update()
    {
        if(TimeSystem.num_day == 11)
        {
            panel_gameOver.SetActive(true);
            panel_gameOver.transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text = "Вы победили!";
            panel_gameOver.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = "Поздравляем! Вы смогли защитить жителей города!!!";

            game_over = true;
        }
        if (EnemySystem.spawn_point != null && InFOVCamera(EnemySystem.spawn_point.position))
        {
            if (see_arrow)
            {
                var arrow_vec3 = Camera.main.WorldToScreenPoint(EnemySystem.spawn_point.position);
                arrow.transform.position = new Vector3(arrow_vec3.x, arrow_vec3.y + 100, 0);
            }
            if (!is_start_timer_for_arrow) 
                StartCoroutine(Timer_ForArrow());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health <= 0)
        {
            game_over = true;
            panel_gameOver.SetActive(true);
            Time.timeScale = 0;
            panel_gameOver.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = "Причина: город разрушен благодаря призракам-магам!";

        }
        if (other.CompareTag("bullet enemy"))
        {
            health_i.fillAmount -= other.GetComponent<Bullet_Ghost>().damage / 500;
            health -= other.GetComponent<Bullet_Ghost>().damage;
            StatisticSystem.geted_damage += other.GetComponent<Bullet_Ghost>().damage;
        }
        if (other.CompareTag("Enemy"))
        {
            game_over = true;
            panel_gameOver.transform.GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = "Причина: в город проникли мечники-скелеты!";
            panel_gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private IEnumerator Timer_ForArrow()
    {
        is_start_timer_for_arrow = true;
        yield return new WaitForSeconds(10);
        see_arrow = false;
        arrow.transform.position = new Vector3(10000, 1000, 0);
    }
    public void Exit() =>
        Application.Quit();
    public void Ok()
    {
       Destroy(panel_gameStarted);
        Time.timeScale = 1;
    }
}
