namespace NetBlocks.Models
{
    public static class Event
    {
        public delegate void EventBase();
        public delegate Task EventBaseAsync();
    }
    
    public static class Events
    {
        public delegate void Event();
        public delegate void Event<T>(T args);
        public delegate Task EventAsync();
        public delegate Task EventAsync<T>(T args);
    }
}
