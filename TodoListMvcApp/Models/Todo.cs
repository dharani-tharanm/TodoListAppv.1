namespace TodoListMvcApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime EndTime { get; set; }
    }

}
