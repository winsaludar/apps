namespace Budget.Infrastructure.Expenses;

internal sealed class ExpenseCategoryTypeConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.ToTable("ExpenseCategories");

        builder.HasOne(x => x.ParentCategory)
            .WithMany(y => y.ChildCategories)
            .HasForeignKey(x => x.ParentCategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
