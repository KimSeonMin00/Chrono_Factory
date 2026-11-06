using UnityEngine;

[CreateAssetMenu(fileName = "ProducerData", menuName = "Scriptable Objects/Building Data/New ProducerData")]
public class ProducerData : BuildingData
{
    [Header("Producer Setting")]
    public RecipeData m_recipe;
    public override bool IsEnable_Spawn(Vector3Int vecCellPos)
    {
        if (!base.IsEnable_Spawn(vecCellPos))
            return false;

        return true;
    }

    public override Building SetUp_Building(GameObject goInstance, Vector3Int vecCellPos, RecipeData recipe)
    {
        if (recipe != null)
            m_recipe = recipe;
        else
            return null;

        Producer producer = goInstance.GetComponent<Producer>();

        producer.Init(this, vecCellPos, m_BuildingName);

        producer.Set_Recipe(recipe);

        return producer;
    }
}
