using Core.Entities;

namespace Core.Models;

public class OverlapEventModel
{
    public string Title { get; set; }
    public Duration Duration { get; set; }
    public DateOnly Date { get; set; }
}
