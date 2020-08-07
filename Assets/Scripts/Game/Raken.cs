using Assets.Scripts.Infrastructure;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Raken : MonoBehaviour
{
    [SerializeField]
    private GameObject _chatCloud;
    [SerializeField]
    private TextMeshProUGUI _chatCloudText;

    private Queue<Message> _messageQueue = new Queue<Message>();
    private bool _isTalking = false;

    public void AddMessage(Message message)
    {
        _messageQueue.Enqueue(message);

        if (!_isTalking)
            Talk();
    }

    public void ClearMessages()
    {
        CancelInvoke();
        _messageQueue.Clear();
        DeactivateChatCloud();
    }

    private void Talk()
    {
        _isTalking = true;

        if (_messageQueue.Count == 0)
            DeactivateChatCloud();
        else
        {
            var message = _messageQueue.Dequeue();

            _chatCloudText.text = message.Content;
            _chatCloud.SetActive(true);
            Invoke("Talk", message.TimeToLive);
        }
    }

    private void DeactivateChatCloud()
    {
        _isTalking = false;
        _chatCloudText.text = string.Empty;
        _chatCloud.SetActive(false);
    }
}