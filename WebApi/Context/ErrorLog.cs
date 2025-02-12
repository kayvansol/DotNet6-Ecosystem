using System;
using System.Collections.Generic;

namespace WebApi.Context;

public partial class ErrorLog
{
    public int Id { get; set; }

    public DateTime? CreateDateTime { get; set; }

    public string? Parameters { get; set; }

    public string? Exception { get; set; }
}
