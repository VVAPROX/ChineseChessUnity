
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField]
    private GameObject movePlate;
    private static PrefabManager instance;
    private void Awake()
    {
        name = "PrefabManager";
        instance = this;
    }

    public static PrefabManager GetInstance()
    {
        return instance;
    }

    public GameObject GetMovePlate()
    {
        return movePlate;
    }
}