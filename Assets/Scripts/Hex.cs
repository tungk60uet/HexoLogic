using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hex : MonoBehaviour {
    [SerializeField]
    private GameObject TriAnglePrefab;
    [SerializeField]
    private List<Sprite> imgNum;
    public int Num;
    public Vector2 Pos;
    //public int[] Tri=new int[6] { 0, 0, 0, 0, 0, 0};
	// Use this for initialization
	void Start () {
        transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sprite = imgNum[Num];
        //for (int i = 0; i < 6; i++)
        //{
        //    if (Tri[i] > 0)
        //    {
        //        Vector2 TrianglePosition = (Quaternion.Euler(0, 0, (60 * i)) * Vector2.up)*(EditMapHexGenerator.HexHeight/2)*0.785f;
        //        TriAnglePrefab.transform.eulerAngles = new Vector3(0, 0, (60 * i));
        //        TriAnglePrefab.transform.localPosition = TrianglePosition;
        //        TriAnglePrefab.transform.GetChild(0).eulerAngles = Vector3.zero;
        //        TriAnglePrefab.GetComponentInChildren<TextMesh>().text = Tri[i].ToString();
        //        Instantiate(TriAnglePrefab,transform);                 
        //    }
        //}
	}
    public void changeNum()
    {
        Num = (Num + 1) % 4;
        transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sprite = imgNum[Num];
    }
}
