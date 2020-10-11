using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] birdPrefab;

    List<GameObject> birds;

    [SerializeField]
    int birdCount = 15;

    float vel = 3.0f;
    Vector3 outPos = new Vector3 (-100f, -100f, -100f);
    // Start is called before the first frame update
    void Start()
    {
        birds = new List<GameObject>();
        for (int i = 0; i < birdCount; i++ )
        {
            AddBird(i%birdPrefab.Length);
        }
    }

    void AddBird(int birdType)
    {
        GameObject bird = Instantiate(birdPrefab[birdType], outPos, Quaternion.identity, transform);
        bird.SetActive(false);
        birds.Add(bird);
    }

    public void ReturnToList(GameObject bird)
    {
        birds.Add(bird);
        bird.SetActive(false);
    }


    void GenerateBird()
    {
        birds[0].SetActive(true);
        birds[0].GetComponent<BirdMind>().Init();
        birds.Remove(birds[0]);
    }

    float cd = 2.0f;
    float cdTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > cdTime)
        {
            GenerateBird();
            cdTime = Time.time + cd;
        }
        
    }
}
