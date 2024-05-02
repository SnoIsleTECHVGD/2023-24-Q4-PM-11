using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DeathMessage : MonoBehaviour
{

    public TextMeshProUGUI text;

    public textDeath[] Message;

    [System.Serializable]
    public struct textDeath 
    {
        public string Message;
        public  float fontSize;
    }

    public void Start()
    {
        textDeath death = Message[Random.Range(1, Message.Length)];
        text.text = death.Message;
        text.fontSize = death.fontSize;

    }
}
