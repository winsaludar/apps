namespace Budget.Domain.Expenses;

public sealed class ExpenseCategory : Entity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Guid? ParentCategoryId { get; private set; }
    public ExpenseCategory? ParentCategory { get; private set; }

    // Navigation Property
    public ICollection<ExpenseCategory> ChildCategories { get; private set; }

    public ExpenseCategory(string name, string description, Guid createdBy, Guid? parentCategoryId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        CreatedBy = createdBy;
        ChildCategories = [];
    }

    public void Update(string name, string description, Guid updatedBy, Guid? parentCategoryId = null)
    {
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}
