using System;
using System.Collections.Generic;

namespace WebApi.Context;

public partial class HandleLog
{
    public int Id { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public string? Parameters { get; set; }

    public int? ErrorCode { get; set; }

    public string? Exception { get; set; }

    public long? ExecuteTime { get; set; }
}
