﻿
namespace Zappr.Api.Domain
{
    public class UserSeries
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int SeriesId { get; set; }
        public Series Series { get; set; }
    }
}