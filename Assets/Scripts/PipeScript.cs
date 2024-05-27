using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;
    bool isPlaced = false;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        StartCoroutine(InitializePipeRotation());
    }

    IEnumerator InitializePipeRotation()
    {
        yield return new WaitForSeconds(0.1f); // Delay to ensure rotation animation completes

        // Set the pipe to a random rotation that is not correct
        List<float> nonCorrectRotations = new List<float>();

        // Populate the list with rotations that are not correct
        foreach (float rotation in rotations)
        {
            bool isCorrect = false;
            foreach (float correctRot in correctRotation)
            {
                if (Mathf.Abs(rotation - correctRot) < 1f)
                {
                    isCorrect = true;
                    break;
                }
            }

            if (!isCorrect)
                nonCorrectRotations.Add(rotation);
        }

        // Set the pipe's rotation to a random rotation from the list of non-correct rotations
        transform.eulerAngles = new Vector3(0, 0, nonCorrectRotations[Random.Range(0, nonCorrectRotations.Count)]);

        // Ensure pipe is not initially placed
        isPlaced = false;
    }

    void OnMouseDown()
    {
        StartCoroutine(RotatePipe());
    }

    IEnumerator RotatePipe()
    {
        yield return new WaitForSeconds(0.1f); // Delay to ensure rotation animation completes

        // Rotate the pipe
        transform.Rotate(new Vector3(0, 0, 90));

        // Check if the current rotation is correct
        bool isCorrect = IsCorrectRotation();

        // If the pipe was not placed and is now in correct rotation, count it as placed
        if (!isPlaced && isCorrect)
        {
            isPlaced = true;
            gameManager.correctMove();
            Debug.Log("Pipe placed correctly.");
        }
        // If the pipe was placed and is not in correct rotation anymore, count it as unplaced
        else if (isPlaced && !isCorrect)
        {
            isPlaced = false;
            gameManager.wrongMove();
            Debug.Log("Pipe removed from correct position.");
        }
    }

    bool IsCorrectRotation()
    {
        float currentRotation = transform.eulerAngles.z;

        foreach (float correctRot in correctRotation)
        {
            // Check if the difference between current rotation and correct rotation is within a small threshold
            if (Mathf.Abs(currentRotation - correctRot) < 1f)
                return true;
        }

        return false;
    }
}
