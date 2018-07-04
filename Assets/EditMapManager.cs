using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMapManager : MonoBehaviour {

    public GameObject EditTri, EditHex;
    private void Start()
    {
        changeEditMode(false);
    }
    public void changeEditMode(bool value)
    {
        if (value) {
            EditHex.SetActive(false);
            EditTri.SetActive(true);
        } else
        {
            EditHex.SetActive(true);
            EditTri.SetActive(false);
        }
    }
}
