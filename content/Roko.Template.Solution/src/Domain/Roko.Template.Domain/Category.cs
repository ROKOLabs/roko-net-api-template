namespace Roko.Template.Domain
{
    using System;

    public class Category: IResource
    {
        public Category(
            Guid id,
            string name,
            string description,
            string color,
            string icon,
            decimal amount)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Color = color;
            this.Icon = icon;
            this.Amount = amount;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public string Color { get; private set; } = default!;

        public string Icon { get; private set; } = default!;

        public decimal Amount { get; private set; }


        public void Update(
            string name,
            string description,
            string color,
            string icon,
            decimal amount)
        {
            this.Name = name;
            this.Description = description;
            this.Color = color;
            this.Icon = icon;
            this.Amount = amount;
        }
    }
}
