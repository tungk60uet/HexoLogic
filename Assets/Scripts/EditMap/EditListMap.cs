using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditListMap : MonoBehaviour {

    public GameObject ListContainer,ItemPrefab;
    List<string> listMapId;
    private void Start()
    {
        string listidstr=PlayerPrefs.GetString("ListMapId");
        string[] temp = listidstr.Split('|');
        listMapId = new List<string>(temp);

        foreach (string  id in listMapId)
        {
            if (id != "")
            {
                GameObject item = Instantiate(ItemPrefab, ListContainer.transform);
                item.GetComponentInChildren<Text>().text = "Map " + id;
                item.GetComponent<Button>().onClick.AddListener(delegate { ItemClick(item, id); });
                item.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { DelItem(item, id); });
            }
        }
    }
    void saveListMapId()
    {
        string s = "";
        foreach (string id in listMapId)
        {
            s += id+"|";
        }
        s = s.TrimEnd('|');
        PlayerPrefs.SetString("ListMapId", s);
    }

    public void AddClick() {
        int id = PlayerPrefs.GetInt("LastId");
        PlayerPrefs.SetInt("LastId", id + 1);
        string strId = id.ToString()+"_"+SystemInfo.deviceUniqueIdentifier;
        GameObject item=Instantiate(ItemPrefab, ListContainer.transform);
        item.GetComponentInChildren<Text>().text = "Map " + strId;
        item.GetComponent<Button>().onClick.AddListener(delegate { ItemClick(item, strId); });
        item.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { DelItem(item, strId); });
        listMapId.Add(strId);
        saveListMapId();
        
    }
    void ItemClick(GameObject obj,string id)
    {
        EditMapHexGenerator.mapId = id;
        SceneManager.LoadScene("CreateMap");
    }
    void DelItem(GameObject obj,string id)
    {
        listMapId.Remove(id);
        saveListMapId();
        Destroy(obj);
    }
}
