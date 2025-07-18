using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float GrSpeed = 5;
    public float platformDeadzone = -30;
    public float coinDeadzone = -10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isGameOver)
        {
            return;
        }

        transform.position += Vector3.left * GrSpeed * Time.deltaTime;
        if (gameObject.tag == "Coin" && transform.position.x < coinDeadzone) { Destroy(gameObject); }
        else if (transform.position.x < platformDeadzone) { Destroy(gameObject); }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Coin" && collision.CompareTag("Player"))
        {
            ScoreManger.instance.addCoins(1);
            Destroy(gameObject);
        }
    }
}
