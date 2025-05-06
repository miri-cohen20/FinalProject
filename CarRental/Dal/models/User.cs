using System;
using System.Collections.Generic;

namespace Dal.models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string PhonNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int? BuildingNumber { get; set; }

    public string Password { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual Worker? Worker { get; set; }
}
