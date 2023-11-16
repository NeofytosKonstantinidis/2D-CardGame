using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Skin")]
public class So_Skin : ScriptableObject
{
    public Material frontMat;
    public Material backMatBlue;
    public Material backMatRed;
    public Material backMatNeutral;
    public int id;
    [Range(0,9999f)]
    public float cost = 0;
    [Range(0, 9999f)]
    public float saleCost = 0;
    public Sprite shopImage;
    public bool commingSoon;
}
