using System;

namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishAt { get; private set; }

        public ProjectDetailsViewModel(string title, string description, decimal totalCost, DateTime? startedAt, DateTime? finishAt)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
            StartedAt = startedAt;
            FinishAt = finishAt;
        }
    }
}
