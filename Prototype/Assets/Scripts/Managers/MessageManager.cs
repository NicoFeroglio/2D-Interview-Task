using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance;

    [SerializeField] private TextMeshProUGUI feedback;
    [SerializeField] private GameObject interactableIcon;
    private GameObject _message;
    public float timeToClose;
    private event Action OnUpdate = delegate {  };
    private bool _waitForClose;
    [SerializeField] private Vector2 offset;

    private void Awake()
    {
        Instance = this;
        _message = transform.GetChild(0).gameObject;
    }

    public void OpenMessage(Transform target, string currentFeedback)
    {
        transform.position = target.position + (Vector3)offset;
        feedback.text = currentFeedback;
        _message.SetActive(true);
        _waitForClose = false;
    }

    public void CloseMessage()
    {
        _message.SetActive(false);
        interactableIcon.SetActive(false);
    }

    public void CloseInTime(string currentFeedback)
    {
        feedback.text = currentFeedback;
        _waitForClose = true;
        _message.SetActive(true);
        float time = timeToClose;
        
        OnUpdate = () =>
        {
            if (!_waitForClose)
            {
                OnUpdate = () => { };
            }
            else if ((time -= Time.deltaTime) <= 0)
            {
                OnUpdate = () => { };
                CloseMessage();
            }
        };
    }

    public void ActivateInteractionFeedback()
    {
        interactableIcon.SetActive(true);
    }
    
    private void Update()
    {
        OnUpdate();
    }
    
}
