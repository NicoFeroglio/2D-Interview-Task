using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : Character
{
    [HideInInspector] public PlayerInputController inputController;
    private PlayerAnimatorController _animatorController;
    private PlayerClothesController _clothesController;
    
    [Min(0)] public int coins;
    public byte speed;
    private bool _inAction;

    private bool _onWalk;
    public bool OnWalk {
        get => _onWalk;
        set {
            if (value != _onWalk)
            {
                _onWalk = value;
                _animatorController.Walk(_onWalk);
            }
        }
    }

    public Equipment defaultEquipment, currentEquipment;
    
    
    private void Awake()
    {
        inputController = GetComponent<PlayerInputController>();
        _clothesController = GetComponent<PlayerClothesController>();
        _animatorController = new PlayerAnimatorController(GetComponentInChildren<Animator>());

        foreach (var element in inventory.elements.Where(e => e.equiped))
            UseCloth(element);
    }

    private void Start()
    {
        GameManager.Instance.myCamera.SetTarget(transform);
    }

    public void Move(Vector3 dir)
    {
        OnWalk = (dir != Vector3.zero);
        if (_onWalk)
        {
            transform.position += dir.normalized * (Time.deltaTime * speed);
            SetGraphicDirection(dir.x);
        }
    }

    private void SetGraphicDirection(float x)
    {
        if (x != 0)
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Interactable interact = other.collider.GetComponent<Interactable>();
        if (interact != null)
        {
            InteractionManager.Instance.RequestInteraction(interact);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Interactable interact = other.collider.GetComponent<Interactable>();
        if (interact != null)
        {
            InteractionManager.Instance.ReleaseInteraction();
        }
    }

    public bool CanBuy(int price) => price <= coins;
    public void RefreshCoins(int value)
    {
        coins += value;
    }

    public void RequestInventory()
    {
        InventoryManager.Instance.OpenInventory(inventory, defaultEquipment, currentEquipment);
    }

    public void UseCloth(Element cloth)
    {
        _clothesController.UseCloth(cloth);
    }
}