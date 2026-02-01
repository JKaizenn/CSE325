using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }

context.Movie.AddRange(
    new Movie
    {
        Title = "Cars",
        ReleaseDate = DateTime.Parse("2006-6-9"),
        Genre = "Animation",
        Price = 9.99M,
        Rating = "G"
    },
    new Movie
    {
        Title = "Cars 2",
        ReleaseDate = DateTime.Parse("2011-6-24"),
        Genre = "Animation",
        Price = 9.99M,
        Rating = "G"
    },
    new Movie
    {
        Title = "Cars 3",
        ReleaseDate = DateTime.Parse("2017-6-16"),
        Genre = "Animation",
        Price = 9.99M,
        Rating = "G"
    },
    new Movie
    {
        Title = "The Count of Monte Cristo",
        ReleaseDate = DateTime.Parse("2002-1-25"),
        Genre = "Adventure",
        Price = 7.99M,
        Rating = "PG13"
    },
    new Movie
    {
        Title = "The Lord of the Rings: The Fellowship of the Ring",
        ReleaseDate = DateTime.Parse("2001-12-19"),
        Genre = "Fantasy",
        Price = 9.99M,
        Rating = "PG13"
    }
);
            context.SaveChanges();
        }
    }
}