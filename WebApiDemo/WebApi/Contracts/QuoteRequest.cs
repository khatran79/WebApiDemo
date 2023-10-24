namespace WebApi.Contracts
{
    public class QuoteRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<Listing> Listings { get; set; }        
    }
}
