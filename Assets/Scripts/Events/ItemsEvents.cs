using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEvents : MonoBehaviour
{
    public event Action<int> onMoneyChange;
    public void MoneyChange(int amount)
    {
        if(onMoneyChange != null) 
            onMoneyChange(amount);
    }
}
