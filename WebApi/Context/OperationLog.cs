using System;
using System.Collections.Generic;

namespace WebApi.Context;

public partial class OperationLog
{
    public int Id { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public string? Parameters { get; set; }

    public string? Answer { get; set; }

    public long? ExecuteTime { get; set; }
}
