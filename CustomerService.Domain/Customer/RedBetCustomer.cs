namespace CustomerService.Domain.Customer
{
    public class RedBetCustomer : global::CustomerService.Domain.Customer.Customer
    {
        protected RedBetCustomer() { }

        public RedBetCustomer(
            string firstName,
            string lastName,
            string favoriteFootballClub,
            Address address) : base(firstName, lastName, address)
        {
            FavoriteFootballClub = (FavoriteFootballClub) favoriteFootballClub;
        }

        public virtual FavoriteFootballClub FavoriteFootballClub { get; protected set; }

        public virtual void ChangeFavoriteFootballClub(string favoriteFootballClub)
        {
            FavoriteFootballClub = FavoriteFootballClub.Create(favoriteFootballClub);
        }
    }
}