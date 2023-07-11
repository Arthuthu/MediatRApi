namespace EntityFrameworkProject.Request;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
