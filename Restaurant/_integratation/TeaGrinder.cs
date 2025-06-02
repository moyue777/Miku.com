public class TeaGrinder : DragCountFacility
{
    public Iinteract teaContainer;
    public override void Handle()
    {
        teaContainer.Recieve();
    }
}