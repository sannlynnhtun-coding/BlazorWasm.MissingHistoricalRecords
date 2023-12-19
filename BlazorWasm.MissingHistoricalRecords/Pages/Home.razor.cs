using Microsoft.JSInterop;

namespace BlazorWasm.MissingHistoricalRecords.Pages;

public partial class Home
{
    private BookResponseModel? _data { get;set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.Delay(500); 
            await JsRuntime.InvokeVoidAsync("scrollTop");
            await JsRuntime.InvokeVoidAsync("loadJs", "themes/js/main.js");
            _data = await _service.GetBookList();
            StateHasChanged();
        }
    }
}
