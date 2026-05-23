using System;
using System.Collections.Generic;

namespace PRN232.LMS.Models.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
