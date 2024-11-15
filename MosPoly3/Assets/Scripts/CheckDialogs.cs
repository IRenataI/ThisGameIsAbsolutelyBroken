using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDialogs : MonoBehaviour
{
    private bool CheckCharacterDialog = true;
    private bool CheckSnow = true;
    private bool CheckGame = true;
    private int CheckJokes = 0;

    [SerializeField] private DialogueManager DialogPlayer;
    public void PlayCharacterTalk()
    {
        if(CheckCharacterDialog)
        {
            DialogPlayer.StartDialogue(1);
            CheckCharacterDialog = false;
        }
        else
        {
            DialogPlayer.StartDialogue(2);
        }
    }

    public void StartSnow()
    {
        if (CheckSnow)
        {
            DialogPlayer.StartDialogue(11);
            CheckSnow = false;
        }
        else
        {
            DialogPlayer.StartDialogue(12);
        }
    }
    public void StartJokes()
    {
        switch (CheckJokes) 
        {
            case 0:
                DialogPlayer.StartDialogue(8);
                CheckJokes = 1;
                break;
            case 1:
                DialogPlayer.StartDialogue(9);
                CheckJokes = 2;
                break;
            case 2:
                DialogPlayer.StartDialogue(10);
                CheckJokes = 3;
                break;
            case 3:
                DialogPlayer.StartDialogue(17);
                CheckJokes = 4;
                break;
            case 4:
                DialogPlayer.StartDialogue(18);
                CheckJokes = 5;
                break;

        }
    }
    public void StartGame()
    {
        if (CheckGame)
        {
            DialogPlayer.StartDialogue(24);
            CheckGame = false;
        }
        else
        {
            DialogPlayer.StartDialogue(26);
        }
    }
    public void Twitch()
    {
        Application.OpenURL("https://www.twitch.tv/iavocadoi");
    }
}
