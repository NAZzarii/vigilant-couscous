using System;
using System.Collections;
using System.Collections.Generic;

public enum Genre
{
    Action,
    Comedy,
    Drama,
    SciFi
}

public class Director : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public object Clone()
    {
        return new Director { Name = this.Name, Age = this.Age };
    }

    public override string ToString()
    {
        return $"{Name} ({Age} years old)";
    }
}

public class Movie : IComparable<Movie>, ICloneable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Director Director { get; set; }
    public string Country { get; set; }
    public Genre Genre { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }

    public int CompareTo(Movie other)
    {
        return this.Year.CompareTo(other.Year);
    }

    public object Clone()
    {
        var movie = (Movie)this.MemberwiseClone();
        movie.Director = (Director)this.Director.Clone();
        return movie;
    }

    public override string ToString()
    {
        return $"{Title} ({Year}) - Genre: {Genre}, Rating: {Rating}/10, Director: {Director.Name}";
    }
}

public class Cinema : IEnumerable<Movie>
{
    private List<Movie> _movies = new List<Movie>();

    public void AddMovie(Movie movie)
    {
        _movies.Add(movie);
    }

    public void Sort(IComparer<Movie> comparer)
    {
        _movies.Sort(comparer);
    }

    public IEnumerator<Movie> GetEnumerator()
    {
        return _movies.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"Cinema contains {_movies.Count} movies.";
    }
}

public class RatingComparer : IComparer<Movie>
{
    public int Compare(Movie x, Movie y)
    {
        return x.Rating.CompareTo(y.Rating);
    }
}

public class TitleComparer : IComparer<Movie>
{
    public int Compare(Movie x, Movie y)
    {
        return string.Compare(x.Title, y.Title);
    }
}
