namespace Assets.Scripts.Infrastructure
{
    public sealed class Message
    {
        public string Content { get; }
        public int TimeToLive { get; }

        public Message(string content, int timeToLive)
        {
            Content = content;
            TimeToLive = timeToLive;
        }
    }
}