using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri : MonoBehaviour {
    public int Num;
    public int Direction;
    public Vector2 Pos;
	// Use this for initialization
	void Start () {
        float HexHeight = EditMapHexGenerator.HexHeight > HexGenerator.HexHeight ? EditMapHexGenerator.HexHeight : HexGenerator.HexHeight;
        Vector2 TrianglePosition = (Quaternion.Euler(0, 0, (60 * Direction)) * Vector2.up) * (HexHeight / 2) * 0.785f;
        transform.eulerAngles = new Vector3(0, 0, (60 * Direction));
        transform.localPosition = TrianglePosition;
        transform.GetChild(0).eulerAngles = Vector3.zero;
        GetComponentInChildren<TextMesh>().text = Num.ToString();
    }

}
