using FluentValidation;

namespace MedokStore.Application.Users.Queries.GetUserListByRole
{
    public class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator()
        {
            RuleFor(user => user.Role).NotEmpty();
        }
    }
}
