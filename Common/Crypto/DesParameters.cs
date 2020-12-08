namespace Common.Crypto
{
    public class DesParameters
    {
        public byte[] IV;
        public byte[] Key;

        public DesParameters(byte[] IV, byte[] Key)
        {
            this.IV = IV;
            this.Key = Key;
        }
    }
}