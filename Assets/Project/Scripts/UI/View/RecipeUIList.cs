using UnityEngine;
using System.Collections.Generic;

public class RecipeUIList : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_recipeUIList;
    [SerializeField] private GameObject m_recipeUIPrefab;

    public float m_fOffsetY = 50;
    public float m_fRectY = 40;

    private void Awake()
    {
        m_recipeUIList = new List<GameObject>();
    }

    public void Set_Recipe(BuildingData building, List<RecipeData> recipes)
    {
        int i = 0;
        foreach(var Recipe in recipes)
        {
            GameObject recipeUI = Instantiate(m_recipeUIPrefab, this.transform);
            recipeUI.GetComponent<RecipeUI>().Set_Info(building, Recipe);
            recipeUI.GetComponent<RectTransform>().sizeDelta =new Vector2(m_fRectY, m_fRectY); 

            m_recipeUIList.Add(recipeUI);

            i++;
        }
    }


}
