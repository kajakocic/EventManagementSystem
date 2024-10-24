public class ReviewDTO
{
    public int UserReviewID { get; set; }
    public int EventReviewID { get; set; }
    public string? ImeKorisnika { get; set; }
    public string? NazivEventa { get; set; }
    public int Ocena { get; set; }
    public string? Komentar { get; set; }
}
