﻿namespace CaseTaskManager.Models.Case
{
    public class CreateCaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
