namespace BlazorWasm.MissingHistoricalRecords.Models;

public class BookModel
{
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public string BookCover { get; set; }
    public string BookCategory { get; set; }
    public string BookDescription { get; set; }
}

public class BookResponseModel
{
    public List<BookModel> Data { get; set; }
    public int? TotalPage { get; set; }
}