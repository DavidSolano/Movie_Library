using System;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using NLog;

namespace Movie_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            string answer;
            int choice;
            
            //reads the files
            List<Movies> moviesList;
            
            using (var streamReader = new StreamReader(@"Files\\movies.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    moviesList = csvReader.GetRecords<Movies>().ToList();
                }
            }

            do
            {
                PrintStartMenu();
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    try
                    {

                        Console.Write("how many movies would you like to see> ");
                        int view = Convert.ToInt32(Console.ReadLine());

                        foreach (Movies movies in moviesList.GetRange(0, view))
                        {
                            Console.WriteLine(movies.movieID + " " + movies.title + " " + movies.genre);
                        }
                    }
                    catch (ExternalException e)
                    {
                        logger.Error(e, "something messed up when adding movie");
                        throw;
                    }
                }
                else if (choice == 2)
                {
                    try
                    {
                        int last = moviesList.Count;
                        // Console.WriteLine(last);
                        
                        Console.Write("enter movie title> ");
                        string newTitle = Console.ReadLine();

                        Console.Write("enter movie genre> ");
                        string newGenre = Console.ReadLine();

                        

                        var records = new List<Movies>
                        {
                            new Movies {movieID = last +1, title = newTitle, genre = newGenre},
                        };

                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = false,
                        };

                        using (var stream = File.Open(@"Files\\movies.csv", FileMode.Append))
                        {
                            using (var writer = new StreamWriter(stream))
                            {
                                using (var csv = new CsvWriter(writer, config))
                                {
                                    csv.WriteRecords(records);
                                }
                            }
                        }
                    }
                    catch (ExternalException e)
                    {
                        logger.Error(e,"Something messed up when appending this time");
                        throw;
                    }
                }
                else if (choice == 3)
                {
                    Console.Write("enter a random number between 1 and " + moviesList.Count);
                    int randomMovie = Convert.ToInt32(Console.ReadLine());
                    
                    RandomMovie(randomMovie);
                }
                else if (choice == 4)
                {
                    Console.WriteLine("guess how many movies there are in total");
                    Console.Write(">");
                    int guess = Convert.ToInt32(Console.ReadLine());
                    OptionalMiniGame(guess);
                }
                
                Console.WriteLine("would you like to go again?(y/n)");
                answer = Console.ReadLine().ToLower();
                
            } while (answer == "y");
            
            NLog.LogManager.Shutdown();
        }

        public static void RandomMovie(int randomId)
        {
            List<Movies> temp;
            using (var streamReader = new StreamReader(@"D:\movies\movies.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    temp = csvReader.GetRecords<Movies>().ToList();
                }
            }

            foreach (Movies movie in temp)
            {
                if (randomId == movie.movieID)
                {
                    Console.WriteLine(movie.movieID + " " + movie.title + " " + movie.genre);
                } else if (randomId != movie.movieID)
                {
                    Console.WriteLine("no movie matches that ID bud");
                }
            }
        }

        public static void PrintStartMenu()
        {
            Console.WriteLine("what would you like to do:");
            Console.WriteLine("1. look for movie?");
            Console.WriteLine("2. add a movie?");
            Console.WriteLine("3. get a random movie?");
            Console.WriteLine("4. optional mini-game?");
            Console.Write(">");
        }

        public static bool OptionalMiniGame(int x)
        {
            List<Movies> tempTwo;
            using (var streamReader = new StreamReader(@"D:\movies\movies.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    tempTwo = csvReader.GetRecords<Movies>().ToList();
                }
            }

            Console.WriteLine(tempTwo.Count);

            if (x == tempTwo.Count) return true;

            return false;
        }
    }
}
