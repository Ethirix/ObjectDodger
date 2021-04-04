using UnityEngine;
public class KillMenuBoxes : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("MainMenu_Cube")) {
            Destroy(other.gameObject);
        }
    }
}
