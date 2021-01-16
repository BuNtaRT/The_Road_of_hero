using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard_controller : MonoBehaviour
{
    #region Speed car correct            
    //крутая штука регионы CodeReadUP
    float speed_car = 0.2f;
    public delegate void Speed(float val);          // так же делегат где будут методы вызываемые
    public event Speed OnUpSpead;                   // так же событие

    void upSpead(float val) {
        OnUpSpead?.Invoke(val);                     // сообщаем всем что скорость изменена 
    }
    #endregion




}
