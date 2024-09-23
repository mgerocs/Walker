using UnityEngine;

public class TransitionScreen : ScreenBase, IScreen
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        if (_animator != null)
        {
            _animator.Play("FadeIn", 0, 0f);
        }
    }

    public void OnFadeInFinished()
    {
        gameObject.SetActive(false);
    }
}
