using UnityEngine;

/// <summary>
/// Calls the DataCtrl in order to get the most recent data
/// </summary>
public class RefreshData : MonoBehaviour
{
    void Start()
    {
        DataCtrl.instance.RefreshData();
    }
}
