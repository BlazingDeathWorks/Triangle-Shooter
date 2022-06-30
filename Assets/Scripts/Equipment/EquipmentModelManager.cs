using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModelManager : MonoBehaviour
{
    public EquipmentModel _currentModel { get; private set; }
    [SerializeField] private SpriteRenderer _equipmentSprite;
    private int _index = 0;
    private List<EquipmentModel> _equipmentModels = new List<EquipmentModel>();

    private void Awake()
    {
        //Disables everything except for the first children
        for (int i = 0; i < transform.childCount; i++)
        {
            _equipmentModels.Add(transform.GetChild(i).GetComponent<EquipmentModel>());
        }

        _currentModel = _equipmentModels[_index];
    }

    public void MoveForward()
    {
        _index = MathUtil.WrapInt(++_index, 0, _equipmentModels.Count - 1);

        EquipmentModel model = _equipmentModels[_index];
        _equipmentSprite.sprite = model.ModelDisplay;
        _currentModel = model;
    }

    public void MoveBackwards()
    {
        _index = MathUtil.WrapInt(--_index, 0, _equipmentModels.Count - 1);

        EquipmentModel model = _equipmentModels[_index];
        _equipmentSprite.sprite = model.ModelDisplay;
        _currentModel = model;
    }
}
