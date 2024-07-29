using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace real_time_chat.Models;

public class Message
{
    [Key]
    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTime SentDate { get; set; }
    public string? Sentiment { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}