using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CapybaraCounter : MonoBehaviour
{
    public static CapybaraCounter Instance;
    public TextMeshProUGUI counterText;
    private int capybaraCount = 0;


    [SerializeField] private Animator CharacterAnimator;
    [SerializeField] private DialogueManager DialogPlayer;

    [SerializeField] private UnityEvent Smile;

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

    public void IncrementCount()
    {
        if(capybaraCount == 0)
        {
            DialogPlayer.StartDialogue(4);
        }

        if (capybaraCount == 10)
        {
            DialogPlayer.StartDialogue(27);
        }

        if (capybaraCount == 100)
        {
            DialogPlayer.StartDialogue(28);
        }

        Smile?.Invoke();
        capybaraCount++;
        counterText.text = "X " + capybaraCount;
    }
}
