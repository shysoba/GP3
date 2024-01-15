using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour
{
    public TextMeshProUGUI output;
    public TMP_InputField userName;

    public void ButtonDemo()
    {
        // name input
        output.text = "Hello, " + userName.text;
    }
}
