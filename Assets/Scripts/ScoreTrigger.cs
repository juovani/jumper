using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool hasScored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasScored && other.CompareTag("Player"))
        {
            hasScored = true;
            ScoreManger.instance.addScore(1);
            gameObject.SetActive(false); // Optional: hide the trigger
        }
    }
}
