namespace TieghiCorp.Core.Abstract;

public abstract record BaseEntity
{
    public int Id { get; set; }

    public void UpdateEntity(object updatedValues)
    {
        if (updatedValues == null)
            return;

        var updatedProperties = updatedValues.GetType().GetProperties();
        var entityType = this.GetType();

        foreach (var updatedProperty in updatedProperties)
        {
            var entityProperty = entityType.GetProperty(updatedProperty.Name);

            if (entityProperty != null && entityProperty.CanWrite)
            {
                var newValue = updatedProperty.GetValue(updatedValues);
                if (newValue != null)
                {
                    entityProperty.SetValue(this, Convert.ChangeType(newValue, entityProperty.PropertyType));
                }
            }
        }
    }
}