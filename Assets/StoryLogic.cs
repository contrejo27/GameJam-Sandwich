using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLogic : MonoBehaviour
{
    public GameObject[] stories;
    public GameObject bgImage;
    int currentStory = 0;

    public BigEnemy firstEnemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StoryRead()
    {
        stories[currentStory].SetActive(false);
        bgImage.SetActive(false);
        firstEnemy.UnpauseEnemy();
    }

    public void NextStory()
    {
        currentStory++;
        stories[currentStory].SetActive(true);
        bgImage.SetActive(true);
    }
}
