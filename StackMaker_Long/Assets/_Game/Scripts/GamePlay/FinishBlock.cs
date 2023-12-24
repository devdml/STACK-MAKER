using UnityEngine;

public class FinishBlock : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstanName.PLAYER_NAME))
        {
            
        }
    }
}
