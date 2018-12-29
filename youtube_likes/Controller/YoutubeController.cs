using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using LiteDB;
using FileMode = System.IO.FileMode;

namespace YoutubeLikes.Controller
{
    public class YoutubeController
    {
        public async void Init(Action<int, int> progressNotificationAction)
        {
            UserCredential credential;
            using (FileStream stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                                                                               // This OAuth 2.0 access scope allows for full read/write access to the
                                                                               // authenticated user's account.
                                                                               new[] {YouTubeService.Scope.Youtube},
                                                                               "user",
                                                                               CancellationToken.None,
                                                                               new FileDataStore(GetType().ToString()));
            }

            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = GetType().ToString()
            });

            ChannelsResource.ListRequest channelsCommand = youtubeService.Channels.List("snippet");
            channelsCommand.Mine = true;
            ChannelListResponse channels = await channelsCommand.ExecuteAsync();
            foreach (Channel channel in channels.Items)
            {
                Console.WriteLine($@"Channel: {channel.Snippet.Title}");
            }


            PlaylistsResource.ListRequest playlistsCommand = youtubeService.Playlists.List("snippet");
            playlistsCommand.Mine = true;
            PlaylistListResponse playlists = await playlistsCommand.ExecuteAsync();
            foreach (Playlist playlist in playlists.Items)
            {
                Console.WriteLine($@"Playlist: {playlist.Snippet.Title}");
            }

            VideosResource.ListRequest videoCommand = youtubeService.Videos.List("snippet");
            videoCommand.MyRating = VideosResource.ListRequest.MyRatingEnum.Like;
            videoCommand.MaxResults = 10;
            var allVideos = new List<Video>();
            VideoListResponse pagedVideos = await videoCommand.ExecuteAsync();

            // Open database (or create if not exits)
            using (LiteDatabase db = new LiteDatabase(@"DB.db"))
            {
                var videosDB = db.GetCollection<Video>("videos");
                foreach (Video video in videosDB.Find(Query.All()))
                {
                    Console.WriteLine($@"Existing video: {video.Snippet.Title}");
                }

                int nProcessedVideos = 0;
                bool run = pagedVideos.Items.Count > 0;

                for (int pageNr = 0; run; pageNr++)
                {
                    nProcessedVideos += pagedVideos.Items.Count;
                    progressNotificationAction(nProcessedVideos, pagedVideos.PageInfo.TotalResults.GetValueOrDefault(-1));

                    allVideos.AddRange(pagedVideos.Items);
                    Console.WriteLine($@"Count on page {pageNr + 1}: {pagedVideos.Items.Count}, Next: {pagedVideos.NextPageToken}");

                    foreach (Video video in pagedVideos.Items)
                    {
                        // Console.WriteLine($@"Video: {video.Snippet.Title}");
                        if (videosDB.FindById(video.Id) == null)
                        {
                            videosDB.Insert(video);
                            videosDB.EnsureIndex(vid => vid.Id);
                        }
                    }

                    if (string.IsNullOrEmpty(pagedVideos.NextPageToken))
                    {
                        run = false;
                    }
                    else
                    {
                        videoCommand.PageToken = pagedVideos.NextPageToken;
                        pagedVideos = await videoCommand.ExecuteAsync();
                    }
                }
            }
        }
    }
}