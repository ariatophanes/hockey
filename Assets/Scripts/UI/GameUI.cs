using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameUI : MonoBehaviour
{
    [Inject]
    public void Construct(ILevelLoaderService levelLoaderService)
    {
        transform.Find("Level Text").GetComponent<Text>().text = "LVL " + (levelLoaderService.CurrentLevel + 1);
    }
}
