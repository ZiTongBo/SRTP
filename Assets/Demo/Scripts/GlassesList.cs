using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GlassesList : MonoBehaviour {
    public List<string> glassesList = new List<string>();
    public static bool List_Show = false;
    public GameObject m_listPanel;
    public GameObject m_itemButton;
    public List<GameObject> items;
    int glassesCurIndex = 0;
    int num_perpage = 10;
    int current_page_index = -1;
    // Use this for initialization
    void Start () {
        glassesList=Mojing.glassesNameList;
        m_listPanel.SetActive(List_Show);
        nextPage();
        RefreshButtonColor();
    }

    void ShowGlasses(int page_index)
    {
        for (int i = 0; i < items.Count; i++)
        {
            DestroyObject(items[i]);
        }
        items.Clear();
        int num_lastpage = num_perpage;
        if (( page_index +1 ) * num_perpage > glassesList.Count)
        {
            num_lastpage = glassesList.Count % num_perpage;
        }
        for (int i = page_index * num_perpage; i < page_index * num_perpage + num_lastpage; i++)
        {
            GameObject glassesButton = Instantiate(m_itemButton);
            items.Add(glassesButton);
            glassesButton.transform.SetParent(m_itemButton.transform.parent);
            glassesButton.name = glassesList[i];
            glassesButton.GetComponent<Text>().text = glassesButton.name;
        }
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localPosition = m_itemButton.transform.localPosition - new Vector3(0, i * 20, 0);
            items[i].transform.localScale = m_itemButton.transform.localScale;
        }
    }

	// Update is called once per frame
	void Update () {
        if (List_Show)
        {
            m_listPanel.SetActive(true);
        }
        else
            m_listPanel.SetActive(false);
    }

    public void prevPage()
    {

        
        if (current_page_index > 0)
        {
            current_page_index--;
        }
        else
        {
            current_page_index = glassesList.Count / num_perpage;
        }
        ShowGlasses(current_page_index);
        int num_lastpage = num_perpage;
        if (num_perpage > items.Count)
        {
            num_lastpage = items.Count;
        }
        glassesCurIndex = num_lastpage - 1;

        RefreshButtonColor();
    }

    public void nextPage()
    {
        glassesCurIndex = 0;
        
        if (current_page_index < glassesList.Count/ num_perpage)
        {
            current_page_index++;
        }
        else
        {
            current_page_index = 0;
        }
        ShowGlasses(current_page_index);
        RefreshButtonColor();
    }

    public void HoverNext()
    {
        if (!List_Show)
            return;
        
        glassesCurIndex++;
        int num_lastpage = num_perpage;
        if (num_perpage > items.Count)
        {
            num_lastpage = items.Count;
        }
        if (glassesCurIndex >= num_lastpage)
        {
            nextPage();
            glassesCurIndex = 0;
        }

        RefreshButtonColor();
    }

    public void HoverPrev()
    {
        if (!List_Show)
            return;

        glassesCurIndex--;
        if (glassesCurIndex < 0)
        {
            prevPage();
        }
        int num_lastpage = num_perpage;
        if (num_perpage > items.Count)
        {
            num_lastpage = items.Count;
        }
        if (glassesCurIndex < 0)
        {
            glassesCurIndex = num_lastpage - 1;
        }
        RefreshButtonColor();
    }
    public Demo demo;
    public void PressCurrent()
    {
        demo.ChangeGlass(items[glassesCurIndex].name);
    }

    private void RefreshButtonColor()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i != glassesCurIndex)
            {
                items[i].GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
                items[i].GetComponent<Text>().fontSize = 14;
            }
            else
            {
                items[i].GetComponent<Text>().color = new Color(0.5f, 1.0f, 0.5f);
                items[i].GetComponent<Text>().fontSize = 16;
            }

        }
    }
}
