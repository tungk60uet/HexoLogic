using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMapHexGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject HexPrefab;
    [SerializeField]
    private float offset;
    private int cols=12, rows=6;
    private GameObject[,] hexMatrix;
    public int[,] numTriInHexMatrix;
    public static float HexHeight,HexWidth;
    // Use this for initialization
    private void Awake()
    {
        hexMatrix = new GameObject[cols, rows];
        numTriInHexMatrix = new int[cols, rows];
        HexHeight = HexPrefab.GetComponent<SpriteRenderer>().bounds.size.y*offset;
        HexWidth = HexPrefab.GetComponent<SpriteRenderer>().bounds.size.x*offset;    
    }
    void Start () {
        transform.localPosition = new Vector3(-(cols - 1) * (3.1f * HexWidth / 4) / 2, (-(rows + 0.5f) / 2 + 1) * HexHeight, 10);
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                HexPrefab.GetComponent<Hex>().Num = 0;
                HexPrefab.GetComponent<Hex>().Pos = new Vector2(i,j);
                HexPrefab.transform.localPosition = new Vector3(i * (3.1f * HexWidth / 4), j * HexHeight - (i % 2) * (HexHeight / 2));
                hexMatrix[i,j]=Instantiate(HexPrefab, transform);
            }
        }
    }
    public GameObject getHex(Vector2 originPos,int direction)
    {
        switch(direction){
            case 0:
                {
                    if ((originPos.y + 1) < rows)
                        return hexMatrix[(int)originPos.x, (int) originPos.y + 1];
                }
                break;
            case 1:
                {
                    if ((originPos.x - 1) >= 0 && (originPos.y + (int)(originPos.x+1) % 2) < rows)
                        return hexMatrix[(int)(originPos.x - 1), (int)(originPos.y + (int)(originPos.x + 1) % 2)];
                }
                break;
            case 2:
                {
                    if ((originPos.x - 1) >= 0 && (originPos.y - (int)originPos.x % 2) >= 0)
                        return hexMatrix[(int)(originPos.x - 1), (int)(originPos.y - (int)originPos.x % 2)];
                }
                break;
            case 3:
                {
                    if ((originPos.y - 1) >=0)
                        return hexMatrix[(int)originPos.x, (int)originPos.y -1 ];
                }
                break;
            case 4:
                {
                    if ((originPos.x + 1) < cols && (originPos.y - (int)originPos.x % 2) >= 0)
                        return hexMatrix[(int)(originPos.x + 1), (int)(originPos.y - (int)originPos.x % 2)];
                }
                break;

            case 5:
                {
                    if ((originPos.x + 1) < cols && (originPos.y + (int)(originPos.x + 1) % 2) < rows)
                        return hexMatrix[(int)(originPos.x + 1), (int)(originPos.y + (int)(originPos.x + 1) % 2)];
                }
                break;
            default: break;
        }
        return null;
    }
}
