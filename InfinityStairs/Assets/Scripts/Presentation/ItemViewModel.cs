public class ItemViewModel
{
    public string GradePath { get; }
    public string IconPath { get; }

    public ItemViewModel (string gradePath, string iconPath)
    {
        GradePath = gradePath;
        IconPath = iconPath;
    }
}