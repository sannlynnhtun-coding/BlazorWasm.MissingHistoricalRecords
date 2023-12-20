namespace BlazorWasm.MissingHistoricalRecords.Models;

public class BookContentModel
{
    public int PageNo { get; set; }
    public string Content { get; set; }
}

public class BookContentResponseModel
{
    public List<BookContentModel> Data { get; set; }
    public int? TotalPage { get; set; }
}
