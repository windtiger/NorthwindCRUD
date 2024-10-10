﻿using System;
using System.Collections.Generic;

namespace NorthwindCRUD.Models;

public partial class Publisher
{
    public string PubId { get; set; } = null!;

    public string? PubName { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual PubInfo? PubInfo { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
