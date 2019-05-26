namespace TanDotNet.Request
{
    public enum Group
    {
        Wallet
    }

    public enum Method
    {
        Address,
        Balance,
        Create,
        Profile,
        List,
        Receive,
        Send,
        Transactions,
        VaultUnseal
    }

    public class RequestEndPoint
    {
        public Group Group { get; set; }
        public Method Method { get; set; }

        public RequestEndPoint()
        {

        }

        public RequestEndPoint(Group Group, Method Method)
        {
            this.Group = Group;
            this.Method = Method;
        }
    }
}
