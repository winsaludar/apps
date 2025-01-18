namespace Budget.Domain.Expenses;

public sealed class ExpenseCategory : Entity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Guid? ParentCategoryId { get; private set; }

    public ExpenseCategory(Guid userId, string name, string description, Guid? parentCategoryId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        CreatedBy = userId;
    }

    public void Update(Guid userId, string name, string description, Guid? parentCategoryId = null)
    {
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userId;
    }
}
