namespace KnowCloud.Service.Contract
{
    public interface ITokenProvider
    {
        void SetToken(string token);

        string GetToken();

        void ClearedToken();
    }
}
