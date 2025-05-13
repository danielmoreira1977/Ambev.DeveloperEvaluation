using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class UserRegisteredEvent
    {
        public UserRegisteredEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}
