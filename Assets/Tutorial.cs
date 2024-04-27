using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public GameObject canvas;
    public GameObject view_howto;
    // Start is called before the first frame update
    void Start () {
        DontDestroyOnLoad (canvas);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            ToggleTutorial ();
        }
    }

    public void ToggleTutorial () {
        view_howto.SetActive(false);
        
        GetComponent<CanvasGroup> ().alpha = 1 -
            GetComponent<CanvasGroup> ().alpha;
    }

    public void openHowToPlay(){
        view_howto.SetActive(!view_howto.activeSelf);
    }
}