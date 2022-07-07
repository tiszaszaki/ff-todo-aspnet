namespace ff_todo_aspnet.PivotTables
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PivotFetch : Attribute
    {
        public PivotFetch(int order, string role, string display)
        {
            this.order = order;
            this.role = role;
            this.display = display;
        }
        public PivotFetch(int order, string display)
        {
            this.order = order;
            role = "";
            this.display = display;
        }
        public PivotFetch(int order)
        {
            this.order = order;
            role = "";
            display = "";
        }
        public PivotFetch()
        {
            order = 0;
            role = "";
            display = "";
        }

        public int order { get; }
        public string role { get; }
        public string display { get; }
    }
}
