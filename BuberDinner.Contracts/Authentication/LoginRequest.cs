namespace BuberDinner.Contracts.Authantication;
public record LoginRequest(
    string Email,
    string Password
);