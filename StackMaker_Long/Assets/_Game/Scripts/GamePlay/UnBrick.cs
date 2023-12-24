using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    public bool isPassed = false;
    public GameObject brickON;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstanName.PLAYER_NAME))
        {
            if (!isPassed)
            {
                other.GetComponent<Player>().UnBrick();
                brickON.SetActive(true);
                isPassed = true;
            }
          
        }
    }

}
