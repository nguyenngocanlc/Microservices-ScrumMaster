using FluentValidation;
using SprintManagement.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintManagement.Application.Validators
{
    public class UpdateIssueInSprintValidator : AbstractValidator<UpdateIssueInSprintCommand>
    {
        public UpdateIssueInSprintValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty();
            RuleFor(x => x.SprintId)
                .NotEqual(0);
        }
    }
}
