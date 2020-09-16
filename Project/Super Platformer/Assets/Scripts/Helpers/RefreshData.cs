using UnityEngine;

public class RefreshData : MonoBehaviour
{
    void Start()
    {
        DataCtrl.instance.RefreshData();
    }
}
