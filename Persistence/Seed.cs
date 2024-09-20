using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>{
                    new AppUser(){
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser(){
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },new AppUser(){
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    }
                };

                foreach(var user in users){
                    await userManager.CreateAsync(user,"123456aA@");
                }
            }
            if (context.Activities.Any()) return;

            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue = "Pub",
                },
                new Activity
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Paris",
                    Venue = "Louvre",
                },
                new Activity
                {
                    Title = "Future Activity 1",
                    Date = DateTime.UtcNow.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Category = "culture",
                    City = "London",
                    Venue = "Natural History Museum",
                },
                new Activity
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "O2 Arena",
                },
                new Activity
                {
                    Title = "Future Activity 3",
                    Date = DateTime.UtcNow.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Another pub",
                },
                new Activity
                {
                    Title = "Future Activity 4",
                    Date = DateTime.UtcNow.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Yet another pub",
                },
                new Activity
                {
                    Title = "Future Activity 5",
                    Date = DateTime.UtcNow.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Just another pub",
                },
                new Activity
                {
                    Title = "Future Activity 6",
                    Date = DateTime.UtcNow.AddMonths(6),
                    Description = "Activity 6 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "Roundhouse Camden",
                },
                new Activity
                {
                    Title = "Future Activity 7",
                    Date = DateTime.UtcNow.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new Activity
                {
                    Title = "Future Activity 8",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Cinema",
                }
            };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }

        public static async Task SeedDataUserBookings(DataContext context)
        {
            if (context.UserBookings.Any()) return;

            var userBookings = new List<UserBooking>
            {
                new UserBooking
                {
                    Subject = "Decoding",
                    StartTime = new DateTime(2024, 9, 19, 9, 30, 0), // month is 9 for September
                    EndTime = new DateTime(2024, 9, 19, 10, 30, 0),
                    IsAllDay = false,
                },
                new UserBooking
                {
                    Subject = "Bug Automation",
                    StartTime = new DateTime(2024, 9, 6, 13, 30, 0),
                    EndTime = new DateTime(2024, 9, 6, 16, 30, 0),
                    IsAllDay = false
                },
                new UserBooking
                {
                    Subject = "Functionality testing",
                    StartTime = new DateTime(2024, 9, 7, 9, 0, 0),
                    EndTime = new DateTime(2024, 9, 7, 10, 30, 0),
                    IsAllDay = false
                },
                new UserBooking
                {
                    Subject = "Resolution-based testing",
                    StartTime = new DateTime(2024, 9, 4, 12, 0, 0),
                    EndTime = new DateTime(2024, 9, 4, 13, 0, 0),
                    IsAllDay = false
                },
                new UserBooking
                {
                    Subject = "Test report Validation",
                    StartTime = new DateTime(2024, 9, 22, 15, 0, 0),
                    EndTime = new DateTime(2024, 9, 22, 18, 0, 0),
                    IsAllDay = false
                },
                new UserBooking
                {
                    Subject = "Test case correction",
                    StartTime = new DateTime(2024, 9, 15, 14, 0, 0),
                    EndTime = new DateTime(2024, 9, 15, 16, 0, 0),
                    IsAllDay = false,
                },
                new UserBooking
                {
                    Subject = "Bug fixing",
                    StartTime = new DateTime(2024, 9, 30, 14, 30, 0),
                    EndTime = new DateTime(2024, 9, 30, 18, 30, 0),
                    IsAllDay = false,
                },
                new UserBooking
                {
                    Subject = "Run test cases",
                    StartTime = new DateTime(2024, 9, 24, 17, 30, 0),
                    EndTime = new DateTime(2024, 9, 24, 19, 30, 0),
                    IsAllDay = false,
                },
                new UserBooking
                {
                    Subject = "Bug Automation",
                    StartTime = new DateTime(2024, 9, 14, 18, 30, 0),
                    EndTime = new DateTime(2024, 9, 14, 20, 0, 0),
                    IsAllDay = false
                }
            };

            await context.UserBookings.AddRangeAsync(userBookings);
            await context.SaveChangesAsync();
        }
    }
}