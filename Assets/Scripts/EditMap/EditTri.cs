using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTri : MonoBehaviour {
    [SerializeField]
    private GameObject TrianglePrefab;
    [SerializeField]
    private EditMapHexGenerator HexGenObj;
    private bool touching = false;
    private GameObject SelectedObject = null;
    GameObject ObjectClicked(Vector2 screenPosition)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 rayPos = new Vector2(worldPos.x, worldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        if (hit)
        {
            return hit.transform.gameObject;
        }
        else return null;
    }
    void touchBegin(Vector2 screenPosition)
    {
        SelectedObject = ObjectClicked(screenPosition);
    }
    void touchHold(Vector2 screenPosition)
    {
        
    }
    bool checkHasTri(GameObject obj,int direction)
    {
        if (obj.transform.childCount > 1)
        {
            for (int i = 1; i < obj.transform.childCount; i++)
            {
                if (obj.transform.GetChild(i).GetComponent<Tri>().Direction == direction)
                {
                    GameObject nearHex = HexGenObj.getHex(obj.GetComponent<Hex>().Pos, direction);
                    if(nearHex!=null)
                        HexGenObj.numTriInHexMatrix[(int)nearHex.GetComponent<Hex>().Pos.x, (int)nearHex.GetComponent<Hex>().Pos.y]--;
                    Destroy(obj.transform.GetChild(i).gameObject);
                    return true;
                }
            }
        }
        return false;
    }
    void touchEnd(Vector2 screenPosition)
    {
        if (SelectedObject != null)
        {
            if (SelectedObject.GetComponent<Hex>().Num > 0)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
                Vector2 directionVector = new Vector2(worldPos.x- SelectedObject.transform.position.x, worldPos.y- SelectedObject.transform.position.y);
                int Angle = Mathf.RoundToInt(Vector2.SignedAngle(Vector2.up, directionVector) / 60) * 60;
                Angle = Angle < 0 ? Angle += 360 : Angle;

                //có tam giác rồi thì bỏ tam giác đi
                //không có tam giác check
                    //rỗng thì thêm tam giác
                    //không rỗng thì đánh dấu không được sửa rồi thêm tam giác

                if (!checkHasTri(SelectedObject, Angle / 60))
                {
                    GameObject nearHex = HexGenObj.getHex(SelectedObject.GetComponent<Hex>().Pos, Angle / 60);
                    if (nearHex == null)
                    {
                        TrianglePrefab.GetComponent<Tri>().Num = 4;
                        TrianglePrefab.GetComponent<Tri>().Direction = Angle / 60;
                        TrianglePrefab.GetComponent<Tri>().Pos = SelectedObject.GetComponent<Hex>().Pos;
                        Instantiate(TrianglePrefab, SelectedObject.transform);
                        Debug.Log("null");
                    }
                    else if (nearHex.GetComponent<Hex>().Num == 0)
                    {
                        TrianglePrefab.GetComponent<Tri>().Num = 4;
                        TrianglePrefab.GetComponent<Tri>().Direction = Angle / 60;
                        TrianglePrefab.GetComponent<Tri>().Pos = SelectedObject.GetComponent<Hex>().Pos;
                        Instantiate(TrianglePrefab, SelectedObject.transform);
                        HexGenObj.numTriInHexMatrix[(int)nearHex.GetComponent<Hex>().Pos.x, (int)nearHex.GetComponent<Hex>().Pos.y]++;
                        Debug.Log("=0");
                    }
                }
                SelectedObject = null;
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !touching)
        {
            touching = true;
            touchBegin(Input.mousePosition);
        }
        if (touching)
        {
            touchHold(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touching = false;
            touchEnd(Input.mousePosition);
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchBegin(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchHold(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchEnd(Input.GetTouch(0).position);
            }
        }
    }
}
