using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue, answButtonText;
    public Image speakerSprite;
    private Conversation currentConvo;
    private int currentIndex;
    [SerializeField] RectTransform scrollRectTransform;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.answButtonText.text = "";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        
        if (currentIndex > currentConvo.GetLength())
        {
            RectTransformExtensions.SetHeight(scrollRectTransform, RectTransformExtensions.GetHeight(scrollRectTransform) + 58);
            dialogue.text = dialogue.text + "\nHero: " + currentConvo.GetLineByIndex(currentIndex - 1).answerText + "\n";
            answButtonText.text = "";
            Button button =  answButtonText.gameObject.GetComponentInParent<Button>();
            button.enabled = false;
            return;
        }
        if (currentIndex > 0)
        {
            RectTransformExtensions.SetHeight(scrollRectTransform, RectTransformExtensions.GetHeight(scrollRectTransform) + 58);
            dialogue.text = dialogue.text + "\nHero: " + currentConvo.GetLineByIndex(currentIndex - 1).answerText;
        }
        
        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
        dialogue.text = dialogue.text + "\n" + speakerName.text + ": " + currentConvo.GetLineByIndex(currentIndex).dialogue;
        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        answButtonText.text = currentConvo.GetLineByIndex(currentIndex).answerText;
        
        currentIndex++;
    }
}
