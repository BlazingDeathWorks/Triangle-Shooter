using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EquipmentModelManager : MonoBehaviour
{
    public EquipmentModel CurrentModel { get; private set; }
    [SerializeField] private SpriteRenderer _equipmentSprite;
    private SpriteRenderer _sr;
    private List<EquipmentModel> _equipmentModels = new List<EquipmentModel>();
    private Vector2 _originalModelPos;
    private int _index = 0;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();

        //Disables everything except for the first children
        for (int i = 0; i < transform.childCount; i++)
        {
            _equipmentModels.Add(transform.GetChild(i).GetComponent<EquipmentModel>());
        }

        CurrentModel = _equipmentModels[_index];
        _originalModelPos = _equipmentSprite.transform.localPosition;
    }

    private void InitializeModel()
    {
        CurrentModel = _equipmentModels[_index];
        _equipmentSprite.transform.localPosition =  _originalModelPos + _equipmentModels[_index].Offset;
        _equipmentSprite.sprite = CurrentModel.ModelDisplay;
        _sr.sprite = CurrentModel.ModelDisplay;
    }

    public void MoveForward()
    {
        _index = MathUtil.WrapInt(++_index, 0, _equipmentModels.Count - 1);

        InitializeModel();
    }

    public void MoveBackwards()
    {
        _index = MathUtil.WrapInt(--_index, 0, _equipmentModels.Count - 1);

        InitializeModel();
    }
}
