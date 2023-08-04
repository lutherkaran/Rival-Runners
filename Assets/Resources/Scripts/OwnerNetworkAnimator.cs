using Unity.Netcode.Components;

public class OwnerNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return base.OnIsServerAuthoritative();
        return false;
    }

}
