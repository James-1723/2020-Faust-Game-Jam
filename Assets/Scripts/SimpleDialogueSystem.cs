using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue {
    [XmlElement ("name")]
    public string name;
    [XmlElement ("img")]
    public string img;
    [XmlElement ("content")]
    public string content;

    public Dialogue () { }
    public Dialogue (string name, string content) {
        this.name = name;
        this.content = content;
    }
}

//TODO: 立繪

public class SimpleDialogueSystem : MonoBehaviour {
    //UI component
    public CanvasGroup dialogueCanvas;
    public Text nameText;
    public Text contentText;
    public Button nextButton;
    //dialogue
    private int dialogueIndex;
    private List<Dialogue> dialogues;
    private bool isDialogueOpen;
    //word animation
    public float wordUpdateInterval;
    public float wordInterval;
    public int wordLength;
    //others
    public bool isDebug;

    void Start () {
        isDebug = false;
        wordUpdateInterval = 0.05f;
        dialogueCanvas = GameObject.Find ("CanvasDialogue").GetComponent<CanvasGroup> ();

        nextButton.onClick.AddListener (delegate () {
            dialogueIndex++;
            if (dialogueIndex >= dialogues.Count) {
                CloseDialogue ();
            } else {
                UpdateDialogue ();
            }
        });

        //show when needed
        CloseDialogue ();
    }

    public void StartDialogue (int index) {
        ReadDialogue (index);
        dialogueIndex = 0;
        OpenDialogue ();
        UpdateDialogue ();
    }

    public void UpdateDialogue () {
        nameText.text = dialogues[dialogueIndex].name;

        contentText.text = "";
        wordLength = 0;
        wordInterval = wordUpdateInterval;

        HideControlButton ();
    }

    public void OpenDialogue () {
        isDialogueOpen = true;
        dialogueCanvas.alpha = 1;
    }
    public void CloseDialogue () {
        isDialogueOpen = false;
        dialogueCanvas.alpha = 0;
    }

    public void ReadDialogue (int index) {
        string path = "dialogue" + index.ToString ();
        Debug.Log ("read new dialogue " + path);
        ReadXML (path);
    }

    public void ShowControlButton () {
        nextButton.gameObject.SetActive (true);
    }
    public void HideControlButton () {
        nextButton.gameObject.SetActive (false);
    }

    void Update () {
        DialogueRun ();
    }

    //文字跑馬燈
    private void DialogueRun () { //TODO: use state
        if (!isDialogueOpen) {
            return;
        }
        if ((contentText.text.Length == dialogues[dialogueIndex].content.Length)) {
            ShowControlButton ();
            return;
        }
        if (isDebug) {
            contentText.text = dialogues[dialogueIndex].content;
        } else {
            string content = dialogues[dialogueIndex].content;
            wordInterval -= Time.deltaTime;
            while (wordInterval < 0 && wordLength <= content.Length) {
                wordInterval += wordUpdateInterval;
                contentText.text += content[wordLength++];
            }
        }
    }

    public void ReadXML (string path) {
        //ref: http://answers.unity3d.com/questions/577984/how-to-deserialize-an-xml-textasset.html
        TextAsset texts = Resources.Load<TextAsset> (path);
        StringReader reader = new StringReader (texts.text);
        XmlSerializer serial = new XmlSerializer (typeof (Dialogues));
        dialogues = (serial.Deserialize (reader) as Dialogues).dialogues;
        reader.Close ();
    }
}