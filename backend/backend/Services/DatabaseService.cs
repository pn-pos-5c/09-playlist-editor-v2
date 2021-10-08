using backend.DTOs;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Services
{
    public class DatabaseService : IHostedService
    {
        public static List<GenreDto> Genres { get; set; }
        public static List<PlaylistDto> Playlists { get; set; }
        public static List<PlaylistTrackDto> PlaylistTracks { get; set; }
        public static List<TrackDto> Tracks { get; set; }

        private static List<string> LowerCase(string[] line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                line[i] = char.ToLower(line[i].ToString()[0]) + line[i][1..];
            }

            return line.ToList();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(ParseAllCsv, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void ParseAllCsv()
        {
            Genres = ParseCsv<GenreDto>("./Assets/genre.csv");
            Playlists = ParseCsv<PlaylistDto>("./Assets/playlist.csv");
            PlaylistTracks = ParseCsv<PlaylistTrackDto>("./Assets/playlist-track.csv");
            Tracks = ParseCsv<TrackDto>("./Assets/track.csv");
        }

        private List<T> ParseCsv<T>(string path)
        {
            List<T> result = new();
            string[] lines = Regex.Replace(File.ReadAllText(path), "\r", "").Split("\n");
            List<string> headers = new();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split("\",");
                line = Regex.Replace(string.Join("|", line), "\"", "").Split("|");

                if (i == 0)
                {
                    headers = LowerCase(line);
                    continue;
                }

                if (line.Length != headers.Count) continue;

                T obj = (T)Activator.CreateInstance(typeof(T));
                for (int j = 0; j < line.Length; j++)
                {
                    Type objType = obj.GetType();

                    var property = objType.GetProperty(headers[j]);

                    if (int.TryParse(line[j], out int numInt))
                    {
                        try // for names that can be parsed to int (e.g 1979)
                        {
                            property.SetValue(obj, numInt);
                        }
                        catch (Exception)
                        {
                            property.SetValue(obj, numInt.ToString());
                        }
                    }
                    else if (double.TryParse(line[j], out double numDouble))
                    {
                        try // for names that can be parsed to double (e.g. 5.15)
                        {
                            property.SetValue(obj, numDouble);
                        }
                        catch (Exception)
                        {
                            property.SetValue(obj, numDouble.ToString());
                        }
                    }
                    else property.SetValue(obj, line[j]);
                }

                result.Add(obj);
            }

            // result.ForEach(x => Debug.WriteLine(x.ToString()));
            return result;
        }
    }
}
