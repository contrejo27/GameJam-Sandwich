using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public StoryLogic story;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            story.NextStory();
            Destroy(gameObject);
        }

    }
}
