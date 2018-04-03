public class SceneObject : Actor
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}

