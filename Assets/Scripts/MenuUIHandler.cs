using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import text mesh pro namespace
using TMPro;
// import UI namespace so can use buttons
using UnityEngine.UI;
// import namespace so can open levels
using UnityEngine.SceneManagement;


public class MenuUIHandler : MonoBehaviour
{
    // Variable to store ref to TMP input game object
    public TMP_InputField inputField;
    // Variable to store ref to Start button
    public Button startButton;

    // Variable to store text component of the TMP input game object
    private string userName;


    // Start is called before the first frame update
    void Start()
    {
        // Attach the OnStartButtonPressed method to button's onClick event
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: Fix this - currently trying to access MainManager before it's been instantiated (currently instantiated in next scene). Need to add empty game object MainManager and MainManager script in this scene, then set up logic to assign game object references in the next scene
    // function called when start button is pressed
    public void OnStartButtonPressed()
    {
        // Get text from the input field and store in userInput variable
        DataHandler.Instance.userName = inputField.text;
        Debug.Log("User input stored: " + DataHandler.Instance.userName);
        // Launch game
        SceneManager.LoadScene(1);
    }




  
}
