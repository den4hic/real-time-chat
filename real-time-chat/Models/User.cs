using System.ComponentModel.DataAnnotations;

namespace real_time_chat.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Message> Messages { get; }
}