using System;
using System.Collections.Generic;
using System.Text;

namespace Z2DataHR.Domain.Tasks.Events
{
    public class TaskDeletedEvent : TaskEvent
    {
        public TaskDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
