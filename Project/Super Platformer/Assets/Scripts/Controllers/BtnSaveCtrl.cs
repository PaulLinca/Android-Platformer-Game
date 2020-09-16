using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSaveCtrl : MonoBehaviour
{
    public void SaveData()
    {
        DataCtrl.instance.SaveData();
    }
}
