namespace BuberDinner.Contracts.Authantication;
public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);