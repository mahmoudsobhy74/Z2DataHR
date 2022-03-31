using System;
using System.Collections.Generic;
using System.Text;

namespace Z2DataHR.Domain.Tasks.Commands
{
    public class DeleteTaskCommand : TaskCommand
    {
        public DeleteTaskCommand(Guid id)
        {
            Id = id;
        }
    }
}
