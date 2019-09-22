namespace Youtube2Mp3.Core.Services
{
    public interface IUiService
    {
        void SendOutput(string line);
        string GetInput();
    }
}
