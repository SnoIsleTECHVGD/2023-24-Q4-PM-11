using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoreTapes : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public GameObject LorePanel;

        public string Lore;
        public float FontSize;


    public void UpdateLore()
    {
        Text.text = Lore;
        Text.fontSize = FontSize;

    }

    public void CloseLore()
    {
        LorePanel.SetActive(false);
    }

    public void OpenLore()
    {
        LorePanel.SetActive(true);
        UpdateLore();
    }
}
