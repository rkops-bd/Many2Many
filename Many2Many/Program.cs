using Many2Many;
using Many2Many.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var host = Startup.CreateHostBuilder(args);

using var serviceScope = host.Services
    .GetRequiredService<IServiceScopeFactory>()
    .CreateScope();

await using var context = serviceScope.ServiceProvider.GetService<MyContext>();
await context!.Database.MigrateAsync();

var student = context.Students
    .Include(x => x.CourseSubscriptions)
        .ThenInclude(x => x.Course)
    .FirstOrDefault();

if (student == null)
{
    var courses = context.Courses.ToList();
    student = new Student
    {
        FirstName = "Piet",
        LastName = "Hein",
        Courses = courses
    };
    context.Students.Add(student);
    context.SaveChanges();
}
else
{
    var c = student.Courses.ToList()[0];
    student.Courses.Remove(c);
    await context.SaveChangesAsync();
}



Console.ReadLine();