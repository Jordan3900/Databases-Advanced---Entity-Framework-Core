namespace PhotoShare.Services
{
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;
    using PhotoShare.Data;
    using System.Linq;
    using System;
    using AutoMapper.QueryableExtensions;
    using PhotoShare.Models.Enums;
    using System.Collections.Generic;

    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id)
                      => By<TModel>(x => x.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name)
                      => By<TModel>(x => x.Name == name).SingleOrDefault();

        public Album Create(int userId, string albumTitle, string BgColor, string[] tags)
        {
            var albumColor = Enum.Parse<Color>(BgColor);

            var album = new Album()
            {
                Name = albumTitle,
                BackgroundColor = albumColor,
            };

            this.context.Albums.Add(album);
            this.context.SaveChanges();

            var albumRole = new AlbumRole()
            {
                UserId = userId,
                Album = album
            };

            this.context.AlbumRoles.Add(albumRole);
            this.context.SaveChanges();

            foreach (var tag in tags)
            {
                var currentTag = this.context.Tags.FirstOrDefault(x => x.Name == tag).Id;

                var albumTag = new AlbumTag
                {
                    Album = album,
                    TagId = currentTag
                };

            }
             
            this.context.SaveChanges();

            return album; 
        }

        public bool Exists(int id)
                     => ById<Album>(id) != null;

        public bool Exists(string name)
                     => ByName<Album>(name) != null;

        private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate)
                 => this.context.Albums.Where(predicate).AsQueryable().ProjectTo<TModel>();
    }
}
