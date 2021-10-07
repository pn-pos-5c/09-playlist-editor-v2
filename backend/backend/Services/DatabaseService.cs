using backend.DTOs;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Services
{
    public class DatabaseService : IHostedService
    {
        private static List<GenreDto> genres;
        private static List<PlaylistDto> playlists;
        private static List<PlaylistTrackDto> playlistTracks;
        private static List<TrackDto> tracks;

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
            genres = ParseCsv<GenreDto>("./Assets/genre.csv");
            playlists = ParseCsv<PlaylistDto>("./Assets/playlist.csv");
            playlistTracks = ParseCsv<PlaylistTrackDto>("./Assets/playlist-track.csv");
            tracks = ParseCsv<TrackDto>("./Assets/track.csv");

            Console.WriteLine(genres);
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
                    bool isNumeric = int.TryParse(line[j], out int num);

                    var property = objType.GetProperty(headers[j]); // objType has no properties

                    if (isNumeric) property.SetValue(obj, num);
                    else property.SetValue(obj, line[j]);
                }

                result.Add(obj);
            }

            return result;
        }
    }
}
