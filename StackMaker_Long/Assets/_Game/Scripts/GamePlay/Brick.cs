using UnityEngine;

public class Brick : MonoBehaviour
{
    public bool isPassed = false;
    public GameObject brickOff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstanName.PLAYER_NAME))
        {
            if (!isPassed)
            {
                other.GetComponent<Player>().AddBrick();
                brickOff.SetActive(false);
                isPassed = true;

            }
        }
    }
}
