using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance;
    [SerializeField] private AudioClip newMusicClip;
    [SerializeField] private AudioClip catMusicClip;
    [SerializeField] private AudioClip oldMusicClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    private bool CheckMusic = true;
    private bool CheckMusic1 = true;
    private bool CheckMusic2 = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndMusic()
    {

        if (audioSource != null && oldMusicClip != null)
        {

            audioSource.clip = oldMusicClip;
            audioSource.Play();
            animator.Play("DanceEnd");

        }
    }

    public void ChangeMusic1()
    {

        if (audioSource != null && newMusicClip != null)
        {
            if (CheckMusic)
            {
                DialogueManager.Instance.StartDialogue(15);
                CheckMusic = false;
            }
            else
            {
                if (CheckMusic1)
                {
                    DialogueManager.Instance.StartDialogue(29);
                    CheckMusic1 = false;
                }
                else
                    DialogueManager.Instance.StartDialogue(16);
            }
            

            audioSource.clip = newMusicClip;
            audioSource.Play();
            animator.Play("DanceStart");

        }
    }

    public void ChangeMusic2()
    {

        if (audioSource != null && catMusicClip != null)
        {
            if (CheckMusic)
            {
                DialogueManager.Instance.StartDialogue(15);
                CheckMusic = false;
            }
            else
            {
                if (CheckMusic2)
                {
                    DialogueManager.Instance.StartDialogue(22);
                    CheckMusic2 = false;
                }
                else
                    DialogueManager.Instance.StartDialogue(23);
            }
            

            audioSource.clip = catMusicClip;
            audioSource.Play();
            animator.Play("DanceStart");

        }
    }

}
