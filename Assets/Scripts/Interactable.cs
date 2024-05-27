using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string messaage;
    public UnityEvent onInteraction;
    public string sceneToLoad; // Name of the scene to load

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact()
    {
        // Invoke other interaction events
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    // Custom function to change scene
    public void ChangeScene()
    {
        // Check if the scene to load is not empty
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Check if the scene exists in the build settings
            if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
            {
                // Load the scene
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("Scene " + sceneToLoad + " does not exist in the build settings.");
            }
        }
        else
        {
            Debug.LogWarning("No scene specified to load.");
        }
    }
}
