using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyProjectApi.Models;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private static List<User> users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
        new User { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com" },
        new User { Id = 3, Name = "John Doe1", Email = "john1.doe@example.com" },
        new User { Id = 4, Name = "Jane Doe1", Email = "jane1.doe@example.com" },
        new User { Id = 5, Name = "John Doe2", Email = "john2.doe@example.com" },
        new User { Id = 6, Name = "Jane Doe2", Email = "jane2.doe@example.com" },
        new User { Id = 7, Name = "John Doe3", Email = "john3.doe@example.com" },
        new User { Id = 8, Name = "Jane Doe3", Email = "jane3.doe@example.com" },
        new User { Id = 9, Name = "John Doe4", Email = "john4.doe@example.com" },
        new User { Id = 10, Name = "Jane Doe4", Email = "jane4.doe@example.com" },
        new User { Id = 11, Name = "John Doe5", Email = "john5.doe@example.com" },
        new User { Id = 12, Name = "Jane Doe5", Email = "jane5.doe@example.com" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers() => Ok(users);

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        user.Id = users.Max(u => u.Id) + 1;
        users.Add(user);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Return 404 if user is not found
        }

        // Update the user's properties
        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;

        // Return the updated user with 200 OK status
        return Ok(user); // Return the updated user object
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        users.Remove(user);
        return NoContent();
    }

    // New search endpoint
    [HttpGet("search")]
    public ActionResult<IEnumerable<User>> SearchUsers(string query)
    {
        // Filter users based on whether Name or Email contains the query string
        var filteredUsers = users
            .Where(u => u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        u.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Return filtered users
        return Ok(filteredUsers);
    }
}
