using Core;
using Interfaces;
using TMPro;
using UnityEngine;

public class PrintDeaths : MonoBehaviour, ILevelLoaderArgsHandler
{
    [SerializeField] private TextMeshProUGUI _deathText;
    
    private int _deathCount;

    private void Start()
    {
        _deathText.text = $"You died: {_deathCount} times";
    }

    public void OnLevelLoad(int args)
    {
        _deathCount = args;
    }
}
