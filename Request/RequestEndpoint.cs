namespace TanDotNet.Request
{
    public enum Group
    {
        Wallet
    }

    public enum Action
    {
        Addresses,
        Balance,
        Create,
        Mnemonic,
        List,
        History,
        Transaction,
        Receive,
        Spend
    }

    public class RequestEndpoint
    {
        public Group Group { get; set; }
        public Action Action { get; set; }

        public RequestEndpoint()
        {
        }

        public RequestEndpoint(Group Group, Action Action)
        {
            this.Group = Group;
            this.Action = Action;
        }
    }
}
