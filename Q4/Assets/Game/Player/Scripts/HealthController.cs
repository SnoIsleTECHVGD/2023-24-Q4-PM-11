using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public Material swordMat;
    public Gradient healthGradient;
    
    public float Health = 100;

    private SwordController sword;
    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    private void Start()
    {
        sword = GetComponent<SwordController>();
    }

    private void Update()
    {
        if(sword.Sword)
        {
            Health = Mathf.Clamp(Health + Time.deltaTime * 8, 0, 100);
            swordMat.SetColor("_EmissiveColor", Color.Lerp(swordMat.GetColor("_EmissiveColor"), healthGradient.Evaluate(Health / 100) * 6, Time.deltaTime * 8));
        }     
    }
}
