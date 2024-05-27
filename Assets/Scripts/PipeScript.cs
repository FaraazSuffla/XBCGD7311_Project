using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;

    int PossibleRots = 1;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        PossibleRots = correctRotation.Length;

        // Shuffle the rotations array
        for (int i = rotations.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            float temp = rotations[i];
            rotations[i] = rotations[j];
            rotations[j] = temp;
        }

        // Set the pipe to a random rotation that is not correct
        foreach (float rotation in rotations)
        {
            if (PossibleRots > 1)
            {
                if (rotation != correctRotation[0] && rotation != correctRotation[1])
                {
                    transform.eulerAngles = new Vector3(0, 0, rotation);
                    break;
                }
            }
            else
            {
                if (rotation != correctRotation[0])
                {
                    transform.eulerAngles = new Vector3(0, 0, rotation);
                    break;
                }
            }
        }

        // Ensure pipe is not initially placed
        isPlaced = false;
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));

        if (PossibleRots > 1)
        {
            if ((transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1]) && !isPlaced)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0] && !isPlaced)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
    }
}
