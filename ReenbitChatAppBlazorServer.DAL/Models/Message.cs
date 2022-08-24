namespace ReenbitChatAppBlazorServer.DAL.Models;

public class Message
{
    public int Id { get; set; }
    public ApplicationUser Author { get; set; }
    public Chat Chat { get; set; }
    public DateTime Date { get; set; }
    public Message? Reply { get; set; }
    public string Data { get; set; }
}