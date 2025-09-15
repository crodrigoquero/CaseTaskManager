﻿namespace CaseTaskManager.UI.Models.Case
{
    public class CaseItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CurrentStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
