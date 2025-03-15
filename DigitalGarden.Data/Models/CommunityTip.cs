using System;

namespace MVCView.Models
{
public class CommunityTip
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? SubmittedBy { get; set; }
    public DateTime SubmittedDate { get; set; }
}
}
