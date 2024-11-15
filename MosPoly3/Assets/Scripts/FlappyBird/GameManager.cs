using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectParent;
    [SerializeField] private GameObject Canvas;

    [SerializeField] private FlyBehavior player;
    [SerializeField] private PipeSpawner pipeSpawner;

    private List<MovePipe> pipes = new List<MovePipe>();
    private Vector3 playerStartPos;
    private Quaternion playerStartRotation;
    private bool gameStarted = false;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player is not assigned in GameManager!");
        }

        if (pipeSpawner == null)
        {
            Debug.LogError("Pipe Spawner is not assigned in GameManager!");
        }

        playerStartPos = player.transform.position;
        playerStartRotation = player.transform.rotation;

        gameObjectParent.SetActive(false);
    }

    private void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        gameObjectParent.SetActive(true);
        Canvas.SetActive(false);      
        gameStarted = true;

        ResetGame();
    }

    public void EndGame()
    {
        gameObjectParent.SetActive(false);
        Canvas.SetActive(true);   
        DialogueManager.Instance.StartDialogue(25);
        gameStarted = false;
    }

    public void ResetGame()
    {
        Debug.Log("Resetting game position...");

        if (player != null)
        {
            player.ResetPosition(playerStartPos, playerStartRotation);
        }
        else
        {
            Debug.LogError("Player reference is missing in ResetGame method!");
        }

        foreach (var pipe in pipes)
        {
            if (pipe != null)
            {
                Destroy(pipe.gameObject);
            }
        }

        pipes.Clear();

        if (pipeSpawner != null)
        {
            pipeSpawner.ResetSpawner();
            pipeSpawner.StartSpawning();
        }
        else
        {
            Debug.LogError("PipeSpawner is missing in ResetGame method!");
        }
    }

    private void OnEnable()
    {
        if (player != null)
        {
            playerStartPos = player.transform.position;
            playerStartRotation = player.transform.rotation;
        }
    }

    public void RegisterPipe(MovePipe pipe)
    {
        pipes.Add(pipe);
    }
}
