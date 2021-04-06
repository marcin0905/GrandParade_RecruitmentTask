using System;
using Common.Domain;

namespace CustomerService.Domain.Customer
{
    public sealed class FavoriteFootballClub : ValueObject<FavoriteFootballClub>
    {
        public string Club { get; }

        protected FavoriteFootballClub() {}

        private FavoriteFootballClub(string club)
        {
            Club = club;
        }

        protected override bool EqualsCore(FavoriteFootballClub other)
        {
            return base.Equals(other) && Club == other.Club;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(base.GetHashCode(), Club);
        }

        public static FavoriteFootballClub Create(string favoriteFootballClub)
        {
            if (string.IsNullOrWhiteSpace(favoriteFootballClub))
            {
                throw new DomainException($"{nameof(favoriteFootballClub)} - cannot be empty");
            }

            return new FavoriteFootballClub(favoriteFootballClub);
        }

        public static implicit operator string(FavoriteFootballClub favoriteFootballClub) => favoriteFootballClub?.Club;
        public static explicit operator FavoriteFootballClub(string favoriteFootballClub) => Create(favoriteFootballClub);
    }
}