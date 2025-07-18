using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject ground;
    private float spawnRate = 2f;
    private float timer = 0;
    private Transform prefabChild;
    private Transform prefabParent;

    public GameObject coinPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GroundSpawn(0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            float lastRightEdge = prefabParent.position.x + (prefabChild.localScale.x / 2f);

            float distanceBetween = Random.Range(5f, 8f);
            float randSize = Random.Range(10f, 20f);
            float leftEdgeOfNew = randSize / 2;

            float xPosSpawn = lastRightEdge + distanceBetween + leftEdgeOfNew;
            if (lastRightEdge + distanceBetween <= transform.position.x)
            {
                GroundSpawn(xPosSpawn, randSize);
            }
            // GroundSpawn(xPosSpawn, randSize);
            timer = 0;
        }
    }
    void GroundSpawn(float xPos, float width)
    {
        // Instantiate(ground, transform.position, transform.rotation);
        // float randSize = Random.Range(5f, 15f);
        Vector2 sapwnX = new Vector2(xPos, transform.position.y);
        // spawning the grounds whole prefab
        GameObject newGround = Instantiate(ground, sapwnX, transform.rotation);
        // Finding the Child Platform not the parent
        Transform groundVisual = newGround.transform.Find("Ground");
        // Finds the Score Trigger
        Transform score = newGround.transform.Find("Score Trigger");
        // Randomizing the X Length of Each Platform
        groundVisual.localScale = new Vector2(width, groundVisual.localScale.y);
        //Spawns the score trigger at the left edge of the platform
        score.position = new Vector2(xPos + .5f - width / 2, score.position.y);
        prefabChild = groundVisual;
        prefabParent = newGround.transform;

        CoinSpawn(groundVisual, xPos, width);
    }

    void CoinSpawn(Transform platform, float xPos, float width)
    {
        float space = 2f;
        float minX = xPos - (width / 2f) + 1f; // slight inward padding
        float maxX = xPos + (width / 2f) - 1f;

        int coinCount = Mathf.FloorToInt((maxX - minX) / space);
        for (int i = 0; i < coinCount; i++)
        {
            float x = minX + i * space;
            float y = platform.position.y + 1.5f; // adjust to float above platform

            Instantiate(coinPrefab, new Vector2(x, y), Quaternion.identity);
        }
    }
}
