﻿using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.Menu
{
    public sealed class Menu : AggregateRoot<MenuId>
    {
        private readonly List<MenuSection> _sections = new();
        private readonly List<DinnerId> _dinnerIds = new();
        private readonly List<MenuReviewId> _menuReviewIds = new();
        public string Name { get; }
        public string Description { get; }
        public AverageRating AverageRating { get; }

        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();

        public HostId HostId { get; }
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
        public DateTime CreatedDateTime { get; }
        public DateTime UpdatedDateTime { get; }

        private Menu(
            MenuId menuId,
            HostId hostId,
            string name,
            string description,
            AverageRating averageRating,
            List<MenuSection> sections) : base(menuId)
        {
            HostId = hostId;
            Name = name;
            Description = description;
            AverageRating = averageRating;
            _sections = sections;
        }

        public static Menu Create(HostId hostId, string name, string description, List<MenuSection>? sections = null)
        {
            return new Menu(
                MenuId.CreateUnique(),
                hostId,
                name,
                description,
                AverageRating.CreateNew(),
                sections ?? new());
        }
    }
}
