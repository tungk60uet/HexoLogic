using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject HexPrefab;
    [SerializeField]
    private float offset;
    private int cols=12, rows=6;
    public static float HexHeight,HexWidth;
    
    // Use this for initialization
    private void Awake()
    {
        HexHeight = HexPrefab.GetComponent<SpriteRenderer>().bounds.size.y*offset;
        HexWidth = HexPrefab.GetComponent<SpriteRenderer>().bounds.size.x*offset;    
    }
    void Start () {
        transform.localPosition = new Vector3(-(cols - 1) * (3.1f * HexWidth / 4) / 2, (-(rows + 0.5f) / 2 + 1) * HexHeight, 10);
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                HexPrefab.GetComponent<Hex>().Num = Random.RandomRange(0, 4);
                HexPrefab.transform.localPosition = new Vector3(i * (3.1f * HexWidth / 4), j * HexHeight - (i % 2) * (HexHeight / 2));
                Instantiate(HexPrefab, transform);
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
