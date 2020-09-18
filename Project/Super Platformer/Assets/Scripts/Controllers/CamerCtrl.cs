using UnityEngine;

/// <summary>
/// Makes the camera follow the player
/// </summary>
public class CamerCtrl : MonoBehaviour
{
    public Transform catTransform;
    public float yOffset;

    void Update()
    {
        transform.position = new Vector3(catTransform.position.x, transform.position.y, transform.position.z);
    }
}
