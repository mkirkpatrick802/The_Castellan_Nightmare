using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Inspector
    [SerializeField] private float moveSpeed;
    
    //Private
    private Rigidbody2D _rb;

    //Tutorial
    private TutorialManager _tutorial;
    private static bool _interacted;
    private static bool _upgraded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tutorial = GetComponent<TutorialManager>();
    }

    public void PlayerMove(Vector2 moveDir)
    {
        _rb.velocity = moveDir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Interactable":
                if (_interacted) return;
                _tutorial.ShowPrompt(TutorialPrompts.Interact);
                _interacted = true;
                break;
            case "Upgradeable":
                if(_upgraded) return;
                _tutorial.ShowPrompt(TutorialPrompts.Upgrade);
                _upgraded = true;
                break;

        }
    }
}
