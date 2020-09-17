using UnityEngine;

public class BtnSaveCtrl : MonoBehaviour
{
    public void SaveData()
    {
        DataCtrl.instance.SaveData(DataCtrl.instance.data);
    }
}
