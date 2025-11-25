using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TodoAppDemo;

[PrimaryKey(nameof(Id))]
[Table("todo")]
public class Todo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Column("title")]
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [Column("is_done")]
    [JsonPropertyName("isDone")]
    public bool IsDone { get; set; } = false;
}
