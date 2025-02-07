using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private static int money = 1000;
    [SerializeField] private TMPro.TMP_Text moneyCount_text;

    public static void GetMoney(int _val_) =>
        money += _val_;

    public static bool CheakMoney(int _val_)
    {
        return money >= _val_;
    }
    private void Update()
    {
        if (moneyCount_text.text != $"Золото: {money}")
            moneyCount_text.text = $"Золото: {money}";
    }
}
