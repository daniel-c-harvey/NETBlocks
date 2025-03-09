namespace NetBlocks.Models
{
    public static class Event
    {
        public delegate void EventBase();
        public delegate Task EventBaseAsync();
    }
}
