using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Todo_API.Models;

public partial class TodoItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Status { get; set; }
}
