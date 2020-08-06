using Assets.Scripts.Infrastructure;
using UnityEngine;

public class TipSpot : MonoBehaviour
{
    [SerializeField]
    private string _tip;
    [SerializeField]
    private int _timeToLive;

    private bool _hasBeenShown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_hasBeenShown)
        {
            var raken = GameObject.FindGameObjectWithTag("Raken").GetComponent<Raken>();
            raken.AddMessage(new Message(_tip, _timeToLive));
            _hasBeenShown = true;
        }
    }
}