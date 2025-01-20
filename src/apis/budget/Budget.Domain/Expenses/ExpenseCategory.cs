namespace Budget.Domain.Expenses;

public sealed class ExpenseCategory : Entity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Guid? ParentCategoryId { get; private set; }
    public ExpenseCategory? ParentCategory { get; private set; }

    // Navigation Property
    public ICollection<ExpenseCategory> ChildCategories { get; private set; }

    public ExpenseCategory(Guid id, string name, string description, Guid createdBy, Guid? parentCategoryId = null)
    {
        SetId(id);
        SetName(name);
        SetDescription(description);
        ParentCategoryId = parentCategoryId;
        CreatedBy = createdBy;
        ChildCategories = [];
    }

    public void Update(string name, string description, Guid updatedBy, Guid? parentCategoryId = null)
    {
        SetName(name);
        SetDescription(description);
        ParentCategoryId = parentCategoryId;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    public void SetId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ExpenseException("Invalid expense category id");

        Id = id;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ExpenseException("Name cannot be empty");

        Name = name;
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            throw new ExpenseException("Description cannot be empty");

        Description = description;
    }

    public void SetParentCategoryId(Guid? parentCategoryId = null)
    {
        if (parentCategoryId is not null && parentCategoryId == Guid.Empty)
            throw new ExpenseException("Invalid parent category id");

        if (parentCategoryId is not null && parentCategoryId == Id)
            throw new ExpenseException("Cannot set parent to self");

        ParentCategoryId = parentCategoryId;
    }

    public void AddSubCategory(ExpenseCategory category)
    {
        if (ParentCategory is not null)
            throw new ExpenseException("Cannot add to a sub category");

        if (ChildCategories.Any(x => x.Id == category.Id))
            throw new ExpenseException("Sub category already exist");

        ChildCategories.Add(category);
    }
}
