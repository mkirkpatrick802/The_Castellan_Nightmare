public class HealthHolder
{
    private float _currentHealth;

    public HealthHolder(float maxHealth)
    {
        _currentHealth = maxHealth;
    }

    public float Health
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
}
