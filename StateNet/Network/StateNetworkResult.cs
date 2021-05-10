namespace Aptacode.StateNet.Network
{
    public record StateNetworkResult(string Message, bool Success, StateNetwork? Network)
    {
        public static StateNetworkResult Fail(string message)
        {
            return new(message, false, null);
        }

        public static StateNetworkResult Ok(StateNetwork network, string message)
        {
            return new(message, true, network);
        }
    }
}