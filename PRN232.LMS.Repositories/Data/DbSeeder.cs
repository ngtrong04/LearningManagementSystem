using PRN232.LMS.Models.Entities;

namespace PRN232.LMS.Repositories.Data
{
    public static class DbSeeder
    {
        public static void Seed(LmsDbContext context)
        {
            // =========================
            // SEMESTERS
            // =========================
            if (!context.Semesters.Any())
            {
                context.Semesters.AddRange(
                    new Semester
                    {
                        SemesterName = "Spring 2026",

                        StartDate = DateTime.SpecifyKind(
                            new DateTime(2026, 1, 1),
                            DateTimeKind.Utc),

                        EndDate = DateTime.SpecifyKind(
                            new DateTime(2026, 5, 1),
                            DateTimeKind.Utc)
                    },

                    new Semester
                    {
                        SemesterName = "Summer 2026",

                        StartDate = DateTime.SpecifyKind(
                            new DateTime(2026, 6, 1),
                            DateTimeKind.Utc),

                        EndDate = DateTime.SpecifyKind(
                            new DateTime(2026, 9, 1),
                            DateTimeKind.Utc)
                    },

                    new Semester
                    {
                        SemesterName = "Fall 2026",

                        StartDate = DateTime.SpecifyKind(
                            new DateTime(2026, 9, 2),
                            DateTimeKind.Utc),

                        EndDate = DateTime.SpecifyKind(
                            new DateTime(2026, 12, 31),
                            DateTimeKind.Utc)
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // SUBJECTS
            // =========================
            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(
                    new Subject
                    {
                        SubjectCode = "PRN232",
                        SubjectName = "Web API",
                        Credit = 3
                    },

                    new Subject
                    {
                        SubjectCode = "SWP391",
                        SubjectName = "Software Project",
                        Credit = 4
                    },

                    new Subject
                    {
                        SubjectCode = "DBI202",
                        SubjectName = "Database Systems",
                        Credit = 3
                    },

                    new Subject
                    {
                        SubjectCode = "MAD101",
                        SubjectName = "Mobile Development",
                        Credit = 3
                    },

                    new Subject
                    {
                        SubjectCode = "OSG202",
                        SubjectName = "Operating Systems",
                        Credit = 2
                    },

                    new Subject
                    {
                        SubjectCode = "CSD201",
                        SubjectName = "Data Structures",
                        Credit = 3
                    },

                    new Subject
                    {
                        SubjectCode = "PRJ301",
                        SubjectName = "Java Web",
                        Credit = 3
                    },

                    new Subject
                    {
                        SubjectCode = "SWR302",
                        SubjectName = "Software Requirements",
                        Credit = 2
                    },

                    new Subject
                    {
                        SubjectCode = "MOB103",
                        SubjectName = "Flutter Basic",
                        Credit = 3
                    },

                    new Subject
                    {
                        SubjectCode = "NET181",
                        SubjectName = "Network Fundamentals",
                        Credit = 2
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // STUDENTS
            // =========================
            if (!context.Students.Any())
            {
                var students = new List<Student>();

                for (int i = 1; i <= 100; i++)
                {
                    students.Add(new Student
                    {
                        FullName = $"Student {i}",

                        Email = $"student{i}@gmail.com",

                        DateOfBirth = DateTime.SpecifyKind(
                            new DateTime(2000, 1, 1)
                                .AddDays(i * 30),
                            DateTimeKind.Utc)
                    });
                }

                context.Students.AddRange(students);

                context.SaveChanges();
            }

            // =========================
            // COURSES
            // =========================
            if (!context.Courses.Any())
            {
                var springSemester = context.Semesters
                    .First(x => x.SemesterName == "Spring 2026");

                var summerSemester = context.Semesters
                    .First(x => x.SemesterName == "Summer 2026");

                var fallSemester = context.Semesters
                    .First(x => x.SemesterName == "Fall 2026");

                context.Courses.AddRange(

                    // SPRING
                    new Course
                    {
                        CourseName = "PRN232 API Spring",
                        SemesterId = springSemester.SemesterId
                    },

                    new Course
                    {
                        CourseName = "DBI202 Database Spring",
                        SemesterId = springSemester.SemesterId
                    },

                    new Course
                    {
                        CourseName = "CSD201 DSA Spring",
                        SemesterId = springSemester.SemesterId
                    },

                    // SUMMER
                    new Course
                    {
                        CourseName = "SWP391 Project Summer",
                        SemesterId = summerSemester.SemesterId
                    },

                    new Course
                    {
                        CourseName = "MAD101 Mobile Summer",
                        SemesterId = summerSemester.SemesterId
                    },

                    new Course
                    {
                        CourseName = "MOB103 Flutter Summer",
                        SemesterId = summerSemester.SemesterId
                    },

                    // FALL
                    new Course
                    {
                        CourseName = "NET181 Network Fall",
                        SemesterId = fallSemester.SemesterId
                    },

                    new Course
                    {
                        CourseName = "OSG202 OS Fall",
                        SemesterId = fallSemester.SemesterId
                    },

                    new Course
                    {
                        CourseName = "PRJ301 Java Web Fall",
                        SemesterId = fallSemester.SemesterId
                    }
                );

                context.SaveChanges();
            }

            // =========================
            // ENROLLMENTS
            // =========================
            if (!context.Enrollments.Any())
            {
                var students = context.Students.ToList();

                var courses = context.Courses.ToList();

                var enrollments = new List<Enrollment>();

                var random = new Random();

                var statuses = new[]
                {
                    "Studying",
                    "Completed",
                    "Pending",
                    "Dropped"
                };

                foreach (var student in students)
                {
                    var randomCourses = courses
                        .OrderBy(x => Guid.NewGuid())
                        .Take(3)
                        .ToList();

                    foreach (var course in randomCourses)
                    {
                        enrollments.Add(new Enrollment
                        {
                            StudentId = student.StudentId,

                            CourseId = course.CourseId,

                            EnrollDate = DateTime.SpecifyKind(
                                DateTime.UtcNow.AddDays(
                                    -random.Next(1, 100)),
                                DateTimeKind.Utc),

                            Status = statuses[
                                random.Next(statuses.Length)
                            ]
                        });
                    }
                }

                context.Enrollments.AddRange(enrollments);

                context.SaveChanges();
            }
        }
    }
}